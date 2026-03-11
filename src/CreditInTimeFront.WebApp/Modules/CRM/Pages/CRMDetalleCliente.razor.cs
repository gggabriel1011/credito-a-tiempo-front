using System.Globalization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using CreditInTimeFront.WebApp.Modules.CRM.State;
using CreditInTimeFront.WebApp.Modules.CRM.ViewModels;

namespace CreditInTimeFront.WebApp.Modules.CRM.Pages;

public partial class CRMDetalleCliente
{
    [Parameter] public string ClienteId { get; set; } = string.Empty;

    [Inject] private NavigationManager NavManager { get; set; } = default!;

    // TODO: reemplazar con [Inject] private CrmState _state { get; set; } al conectar servicios reales
    private readonly CrmState _state = new();

    private ClienteViewModel? _cliente;
    private int _activeTab = 0;

    private IReadOnlyList<PrestamoViewModel>    _prestamos     = [];
    private IReadOnlyList<SolicitudViewModel>   _solicitudes   = [];
    private IReadOnlyList<InteraccionViewModel> _interacciones = [];

    protected override void OnParametersSet()
    {
        _cliente       = _state.GetMockClienteById(ClienteId);
        _prestamos     = _state.GetMockPrestamos();
        _solicitudes   = _state.GetMockSolicitudes();
        _interacciones = _state.Interacciones
            .Where(i => i.ClienteId == _cliente!.Id)
            .ToList();
    }

    private static Color GetPrestamoEstadoColor(string estado) => estado switch
    {
        "Vigente"   => Color.Success,
        "En Mora"   => Color.Error,
        "Cancelado" => Color.Default,
        _           => Color.Default
    };

    private static Color GetEtapaColor(string etapa) => etapa switch
    {
        "Aprobado"       => Color.Success,
        "En Análisis"    => Color.Info,
        "Pendiente Docs" => Color.Warning,
        _                => Color.Default
    };

    private static Color GetInteraccionEstadoColor(InteraccionStatus estado) => estado switch
    {
        InteraccionStatus.Completado => Color.Success,
        InteraccionStatus.EnProceso  => Color.Info,
        InteraccionStatus.Pendiente  => Color.Warning,
        _                            => Color.Default
    };

    private string FechaNacimientoLabel
    {
        get
        {
            if (_cliente?.FechaNacimiento is not { } f) return "—";
            var cultura = new CultureInfo("es-ES");
            int edad    = (int)((DateTime.Today - f).TotalDays / 365.25);
            string mes  = cultura.DateTimeFormat.MonthNames[f.Month - 1];
            return $"{f.Day} de {mes}, {f.Year} ({edad} años)";
        }
    }

    private string UltimoContacto => _interacciones
        .Where(i => i.FechaRealizado.HasValue)
        .OrderByDescending(i => i.FechaRealizado)
        .Select(i => i.FechaRealizado!.Value.ToString("dd/MM/yyyy"))
        .FirstOrDefault() ?? "—";

    private string CanalPreferido => _interacciones
        .GroupBy(i => i.TipoContacto)
        .OrderByDescending(g => g.Count())
        .Select(g => g.Key.ToLabel())
        .FirstOrDefault() ?? "—";

    private void Cerrar() => NavManager.NavigateTo("/crm");
}
