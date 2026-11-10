using MoneyControlApp.Dtos;

namespace MoneyControlApp.Services;

/// <summary>Notificaciones del usuario. Guarda la cantidad de no leidas para el globito de la barra superior.</summary>
public class NotificacionService
{
    private readonly ApiClient _api;

    public int NoLeidas { get; private set; }

    /// <summary>Se dispara cuando cambia el contador (el layout se redibuja).</summary>
    public event Action? Cambio;

    public NotificacionService(ApiClient api, AuthService auth)
    {
        _api = api;
        auth.Cambio += () =>
        {
            // Al cerrar sesion el contador vuelve a cero
            if (!auth.EstaLogueado) Actualizar(0);
        };
    }

    public async Task<NotificacionesRespuesta> ListarAsync(int page = 1, int pageSize = 20)
    {
        var r = await _api.GetAsync<NotificacionesRespuesta>($"api/Notificaciones?page={page}&pageSize={pageSize}")
                ?? new NotificacionesRespuesta();
        Actualizar(r.NoLeidas);
        return r;
    }

    /// <summary>Pide solo el contador (una notificacion por pagina alcanza). Si falla, no molesta al usuario.</summary>
    public async Task RefrescarAsync()
    {
        try
        {
            var r = await _api.GetAsync<NotificacionesRespuesta>("api/Notificaciones?page=1&pageSize=1");
            if (r is not null) Actualizar(r.NoLeidas);
        }
        catch { /* sin conexion o sesion vencida: el globito queda como estaba */ }
    }

    public async Task MarcarLeidaAsync(int id)
    {
        await _api.PutAsync($"api/Notificaciones/{id}/leida");
        await RefrescarAsync();
    }

    public async Task MarcarTodasLeidasAsync()
    {
        await _api.PutAsync("api/Notificaciones/leer-todas");
        Actualizar(0);
    }

    private void Actualizar(int valor)
    {
        NoLeidas = valor;
        Cambio?.Invoke();
    }
}
