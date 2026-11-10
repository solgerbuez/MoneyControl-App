using MoneyControlApp.Dtos;

namespace MoneyControlApp.Services;

public class PresupuestoService
{
    private readonly ApiClient _api;
    public PresupuestoService(ApiClient api) => _api = api;

    public async Task<List<PresupuestoDto>> ListarAsync(bool soloVigentes = true)
        => await _api.GetAsync<List<PresupuestoDto>>($"api/Presupuestos?soloVigentes={(soloVigentes ? "true" : "false")}") ?? new();

    public Task<PresupuestoDto?> CrearAsync(PresupuestoGuardarRequest datos)
        => _api.PostAsync<PresupuestoDto>("api/Presupuestos", datos);

    public Task EditarAsync(int id, PresupuestoGuardarRequest datos)
        => _api.PutAsync($"api/Presupuestos/{id}", datos);

    public Task EliminarAsync(int id) => _api.DeleteAsync($"api/Presupuestos/{id}");
}
