namespace CreditInTimeFront.WebApp.Modules.CollectionManagement.ViewModels;

// ─── Enums ────────────────────────────────────────────────────────────────────

public enum CobroEstado        { EnGestion, EnGestionCritica, Normalizado, Judicial }
public enum TipoAccionCobro    { Llamada, Visita, NotificacionJudicial, MensajeDirecto }
public enum ResultadoGestion   { ContactoEfectivo, NoContesta, PromesaPago, Ilocalizable, RechazoPago }
public enum TipoSeguimiento    { LlamadaCobro, VisitaDomiciliaria, NotificacionEscrita, SeguimientoJudicial }
public enum PrioridadSeguimiento { Baja, Media, Alta }

// ─── Extension methods ────────────────────────────────────────────────────────

public static class CobroEstadoExtensions
{
    public static string ToLabel(this CobroEstado e) => e switch
    {
        CobroEstado.EnGestion        => "En Gestión",
        CobroEstado.EnGestionCritica => "En Gestión Crítica",
        CobroEstado.Normalizado      => "Normalizado",
        CobroEstado.Judicial         => "Judicial",
        _                            => e.ToString()
    };

    public static string ToCssKey(this CobroEstado e) => e switch
    {
        CobroEstado.EnGestion        => "en-gestion",
        CobroEstado.EnGestionCritica => "critico",
        CobroEstado.Normalizado      => "normalizado",
        CobroEstado.Judicial         => "Judicial",
        _                            => e.ToString().ToLower()
    };
}

public static class TipoAccionCobroExtensions
{
    public static string ToLabel(this TipoAccionCobro t) => t switch
    {
        TipoAccionCobro.Llamada           => "Llamada",
        TipoAccionCobro.Visita            => "Visita",
        TipoAccionCobro.NotificacionJudicial => "Notificación Judicial",
        TipoAccionCobro.MensajeDirecto    => "Mensaje Directo",
        _                                 => t.ToString()
    };
}

public static class ResultadoGestionExtensions
{
    public static string ToLabel(this ResultadoGestion r) => r switch
    {
        ResultadoGestion.ContactoEfectivo => "Contacto Efectivo",
        ResultadoGestion.NoContesta       => "No Contesta",
        ResultadoGestion.PromesaPago      => "Promesa de Pago",
        ResultadoGestion.Ilocalizable     => "Ilocalizable",
        ResultadoGestion.RechazoPago      => "Rechazo de Pago",
        _                                 => r.ToString()
    };
}

public static class TipoSeguimientoExtensions
{
    public static string ToLabel(this TipoSeguimiento t) => t switch
    {
        TipoSeguimiento.LlamadaCobro       => "Llamada de Cobro",
        TipoSeguimiento.VisitaDomiciliaria => "Visita Domiciliaria",
        TipoSeguimiento.NotificacionEscrita => "Notificación Escrita",
        TipoSeguimiento.SeguimientoJudicial   => "Seguimiento Judicial",
        _                                  => t.ToString()
    };
}

public static class PrioridadSeguimientoExtensions
{
    public static string ToLabel(this PrioridadSeguimiento p) => p switch
    {
        PrioridadSeguimiento.Baja  => "Baja",
        PrioridadSeguimiento.Media => "Media",
        PrioridadSeguimiento.Alta  => "Alta / Urgente",
        _                          => p.ToString()
    };
}

// ─── POCOs ────────────────────────────────────────────────────────────────────

public class CobroCasoViewModel
{
    public string      ClienteId           { get; set; } = string.Empty;
    public string      ClienteNombre       { get; set; } = string.Empty;
    public string      TipoCredito         { get; set; } = string.Empty;
    public bool        EsEmpresa           { get; set; }
    public int         DiasAtraso          { get; set; }
    public decimal     Monto               { get; set; }
    public string      UltimaGestion       { get; set; } = string.Empty;
    public string      TipoUltimaGestion   { get; set; } = string.Empty;
    public string      UltimaGestionTiempo { get; set; } = string.Empty;
    public CobroEstado Estado              { get; set; }
}

public class GestionHistorialViewModel
{
    public string   TipoAccion   { get; set; } = string.Empty;
    public string   Icono        { get; set; } = string.Empty;
    public bool     EsAlerta     { get; set; }
    public string   Descripcion  { get; set; } = string.Empty;
    public string   GestorNombre { get; set; } = string.Empty;
    public bool     EsSistema    { get; set; }
    public string   TiempoLabel  { get; set; } = string.Empty;
}

// ─── Shared UI record ─────────────────────────────────────────────────────────

public record KpiCard(string Label, string Value, string Icon, string BadgeVariant, string Detail);
