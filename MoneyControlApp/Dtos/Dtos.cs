namespace MoneyControlApp.Dtos;


public class LoginRequest
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}

public class RegistroRequest
{
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string? Telefono { get; set; }
}

public class LoginResponse
{
    public string Token { get; set; } = "";
    public DateTime Expira { get; set; }
    public int IdUsuario { get; set; }
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Email { get; set; } = "";
    public string Rol { get; set; } = "";
}


public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}

public class CatalogoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string? Icono { get; set; }
    public string? Color { get; set; }
}


public class CuentaDto
{
    public int IdCuenta { get; set; }
    public string NombreCuenta { get; set; } = "";
    public string? UltimosDigitos { get; set; }
    public decimal Saldo { get; set; }
    public int IdTipoCuenta { get; set; }
    public string TipoCuenta { get; set; } = "";
}


public class CuentaGuardarRequest
{
    public string NombreCuenta { get; set; } = "";
    public string? UltimosDigitos { get; set; }
    public decimal SaldoInicial { get; set; }
    public int IdTipoCuenta { get; set; }
}


public class MovimientoDto
{
    public int Id { get; set; }
    public string Tipo { get; set; } = "";      
    public decimal Monto { get; set; }
    public DateTime Fecha { get; set; }
    public string? Descripcion { get; set; }
    public int IdCuenta { get; set; }
    public string Cuenta { get; set; } = "";
    public string Categoria { get; set; } = "";
    public string? Icono { get; set; }
    public string? Color { get; set; }
    public string? MetodoPago { get; set; }
}

public class IngresoCrearRequest
{
    public decimal Monto { get; set; }
    public DateTime? Fecha { get; set; }
    public string? Descripcion { get; set; }
    public int IdCuenta { get; set; }
    public int IdCategoriaIngreso { get; set; }
}

public class GastoCrearRequest
{
    public decimal Monto { get; set; }
    public DateTime? Fecha { get; set; }
    public string? Descripcion { get; set; }
    public int IdCuenta { get; set; }
    public int IdCategoriaGasto { get; set; }
    public int IdMetodoPago { get; set; }
}

// ---------- Presupuestos ----------
public class PresupuestoDto
{
    public int IdPresupuesto { get; set; }
    public int IdCategoriaGasto { get; set; }
    public string Categoria { get; set; } = "";
    public string? Icono { get; set; }
    public string? Color { get; set; }
    public decimal MontoLimite { get; set; }
    public decimal Gastado { get; set; }
    public decimal Disponible { get; set; }
    public int Porcentaje { get; set; }
    public bool AlertaLimite { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
}

public class PresupuestoGuardarRequest
{
    public int IdCategoriaGasto { get; set; }
    public decimal MontoLimite { get; set; }
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin { get; set; }
}

// ---------- Objetivos de ahorro ----------
public class ObjetivoDto
{
    public int IdObjetivoAhorro { get; set; }
    public string Nombre { get; set; } = "";
    public decimal MontoObjetivo { get; set; }
    public decimal MontoActual { get; set; }
    public decimal Faltante { get; set; }
    public int Porcentaje { get; set; }
    public DateTime? FechaLimite { get; set; }
    public string? Icono { get; set; }
    public string? Color { get; set; }
}

public class AporteDto
{
    public int IdAporteObjetivo { get; set; }
    public decimal Monto { get; set; }
    public DateTime Fecha { get; set; }
}

public class ObjetivoDetalleDto : ObjetivoDto
{
    public List<AporteDto> Aportes { get; set; } = new();
}

public class ObjetivoGuardarRequest
{
    public string Nombre { get; set; } = "";
    public decimal MontoObjetivo { get; set; }
    public DateTime? FechaLimite { get; set; }
    public string? Icono { get; set; }
    public string? Color { get; set; }
}

public class AporteCrearRequest
{
    public decimal Monto { get; set; }
    public DateTime? Fecha { get; set; }
}

// ---------- Notificaciones ----------
public class NotificacionDto
{
    public int IdNotificacion { get; set; }
    public string Tipo { get; set; } = "";
    public string Mensaje { get; set; } = "";
    public DateTime Fecha { get; set; }
    public bool Leida { get; set; }
}

public class NotificacionesRespuesta
{
    public int NoLeidas { get; set; }
    public PagedResult<NotificacionDto> Notificaciones { get; set; } = new();
}

// ---------- Dashboard ----------
public class GastoCategoriaDto
{
    public string Categoria { get; set; } = "";
    public string? Color { get; set; }
    public decimal Total { get; set; }
    public int Porcentaje { get; set; }
}

public class DashboardDto
{
    public string NombreUsuario { get; set; } = "";
    public decimal BalanceTotal { get; set; }
    public decimal IngresosMes { get; set; }
    public decimal GastosMes { get; set; }
    public decimal BalanceMes { get; set; }
    public int NotificacionesNoLeidas { get; set; }
    public List<CuentaDto> Cuentas { get; set; } = new();
    public List<MovimientoDto> UltimosMovimientos { get; set; } = new();
    public List<GastoCategoriaDto> GastosPorCategoria { get; set; } = new();
}

// ---------- Administracion de usuarios ----------
public class UsuarioListaDto
{
    public int IdUsuario { get; set; }
    public string Nombre { get; set; } = "";
    public string Apellido { get; set; } = "";
    public string Email { get; set; } = "";
    public string Rol { get; set; } = "";
    public bool Estado { get; set; }
    public DateTime FechaRegistro { get; set; }
    public DateTime? UltimoAcceso { get; set; }
}

public class UsuariosRespuesta
{
    public int Total { get; set; }
    public int Activos { get; set; }
    public int Inactivos { get; set; }
    public PagedResult<UsuarioListaDto> Usuarios { get; set; } = new();
}

public class CambiarEstadoRequest
{
    public bool Estado { get; set; }
}
