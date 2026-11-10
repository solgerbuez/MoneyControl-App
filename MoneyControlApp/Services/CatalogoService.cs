using MoneyControlApp.Dtos;

namespace MoneyControlApp.Services;

/// <summary>Listas fijas para los combos (tipos de cuenta, categorias, metodos de pago). Se piden una vez y se guardan.</summary>
public class CatalogoService
{
    private readonly ApiClient _api;
    private List<CatalogoDto>? _tiposCuenta, _categoriasIngreso, _categoriasGasto, _metodosPago;

    public CatalogoService(ApiClient api) => _api = api;

    public async Task<List<CatalogoDto>> TiposCuentaAsync()
        => _tiposCuenta ??= await _api.GetAsync<List<CatalogoDto>>("api/Catalogos/tipos-cuenta") ?? new();

    public async Task<List<CatalogoDto>> CategoriasIngresoAsync()
        => _categoriasIngreso ??= await _api.GetAsync<List<CatalogoDto>>("api/Catalogos/categorias-ingreso") ?? new();

    public async Task<List<CatalogoDto>> CategoriasGastoAsync()
        => _categoriasGasto ??= await _api.GetAsync<List<CatalogoDto>>("api/Catalogos/categorias-gasto") ?? new();

    public async Task<List<CatalogoDto>> MetodosPagoAsync()
        => _metodosPago ??= await _api.GetAsync<List<CatalogoDto>>("api/Catalogos/metodos-pago") ?? new();
}
