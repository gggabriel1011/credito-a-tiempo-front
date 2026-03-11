namespace CreditInTimeFront.WebApp.Modules.CRM.ViewModels;

// ─── Enums ───────────────────────────────────────────────────────────────────

public enum InteraccionStatus { Completado, EnProceso, Pendiente }
public enum TipoContacto      { Llamada = 0, Visita = 1, Correo = 2 }
public enum SegmentoCliente   { Corporativo, Individual, Pyme }

// ─── Extension methods ────────────────────────────────────────────────────────

public static class InteraccionStatusExtensions
{
    public static string ToLabel(this InteraccionStatus s) => s switch
    {
        InteraccionStatus.Completado => "Completado",
        InteraccionStatus.EnProceso  => "En Proceso",
        InteraccionStatus.Pendiente  => "Pendiente",
        _                            => s.ToString()
    };

    public static string ToCssKey(this InteraccionStatus s) => s switch
    {
        InteraccionStatus.Completado => "completado",
        InteraccionStatus.EnProceso  => "en-proceso",
        InteraccionStatus.Pendiente  => "pendiente",
        _                            => s.ToString().ToLower()
    };
}

public static class TipoContactoExtensions
{
    public static string ToLabel(this TipoContacto t) => t switch
    {
        TipoContacto.Llamada => "Llamada",
        TipoContacto.Visita  => "Visita",
        TipoContacto.Correo  => "Correo",
        _                    => t.ToString()
    };
}

public static class SegmentoClienteExtensions
{
    public static string ToLabel(this SegmentoCliente s) => s switch
    {
        SegmentoCliente.Corporativo => "Corporativo",
        SegmentoCliente.Individual  => "Individual",
        SegmentoCliente.Pyme        => "PYME",
        _                           => s.ToString()
    };
}

// ─── POCOs ────────────────────────────────────────────────────────────────────

public class ClienteViewModel
{
    public string Id             { get; set; } = string.Empty;
    public string Nombre         { get; set; } = string.Empty;
    public string Identificacion { get; set; } = string.Empty;
    public SegmentoCliente Segmento { get; set; }
    public string Email          { get; set; } = string.Empty;
    public string Telefono       { get; set; } = string.Empty;
    public string Sucursal       { get; set; } = string.Empty;
    public string Departamento   { get; set; } = string.Empty;
    public bool EsEmpresa        { get; set; }
    public DateTime FechaRegistro { get; set; }
    public string Direccion      { get; set; } = string.Empty;
    public string Provincia      { get; set; } = string.Empty;
    public string NombreEmpresa  { get; set; } = string.Empty;
    public string Rnc            { get; set; } = string.Empty;
    public string Ocupacion      { get; set; } = string.Empty;
    public string Oficial        { get; set; } = string.Empty;
    public string RangoOficial   { get; set; } = string.Empty;
    public string Nacionalidad   { get; set; } = string.Empty;
    public string EstadoCivil    { get; set; } = string.Empty;
    public DateTime? FechaNacimiento { get; set; }
}

public class PrestamoViewModel
{
    public string Expediente    { get; set; } = string.Empty;
    public string Tipo          { get; set; } = string.Empty;
    public string Monto         { get; set; } = string.Empty;
    public string Saldo         { get; set; } = string.Empty;
    public string Estado        { get; set; } = string.Empty;  // "Vigente" | "En Mora" | "Cancelado"
    public string FechaApertura { get; set; } = string.Empty;
}

public class SolicitudViewModel
{
    public string Numero   { get; set; } = string.Empty;
    public string Producto { get; set; } = string.Empty;
    public string Monto    { get; set; } = string.Empty;
    public string Fecha    { get; set; } = string.Empty;
    public string Etapa    { get; set; } = string.Empty;  // "En Análisis" | "Pendiente Docs" | "Aprobado"
}

public class InteraccionViewModel
{
    public int    Id                { get; set; }
    public string ClienteId         { get; set; } = string.Empty;
    public string ClienteNombre     { get; set; } = string.Empty;
    public string Identificacion    { get; set; } = string.Empty;
    public string TipoSolicitud     { get; set; } = string.Empty;
    public TipoContacto TipoContacto { get; set; }
    public DateTime FechaCreacion   { get; set; }
    public DateTime? FechaInicio    { get; set; }
    public DateTime? FechaEstimada  { get; set; }
    public DateTime? FechaRealizado { get; set; }
    public string Sucursal          { get; set; } = string.Empty;
    public string Departamento      { get; set; } = string.Empty;
    public string EjecutivoAsignado { get; set; } = string.Empty;
    public InteraccionStatus Estado { get; set; }
    public List<SeguimientoViewModel> Seguimientos { get; set; } = [];
}

public class SeguimientoViewModel
{
    public string TipoAccion      { get; set; } = string.Empty;
    public string TipoRespuesta   { get; set; } = string.Empty;
    public string Estado          { get; set; } = string.Empty;
    public string Resultado       { get; set; } = string.Empty;
    public string UsuarioAtencion { get; set; } = string.Empty;
    public string Departamento    { get; set; } = string.Empty;
}
