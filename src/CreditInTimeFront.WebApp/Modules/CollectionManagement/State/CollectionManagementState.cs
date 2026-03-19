using MudBlazor;
using CreditInTimeFront.WebApp.Modules.CollectionManagement.ViewModels;

namespace CreditInTimeFront.WebApp.Modules.CollectionManagement.State;

public class CollectionManagementState
{
    // ─── Static config ────────────────────────────────────────────────────────

    public static readonly IReadOnlyList<string> TiposAccion =
        ["Llamada", "Visita", "Notificación Judicial", "Mensaje Directo"];

    public static readonly IReadOnlyList<string> Resultados =
        ["Contacto Efectivo", "No Contesta", "Promesa de Pago", "Ilocalizable", "Rechazo de Pago"];

    public static readonly IReadOnlyList<string> TiposSeguimiento =
        ["Llamada de Cobro", "Visita Domiciliaria", "Notificación Escrita", "Seguimiento Judicial"];

    // ─── Chart data ───────────────────────────────────────────────────────────

    public static readonly double[] RecuperacionMensual =
        [1200, 1850, 1500, 2100, 1750, 2300, 1900, 2450, 2100, 2800, 2350, 2650];

    public static readonly string[] Meses =
        ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"];

    public static readonly double[] DistribucionAntiguedad = [40, 25, 20, 15];
    public static readonly string[] DistribucionLabels     = ["0-30 días", "31-60 días", "61-90 días", "90+ días"];

    // ─── Mock data ────────────────────────────────────────────────────────────

    public IReadOnlyList<CobroCasoViewModel> Casos { get; } = LoadMockData();

    // ─── KPIs ─────────────────────────────────────────────────────────────────

    public string MontoEnMora      => FormatMonto(Casos.Sum(c => c.Monto));
    public string IndiceMorosidad  => "4.8%";
    public string PromesasPagoHoy  => "RD$ 2.4M";
    public int    CasosCriticos    => Casos.Count(c => c.DiasAtraso > 90);
    public int    PromesasCantidad => 18;

    // ─── Helpers ──────────────────────────────────────────────────────────────

    /// <summary>
    /// Retorna el historial de gestiones mock.
    /// TODO: reemplazar con consulta real por clienteId al conectar servicios.
    /// </summary>
    public IReadOnlyList<GestionHistorialViewModel> GetHistorialMockData() =>
    [
        new()
        {
            TipoAccion   = "Llamada Fallida",
            Icono        = Icons.Material.Outlined.PhoneMissed,
            EsAlerta     = true,
            Descripcion  = "El cliente no contestó la llamada al número registrado. Se dejó mensaje de voz.",
            GestorNombre = "Gabriel Almonte",
            EsSistema    = false,
            TiempoLabel  = "Hace 2 horas"
        },
        new()
        {
            TipoAccion   = "Visita de Campo",
            Icono        = Icons.Material.Outlined.HomeWork,
            EsAlerta     = false,
            Descripcion  = "Visita presencial a la propiedad. Se conversó con el encargado, quien indica que el dueño estará de viaje hasta el fin de semana.",
            GestorNombre = "Gabriel Almonte",
            EsSistema    = false,
            TiempoLabel  = "12 Oct, 2023"
        },
        new()
        {
            TipoAccion   = "Promesa de Pago Incumplida",
            Icono        = Icons.Material.Outlined.EventBusy,
            EsAlerta     = true,
            Descripcion  = "Vencimiento de acuerdo de pago por RD$ 50,000. No se registró el depósito en cuenta colectora.",
            GestorNombre = "Notificación Automática",
            EsSistema    = true,
            TiempoLabel  = "05 Oct, 2023"
        },
    ];

    public static string FormatMonto(decimal amount) =>
        amount >= 1_000_000
            ? $"RD$ {amount / 1_000_000:N1}M"
            : $"RD$ {amount:N0}";

    // ─── Private loader ───────────────────────────────────────────────────────

    private static List<CobroCasoViewModel> LoadMockData() =>
    [
        new()
        {
            ClienteId = "COB-001", ClienteNombre = "Agrícola Los Pinos, S.A.",
            TipoCredito = "Préstamo Agropecuario", EsEmpresa = true,
            DiasAtraso = 105, Monto = 450200m,
            UltimaGestion = "Llamada Fallida", TipoUltimaGestion = "Llamada",
            UltimaGestionTiempo = "Hace 2 horas", Estado = CobroEstado.EnGestionCritica
        },
        new()
        {
            ClienteId = "COB-002", ClienteNombre = "Constructora del Caribe, SRL",
            TipoCredito = "Crédito Empresarial", EsEmpresa = true,
            DiasAtraso = 180, Monto = 3450000m,
            UltimaGestion = "Judicial: Notificación Enviada", TipoUltimaGestion = "Judicial",
            UltimaGestionTiempo = "Hace 1 semana", Estado = CobroEstado.Judicial
        },
        new()
        {
            ClienteId = "COB-003", ClienteNombre = "Supermercado El Valle",
            TipoCredito = "Línea de Crédito", EsEmpresa = true,
            DiasAtraso = 5, Monto = 125000m,
            UltimaGestion = "Recordatorio SMS", TipoUltimaGestion = "Correo",
            UltimaGestionTiempo = "Hoy 8:00 AM", Estado = CobroEstado.EnGestion
        },
        new()
        {
            ClienteId = "COB-004", ClienteNombre = "María Altagracia Espinal",
            TipoCredito = "Crédito Personal", EsEmpresa = false,
            DiasAtraso = 62, Monto = 45800m,
            UltimaGestion = "Visita Domiciliaria", TipoUltimaGestion = "Visita",
            UltimaGestionTiempo = "Ayer 4:30 PM", Estado = CobroEstado.EnGestion
        },
        new()
        {
            ClienteId = "COB-005", ClienteNombre = "Transporte Jiménez Hermanos",
            TipoCredito = "Préstamo Comercial", EsEmpresa = true,
            DiasAtraso = 112, Monto = 890000m,
            UltimaGestion = "Incautación Pendiente", TipoUltimaGestion = "Judicial",
            UltimaGestionTiempo = "Hace 3 horas", Estado = CobroEstado.Judicial
        },
        new()
        {
            ClienteId = "COB-006", ClienteNombre = "Farmacia San Judas",
            TipoCredito = "Crédito Empresarial", EsEmpresa = true,
            DiasAtraso = 38, Monto = 210000m,
            UltimaGestion = "Promesa Incumplida", TipoUltimaGestion = "Promesa de Pago",
            UltimaGestionTiempo = "Hace 2 días", Estado = CobroEstado.EnGestion
        },
    ];
}
