using System.Globalization;

namespace MoneyControlApp.Config;

/// <summary>Formato de montos y fechas en estilo argentino ($ 1.234,56 y dd/MM/yyyy).</summary>
public static class Formato
{
    public static readonly CultureInfo Cultura = new("es-AR");

    public static string Moneda(decimal monto) => "$ " + monto.ToString("N2", Cultura);

    public static string Fecha(DateTime fecha) => fecha.ToString("dd/MM/yyyy", Cultura);

    public static string FechaHora(DateTime fecha) => fecha.ToString("dd/MM/yyyy HH:mm", Cultura);

    public static string Fecha(DateTime? fecha) => fecha.HasValue ? Fecha(fecha.Value) : "-";
}
