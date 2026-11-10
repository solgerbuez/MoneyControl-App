using MoneyControlApp.Dtos;

namespace MoneyControlApp.Services;

/// <summary>Ingresos y gastos. Registrar o eliminar uno actualiza el saldo de la cuenta (lo hace la API).</summary>
public class MovimientoService
{
    private readonly ApiClient _api;
    public MovimientoService(ApiClient api) => _api = api;

    public async Task<PagedResult<MovimientoDto>> IngresosAsync(int page, int pageSize, int? idCuenta = null)
        => await _api.GetAsync<PagedResult<MovimientoDto>>(Url("api/Ingresos", page, pageSize, idCuenta)) ?? new();

    public async Task<PagedResult<MovimientoDto>> GastosAsync(int page, int pageSize, int? idCuenta = null)
        => await _api.GetAsync<PagedResult<MovimientoDto>>(Url("api/Gastos", page, pageSize, idCuenta)) ?? new();

    public Task<MovimientoDto?> CrearIngresoAsync(IngresoCrearRequest datos)
        => _api.PostAsync<MovimientoDto>("api/Ingresos", datos);

    public Task<MovimientoDto?> CrearGastoAsync(GastoCrearRequest datos)
        => _api.PostAsync<MovimientoDto>("api/Gastos", datos);

    public Task EliminarIngresoAsync(int id) => _api.DeleteAsync($"api/Ingresos/{id}");

    public Task EliminarGastoAsync(int id) => _api.DeleteAsync($"api/Gastos/{id}");

    private static string Url(string ruta, int page, int pageSize, int? idCuenta)
    {
        var url = $"{ruta}?page={page}&pageSize={pageSize}";
        if (idCuenta.HasValue && idCuenta.Value > 0) url += $"&idCuenta={idCuenta.Value}";
        return url;
    }
}
