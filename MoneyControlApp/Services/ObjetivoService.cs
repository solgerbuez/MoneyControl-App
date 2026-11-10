using MoneyControlApp.Dtos;

namespace MoneyControlApp.Services;

public class ObjetivoService
{
    private readonly ApiClient _api;
    public ObjetivoService(ApiClient api) => _api = api;

    public async Task<List<ObjetivoDto>> ListarAsync()
        => await _api.GetAsync<List<ObjetivoDto>>("api/Objetivos") ?? new();

    public Task<ObjetivoDetalleDto?> ObtenerAsync(int id)
        => _api.GetAsync<ObjetivoDetalleDto>($"api/Objetivos/{id}");

    public Task<ObjetivoDto?> CrearAsync(ObjetivoGuardarRequest datos)
        => _api.PostAsync<ObjetivoDto>("api/Objetivos", datos);

    public Task EditarAsync(int id, ObjetivoGuardarRequest datos)
        => _api.PutAsync($"api/Objetivos/{id}", datos);

    public Task EliminarAsync(int id) => _api.DeleteAsync($"api/Objetivos/{id}");

    /// <summary>Agrega un aporte: la API suma el monto al objetivo y lo devuelve actualizado.</summary>
    public Task<ObjetivoDto?> AportarAsync(int id, AporteCrearRequest datos)
        => _api.PostAsync<ObjetivoDto>($"api/Objetivos/{id}/aportes", datos);
}
