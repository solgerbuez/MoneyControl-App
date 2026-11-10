# MoneyControl – App móvil

App móvil de finanzas personales hecha con **.NET MAUI Blazor Hybrid (.NET 10) + MudBlazor**.
Consume la API publicada en `https://moneycontrol.runasp.net/` (ASP.NET Core + SQL Server en MonsterASP).

## Pantallas
| Pantalla | Ruta | Quién |
|---|---|---|
| Iniciar sesión / Crear cuenta | `/login`, `/registro` | público |
| Inicio (saldo, resumen del mes, últimos movimientos) | `/` | usuario |
| Cuentas (alta, edición, baja) | `/cuentas` | usuario |
| Ingresos y gastos (listado, baja) | `/movimientos` | usuario |
| Nuevo ingreso / nuevo gasto | `/movimientos/nuevo-ingreso`, `/movimientos/nuevo-gasto` | usuario |
| Presupuestos (alerta al 90 %) | `/presupuestos` | usuario |
| Objetivos de ahorro y aportes | `/objetivos`, `/objetivos/{id}` | usuario |
| Notificaciones | `/notificaciones` | usuario |
| Administración de usuarios (buscar, activar, desactivar) | `/usuarios` | administrador |

## Organización
```
Components/Pages/*.razor  ->  Services/*Service.cs  ->  Services/ApiClient.cs  ->  API (HTTP + JWT)
                                      ^                         ^
                                 Dtos/Dtos.cs            AuthService (token en SecureStorage)
```
- `Config/ApiConfig.cs`: URL de la API (debe terminar en `/`).
- `Services/ApiClient.cs`: único lugar que hace HTTP; agrega el token y traduce los errores de la API.
- `Services/AuthService.cs`: login, registro y logout; guarda el token.
- `Components/Layout`: menú lateral, barra superior con campanita y barra inferior.

## Cómo ejecutarla
Requiere **Visual Studio 2026** con la carga de trabajo *.NET MAUI*.
1. Abrir `MoneyControlApp.slnx`.
2. Elegir el emulador de Android en la barra superior y presionar ▶.
