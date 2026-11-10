using MudBlazor;

namespace MoneyControlApp.Config;


public static class Iconos
{
    public static string Categoria(string? nombre) => nombre switch
    {
        "Alimentación" => Icons.Material.Filled.Restaurant,
        "Transporte" => Icons.Material.Filled.DirectionsBus,
        "Servicios" => Icons.Material.Filled.Receipt,
        "Entretenimiento" => Icons.Material.Filled.Movie,
        "Salud" => Icons.Material.Filled.LocalHospital,
        "Educación" => Icons.Material.Filled.School,
        "Hogar" => Icons.Material.Filled.Home,
        "Sueldo" => Icons.Material.Filled.AttachMoney,
        "Trabajo freelance" => Icons.Material.Filled.Work,
        "Inversiones" => Icons.Material.Filled.TrendingUp,
        "Regalo" => Icons.Material.Filled.CardGiftcard,
        _ => Icons.Material.Filled.MoreHoriz
    };

    
    public static string Iniciales(string? nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre)) return "?";
        var limpio = nombre.Trim();
        return (limpio.Length >= 2 ? limpio[..2] : limpio).ToUpperInvariant();
    }

   
    public static string TonoCategoria(string? nombre, bool esIngreso = false)
    {
        if (esIngreso) return nombre == "Regalo" ? "ambar" : "verde";
        return nombre switch
        {
            "Alimentación" or "Salud" => "rojo",
            "Transporte" or "Entretenimiento" => "ambar",
            _ => "verde"
        };
    }

    
    public static string TonoCuenta(string? tipo) => tipo == "Efectivo" ? "ambar" : "verde";
}
