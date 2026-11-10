using Microsoft.Extensions.Logging;
using MoneyControlApp.Config;
using MoneyControlApp.Services;
using MudBlazor.Services;

namespace MoneyControlApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices(config =>
            {
                
                config.SnackbarConfiguration.PositionClass = MudBlazor.Defaults.Classes.Position.BottomCenter;
                config.SnackbarConfiguration.SnackbarVariant = MudBlazor.Variant.Filled;
                config.SnackbarConfiguration.VisibleStateDuration = 3500;
                config.SnackbarConfiguration.ShowCloseIcon = true;
            });   // servicios de MudBlazor (Snackbar, Dialog, etc.)

            // Un unico HttpClient apuntando a la API publicada (BaseAddress termina en "/")
            builder.Services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri(ApiConfig.BaseUrl),
                Timeout = TimeSpan.FromSeconds(30)
            });

            // Orden de dependencias: AuthService -> ApiClient -> servicios de cada pantalla
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<ApiClient>();
            builder.Services.AddSingleton<CatalogoService>();
            builder.Services.AddSingleton<CuentaService>();
            builder.Services.AddSingleton<MovimientoService>();
            builder.Services.AddSingleton<PresupuestoService>();
            builder.Services.AddSingleton<ObjetivoService>();
            builder.Services.AddSingleton<NotificacionService>();
            builder.Services.AddSingleton<DashboardService>();
            builder.Services.AddSingleton<UsuarioService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
