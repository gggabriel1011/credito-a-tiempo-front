using CreditInTimeFront.WebApp.Modules.CRM.ViewModels;

namespace CreditInTimeFront.WebApp.Modules.CRM.State;

public class CrmState
{
    // ─── Static config ────────────────────────────────────────────────────────

    public static readonly IReadOnlyList<string> TiposContacto =
        ["Llamada", "Visita", "Correo"];

    public static readonly IReadOnlyList<string> TiposSolicitud =
        ["Crédito Personal", "Préstamo Hipotecario", "Línea de Crédito", "Crédito Empresarial", "Refinanciamiento"];

    public static readonly IReadOnlyList<string> Sucursales =
        ["Sede Central", "Santiago", "La Romana", "San Pedro"];

    public static readonly IReadOnlyList<string> Departamentos =
        ["Comercialización", "Créditos", "Banca Empresarial"];

    // ─── Chart data ───────────────────────────────────────────────────────────

    public static readonly double[] AdquisicionMensual =
        [65, 72, 58, 89, 95, 78, 102, 88, 115, 95, 108, 122];

    public static readonly string[] Meses =
        ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"];

    public static readonly double[] SegmentacionCartera = [45, 30, 25];
    public static readonly string[] SegmentacionLabels  = ["Corporativo", "Individual", "PYME"];

    // ─── Date anchors (relative to today, so mock data stays current) ─────────

    private static readonly DateTime _hoy  = DateTime.Today;
    private static readonly DateTime _base = _hoy.AddDays(-60);

    // ─── Mock data ────────────────────────────────────────────────────────────

    public IReadOnlyList<InteraccionViewModel> Interacciones { get; } = LoadMockData();

    // ─── KPIs ────────────────────────────────────────────────────────────────

    public int     TotalClientes     => 1250;
    public int     ProspectosNuevos  => 45;
    public decimal TasaConversion    => 12m;
    public decimal RetencionClientes => 94m;

    // KPI helpers para detalle de cliente (mock hardcoded)
    public int    PrestamosActivos   => 2;
    public string MontoTotal         => "RD$ 2,445,600";
    public int    SolicitudesActivas => 1;
    public string MontoEnTramite     => "RD$ 500,000";

    // ─── Helpers ─────────────────────────────────────────────────────────────

    /// <summary>
    /// Devuelve el cliente por <paramref name="id"/>.
    /// Mock: retorna siempre el cliente demo (CLI-001).
    /// TODO: reemplazar con consulta real al conectar servicios.
    /// </summary>
    public ClienteViewModel GetMockClienteById(string id)
    {
        _ = id; // reservado para implementación real
        return GetMockCliente();
    }

    public ClienteViewModel GetMockCliente() => new()
    {
        Id             = "CLI-001",
        Nombre         = "Ramón Antonio Santos",
        Identificacion = "001-0000001-1",
        Segmento       = SegmentoCliente.Corporativo,
        Email          = "ramon.santos@empresa.com.do",
        Telefono       = "809-555-1001",
        Sucursal       = "Sede Central",
        Departamento   = "Banca Empresarial",
        EsEmpresa      = false,
        FechaRegistro  = new DateTime(2023, 3, 15),
        Direccion      = "Av. Winston Churchill No. 1099, Piantini",
        Provincia      = "Distrito Nacional",
        NombreEmpresa   = "Santos & Asociados, SRL",
        Rnc             = "1-31-00001-2",
        Ocupacion       = "Gerente de Logística",
        Oficial         = "Juan Delgado",
        RangoOficial    = "OFICIAL I",
        Nacionalidad    = "Dominicano",
        EstadoCivil     = "Casado",
        FechaNacimiento = new DateTime(1985, 8, 25)
    };

    public IReadOnlyList<PrestamoViewModel> GetMockPrestamos() =>
    [
        new() { Expediente = "LP-2023-0842", Tipo = "Préstamo Hipotecario", Monto = "RD$ 2,000,000", Saldo = "RD$ 1,845,600", Estado = "Vigente",  FechaApertura = "15/03/2023" },
        new() { Expediente = "LP-2023-0125", Tipo = "Crédito Personal",     Monto = "RD$ 600,000",   Saldo = "RD$ 600,000",   Estado = "En Mora",  FechaApertura = "10/01/2023" },
    ];

