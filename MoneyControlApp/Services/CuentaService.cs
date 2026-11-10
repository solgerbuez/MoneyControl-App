using MoneyControlApp.Dtos;

namespace MoneyControlApp.Services;

public class CuentaService
{
    private readonly ApiClient _api;
    public CuentaService(ApiClient api) => _api = api;

    public async Task<List<CuentaDto>> ListarAsync()
        => await _api.GetAsync<List<CuentaDto>>("api/Cuentas") ?? new();

    public Task<CuentaDto?> CrearAsync(CuentaGuardarRequest datos)
        => _api.PostAsync<CuentaDto>("api/Cuentas", datos);

    public Task EditarAsync(int id, CuentaGuardarRequest datos)
        => _api.PutAsync($"api/Cuentas/{id}", datos);

    /// <summary>La API no deja eliminar una cuenta que tiene movimientos.</summary>
    public Task EliminarAsync(int id) => _api.DeleteAsync($"api/Cuentas/{id}");
}
