using MoneyControlApp.Dtos;

namespace MoneyControlApp.Services;

public class DashboardService
{
    private readonly ApiClient _api;
    public DashboardService(ApiClient api) => _api = api;

    public Task<DashboardDto?> ObtenerAsync() => _api.GetAsync<DashboardDto>("api/Dashboard");
}