    public IReadOnlyList<SolicitudViewModel> GetMockSolicitudes() =>
    [
        new() { Numero = "SOL-2023-4591", Producto = "Crédito Empresarial", Monto = "RD$ 500,000", Fecha = _base.ToString("dd/MM/yyyy"), Etapa = "En Análisis" },
    ];

    // ─── Private loader ───────────────────────────────────────────────────────

    private static List<InteraccionViewModel> LoadMockData() =>
    [
        new()
        {
            Id = 1, ClienteId = "CLI-001", ClienteNombre = "Ramón Antonio Santos",
            Identificacion = "001-0000001-1", TipoSolicitud = "Crédito Personal",
            TipoContacto = TipoContacto.Llamada,
            FechaCreacion = _base,           FechaInicio = _base.AddDays(1),
            FechaEstimada = _base.AddDays(15), FechaRealizado = _base.AddDays(13),
            Sucursal = "Sede Central", Departamento = "Comercialización",
            EjecutivoAsignado = "Ana García", Estado = InteraccionStatus.Completado,
            Seguimientos =
            [
                new() { TipoAccion = "Llamada inicial", TipoRespuesta = "Contestó", Estado = "Exitoso", Resultado = "Interesado en producto", UsuarioAtencion = "Ana García", Departamento = "Comercialización" },
                new() { TipoAccion = "Envío propuesta", TipoRespuesta = "Email confirmado", Estado = "Exitoso", Resultado = "Propuesta aceptada", UsuarioAtencion = "Ana García", Departamento = "Créditos" }
            ]
        },
        new()
        {
            Id = 2, ClienteId = "CLI-002", ClienteNombre = "María Elena Guzmán",
            Identificacion = "002-1234567-8", TipoSolicitud = "Préstamo Hipotecario",
            TipoContacto = TipoContacto.Visita,
            FechaCreacion = _base.AddDays(3),  FechaInicio = _base.AddDays(4),
            FechaEstimada = _base.AddDays(20), FechaRealizado = null,
            Sucursal = "Santiago", Departamento = "Créditos",
            EjecutivoAsignado = "Luis Martínez", Estado = InteraccionStatus.EnProceso,
            Seguimientos =
            [
                new() { TipoAccion = "Visita domicilio", TipoRespuesta = "Presencial", Estado = "En curso", Resultado = "Verificación pendiente", UsuarioAtencion = "Luis Martínez", Departamento = "Créditos" },
                new() { TipoAccion = "Revisión documentos", TipoRespuesta = "Recibidos", Estado = "Pendiente", Resultado = "Documentos incompletos", UsuarioAtencion = "Luis Martínez", Departamento = "Créditos" }
            ]
        },
        new()
        {
            Id = 3, ClienteId = "CLI-003", ClienteNombre = "Constructora Delta, SAS",
            Identificacion = "1-31-44556-2", TipoSolicitud = "Crédito Empresarial",
            TipoContacto = TipoContacto.Correo,
            FechaCreacion = _base.AddDays(5),  FechaInicio = _base.AddDays(6),
            FechaEstimada = _base.AddDays(31), FechaRealizado = null,
            Sucursal = "La Romana", Departamento = "Banca Empresarial",
            EjecutivoAsignado = "Carmen Díaz", Estado = InteraccionStatus.EnProceso,
            Seguimientos =
            [
                new() { TipoAccion = "Correo bienvenida", TipoRespuesta = "Leído", Estado = "Completado", Resultado = "Datos recibidos", UsuarioAtencion = "Carmen Díaz", Departamento = "Banca Empresarial" },
                new() { TipoAccion = "Llamada seguimiento", TipoRespuesta = "No contestó", Estado = "Pendiente", Resultado = "Dejar mensaje", UsuarioAtencion = "Carmen Díaz", Departamento = "Banca Empresarial" },
                new() { TipoAccion = "Segunda llamada", TipoRespuesta = "Contestó", Estado = "En curso", Resultado = "Reunión agendada", UsuarioAtencion = "Carmen Díaz", Departamento = "Banca Empresarial" }
            ]
        },
        new()
        {
            Id = 4, ClienteId = "CLI-004", ClienteNombre = "José Miguel Peña",
            Identificacion = "003-9876543-2", TipoSolicitud = "Línea de Crédito",
            TipoContacto = TipoContacto.Llamada,
            FechaCreacion = _base.AddDays(7),  FechaInicio = _base.AddDays(9),
            FechaEstimada = _base.AddDays(25), FechaRealizado = _base.AddDays(23),
            Sucursal = "Sede Central", Departamento = "Créditos",
            EjecutivoAsignado = "Pedro Ramírez", Estado = InteraccionStatus.Completado,
            Seguimientos =
            [
                new() { TipoAccion = "Consulta inicial", TipoRespuesta = "Atendido", Estado = "Exitoso", Resultado = "Aprobado línea básica", UsuarioAtencion = "Pedro Ramírez", Departamento = "Créditos" }
            ]
        },
        new()
        {
            Id = 5, ClienteId = "CLI-005", ClienteNombre = "Distribuidora Central, SRL",
            Identificacion = "1-32-55667-8", TipoSolicitud = "Refinanciamiento",
            TipoContacto = TipoContacto.Visita,
            FechaCreacion = _base.AddDays(10), FechaInicio = _base.AddDays(11),
            FechaEstimada = _base.AddDays(36), FechaRealizado = null,
            Sucursal = "San Pedro", Departamento = "Comercialización",
            EjecutivoAsignado = "Ana García", Estado = InteraccionStatus.Pendiente,
            Seguimientos =
            [
                new() { TipoAccion = "Primera visita", TipoRespuesta = "Pendiente", Estado = "Pendiente", Resultado = "Sin respuesta", UsuarioAtencion = "Ana García", Departamento = "Comercialización" },
                new() { TipoAccion = "Contacto alternativo", TipoRespuesta = "Email enviado", Estado = "Pendiente", Resultado = "Esperando confirmación", UsuarioAtencion = "Ana García", Departamento = "Comercialización" }
            ]
        },
        new()
        {
            Id = 6, ClienteId = "CLI-006", ClienteNombre = "Ana Patricia Mejía",
            Identificacion = "004-5678901-3", TipoSolicitud = "Crédito Personal",
            TipoContacto = TipoContacto.Correo,
            FechaCreacion = _base.AddDays(13), FechaInicio = _base.AddDays(15),
            FechaEstimada = _base.AddDays(29), FechaRealizado = _base.AddDays(27),
            Sucursal = "Santiago", Departamento = "Comercialización",
            EjecutivoAsignado = "Luis Martínez", Estado = InteraccionStatus.Completado,
            Seguimientos =
            [
                new() { TipoAccion = "Correo propuesta", TipoRespuesta = "Respondido", Estado = "Exitoso", Resultado = "Cliente aceptó", UsuarioAtencion = "Luis Martínez", Departamento = "Comercialización" },
                new() { TipoAccion = "Firma digital", TipoRespuesta = "Completada", Estado = "Exitoso", Resultado = "Documentación completa", UsuarioAtencion = "Luis Martínez", Departamento = "Créditos" }
            ]
        },
        new()
        {
            Id = 7, ClienteId = "CLI-007", ClienteNombre = "Carlos Eduardo Núñez",
            Identificacion = "005-4321098-7", TipoSolicitud = "Préstamo Hipotecario",
            TipoContacto = TipoContacto.Llamada,
            FechaCreacion = _base.AddDays(15), FechaInicio = _base.AddDays(17),
            FechaEstimada = _base.AddDays(41), FechaRealizado = null,
            Sucursal = "La Romana", Departamento = "Créditos",
            EjecutivoAsignado = "Carmen Díaz", Estado = InteraccionStatus.EnProceso,
            Seguimientos =
            [
                new() { TipoAccion = "Precalificación", TipoRespuesta = "Aprobada", Estado = "Completado", Resultado = "Proceder con solicitud", UsuarioAtencion = "Carmen Díaz", Departamento = "Créditos" },
                new() { TipoAccion = "Avalúo propiedad", TipoRespuesta = "En proceso", Estado = "En curso", Resultado = "Esperando informe", UsuarioAtencion = "Carmen Díaz", Departamento = "Créditos" }
            ]
        },
        new()
        {
            Id = 8, ClienteId = "CLI-008", ClienteNombre = "Ferretería El Progreso, SA",
            Identificacion = "1-33-77889-0", TipoSolicitud = "Crédito Empresarial",
            TipoContacto = TipoContacto.Visita,
            FechaCreacion = _base.AddDays(17), FechaInicio = _base.AddDays(19),
            FechaEstimada = _base.AddDays(46), FechaRealizado = null,
            Sucursal = "Sede Central", Departamento = "Banca Empresarial",
            EjecutivoAsignado = "Pedro Ramírez", Estado = InteraccionStatus.Pendiente,
            Seguimientos =
            [
                new() { TipoAccion = "Visita comercial", TipoRespuesta = "Programada", Estado = "Pendiente", Resultado = "Sin confirmar", UsuarioAtencion = "Pedro Ramírez", Departamento = "Banca Empresarial" }
            ]
        },
        new()
        {
            Id = 9, ClienteId = "CLI-009", ClienteNombre = "Luisa Fernanda Castro",
            Identificacion = "006-8765432-1", TipoSolicitud = "Línea de Crédito",
            TipoContacto = TipoContacto.Correo,
            FechaCreacion = _base.AddDays(20), FechaInicio = _base.AddDays(21),
            FechaEstimada = _base.AddDays(34), FechaRealizado = _base.AddDays(33),
            Sucursal = "San Pedro", Departamento = "Créditos",
            EjecutivoAsignado = "Ana García", Estado = InteraccionStatus.Completado,
            Seguimientos =
            [
                new() { TipoAccion = "Solicitud online", TipoRespuesta = "Recibida", Estado = "Exitoso", Resultado = "Línea aprobada", UsuarioAtencion = "Ana García", Departamento = "Créditos" },
                new() { TipoAccion = "Notificación aprobación", TipoRespuesta = "Email enviado", Estado = "Exitoso", Resultado = "Cliente notificado", UsuarioAtencion = "Ana García", Departamento = "Créditos" }
            ]
        },
        new()
        {
            Id = 10, ClienteId = "CLI-010", ClienteNombre = "Agrícola Los Pinos, SRL",
            Identificacion = "1-30-22334-5", TipoSolicitud = "Crédito Empresarial",
            TipoContacto = TipoContacto.Llamada,
            FechaCreacion = _base.AddDays(23), FechaInicio = _base.AddDays(24),
            FechaEstimada = _base.AddDays(51), FechaRealizado = null,
            Sucursal = "Santiago", Departamento = "Banca Empresarial",
            EjecutivoAsignado = "Luis Martínez", Estado = InteraccionStatus.EnProceso,
            Seguimientos =
            [
                new() { TipoAccion = "Reunión gerencial", TipoRespuesta = "Realizada", Estado = "Completado", Resultado = "Interés confirmado", UsuarioAtencion = "Luis Martínez", Departamento = "Banca Empresarial" },
                new() { TipoAccion = "Análisis financiero", TipoRespuesta = "En revisión", Estado = "En curso", Resultado = "Pendiente aprobación", UsuarioAtencion = "Luis Martínez", Departamento = "Créditos" },
                new() { TipoAccion = "Comité de crédito", TipoRespuesta = "Agendado", Estado = "Pendiente", Resultado = "Pendiente de fecha", UsuarioAtencion = "Luis Martínez", Departamento = "Créditos" }
            ]
        },
    ];
}
