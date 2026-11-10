using MoneyControlApp.Dtos;

namespace MoneyControlApp.Services;

/// <summary>Panel de administracion (solo rol Administrador): listar, buscar, activar y desactivar usuarios.</summary>
public class UsuarioService
{
    private readonly ApiClient _api;
    public UsuarioService(ApiClient api) => _api = api;

    /// <param name="estado">"activos", "inactivos" o vacio (todos)</param>
    public async Task<UsuariosRespuesta> ListarAsync(string? buscar, string? estado, int page, int pageSize)
    {
        var url = $"api/Usuarios?page={page}&pageSize={pageSize}";
        if (!string.IsNullOrWhiteSpace(buscar)) url += $"&buscar={Uri.EscapeDataString(buscar.Trim())}";
        if (!string.IsNullOrWhiteSpace(estado)) url += $"&estado={Uri.EscapeDataString(estado)}";
        return await _api.GetAsync<UsuariosRespuesta>(url) ?? new();
    }

    /// <summary>Un usuario desactivado no puede iniciar sesion. La API no deja cambiar el estado de la propia cuenta.</summary>
    public Task CambiarEstadoAsync(int id, bool activo)
        => _api.PutAsync($"api/Usuarios/{id}/estado", new CambiarEstadoRequest { Estado = activo });
}
