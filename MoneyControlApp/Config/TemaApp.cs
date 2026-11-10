using MudBlazor;

namespace MoneyControlApp.Config;

/// <summary>
/// Tema de MudBlazor para toda la app (se aplica en MainLayout).
/// Paleta del diseno: verdes como base, rojo para gastos/alertas y ambar para precauciones.
/// </summary>
public static class TemaApp
{
    public const string Verde = "#3C8B68";
    public const string VerdeOscuro = "#103B31";

    public static readonly MudTheme Principal = new()
    {
        PaletteLight = new PaletteLight
        {
            Primary = Verde,
            Secondary = "#718078",
            Tertiary = "#A96413",
            Info = "#3C8B68",
            Success = "#287553",
            Warning = "#D98A22",
            Error = "#A7463C",
            AppbarBackground = VerdeOscuro,
            AppbarText = "#FFFEFA",
            Background = "#F5F7F1",
            Surface = "#FFFEFA",
            DrawerBackground = "#FFFEFA",
            DrawerText = "#24372E",
            DrawerIcon = "#3C8B68",
            TextPrimary = "#24372E",
            TextSecondary = "#718078",
            ActionDefault = "#4B5F54",
            LinesDefault = "#DCE9DF",
            LinesInputs = "#C9DACE"
        },
        LayoutProperties = new LayoutProperties
        {
            DefaultBorderRadius = "14px",
            DrawerWidthLeft = "270px"
        }
    };
}
