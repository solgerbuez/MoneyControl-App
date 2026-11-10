using System.Net.Http.Json;
using MoneyControlApp.Dtos;

namespace MoneyControlApp.Services;

/// <summary>
/// Sesion del usuario: hace el login / registro, guarda el token (SecureStorage) y lo expone al resto de la app.
/// Usa HttpClient directo (no ApiClient) porque ApiClient depende de este servicio.
/// </summary>
public class AuthService
{
    private const string KToken = "auth_token";
    private const string KNombre = "auth_nombre";
    private const string KApellido = "auth_apellido";
    private const string KEmail = "auth_email";
    private const string KRol = "auth_rol";
    private const string KExpira = "auth_expira";

    private readonly HttpClient _http;
    private Task? _inicio;

    public string? Token { get; private set; }
    public string? Nombre { get; private set; }
    public string? Apellido { get; private set; }
    public string? Email { get; private set; }
    public string? Rol { get; private set; }

    public bool EstaLogueado => !string.IsNullOrEmpty(Token);
    public bool EsAdmin => Rol == "Administrador";
    public string NombreCompleto => $"{Nombre} {Apellido}".Trim();

    /// <summary>Se dispara cuando cambia la sesion (login / logout) para que el menu se redibuje.</summary>
    public event Action? Cambio;

    public AuthService(HttpClient http) => _http = http;

    /// <summary>Recupera la sesion guardada. Se puede llamar desde cualquier pagina: solo se ejecuta una vez.</summary>
    public Task InicializarAsync() => _inicio ??= RestaurarAsync();

    private async Task RestaurarAsync()
    {
        try
        {
            var token = await SecureStorage.Default.GetAsync(KToken);
            var expira = await SecureStorage.Default.GetAsync(KExpira);

            // Si el token ya vencio no tiene sentido restaurarlo
            if (!string.IsNullOrEmpty(token) &&
                DateTime.TryParse(expira, null, System.Globalization.DateTimeStyles.RoundtripKind, out var vence) &&
                vence.ToUniversalTime() > DateTime.UtcNow)
            {
                Token = token;
                Nombre = await SecureStorage.Default.GetAsync(KNombre);
                Apellido = await SecureStorage.Default.GetAsync(KApellido);
                Email = await SecureStorage.Default.GetAsync(KEmail);
                Rol = await SecureStorage.Default.GetAsync(KRol);
            }
        }
        catch
        {
            // SecureStorage puede no estar disponible (ej: Windows sin empaquetar). La sesion queda solo en memoria.
        }
        Cambio?.Invoke();
    }

    public Task LoginAsync(string email, string password)
        => IniciarSesionAsync("api/Auth/login", new LoginRequest { Email = email, Password = password });

    /// <summary>Registro publico: la API crea el usuario y devuelve el token, asi que queda logueado.</summary>
    public Task RegistrarAsync(RegistroRequest datos)
        => IniciarSesionAsync("api/Auth/registro", datos);

    // Login y registro responden lo mismo (LoginResponse con el token): comparten este metodo
    private async Task IniciarSesionAsync(string url, object body)
    {
        HttpResponseMessage resp;
        try
        {
            resp = await _http.PostAsync(url, JsonContent.Create(body, body.GetType(), options: ApiClient.Json));
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException(0, "No se pudo conectar con el servidor. Revisá tu conexión a internet.", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new ApiException(0, "El servidor tardó demasiado en responder.", ex);
        }

        using (resp)
        {
            if (!resp.IsSuccessStatusCode)
            {
                // La API manda el motivo en texto ("Email o contraseña incorrectos." / "Tu cuenta está desactivada...")
                var mensaje = await ApiClient.LeerMensajeAsync(resp);
                throw new ApiException((int)resp.StatusCode, mensaje);
            }

            var datos = await resp.Content.ReadFromJsonAsync<LoginResponse>(ApiClient.Json)
                        ?? throw new ApiException((int)resp.StatusCode, "Respuesta inválida del servidor.");

            Token = datos.Token;
            Nombre = datos.Nombre;
            Apellido = datos.Apellido;
            Email = datos.Email;
            Rol = datos.Rol;

            try
            {
                await SecureStorage.Default.SetAsync(KToken, datos.Token);
                await SecureStorage.Default.SetAsync(KNombre, datos.Nombre);
                await SecureStorage.Default.SetAsync(KApellido, datos.Apellido);
                await SecureStorage.Default.SetAsync(KEmail, datos.Email);
                await SecureStorage.Default.SetAsync(KRol, datos.Rol);
                await SecureStorage.Default.SetAsync(KExpira, datos.Expira.ToString("O"));
            }
            catch { /* igual que arriba: sin almacenamiento seguro la sesion dura mientras la app este abierta */ }

            Cambio?.Invoke();
        }
    }

    public Task LogoutAsync()
    {
        Token = Nombre = Apellido = Email = Rol = null;
        try
        {
            SecureStorage.Default.Remove(KToken);
            SecureStorage.Default.Remove(KNombre);
            SecureStorage.Default.Remove(KApellido);
            SecureStorage.Default.Remove(KEmail);
            SecureStorage.Default.Remove(KRol);
            SecureStorage.Default.Remove(KExpira);
        }
        catch { }
        Cambio?.Invoke();
        return Task.CompletedTask;
    }
}
