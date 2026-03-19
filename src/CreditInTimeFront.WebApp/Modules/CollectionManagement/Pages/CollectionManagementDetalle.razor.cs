using Microsoft.AspNetCore.Components;
using MudBlazor;
using CreditInTimeFront.WebApp.Modules.CollectionManagement.State;
using CreditInTimeFront.WebApp.Modules.CollectionManagement.ViewModels;

namespace CreditInTimeFront.WebApp.Modules.CollectionManagement.Pages;

public partial class CollectionManagementDetalle
{
    [Parameter] public string ClienteId { get; set; } = string.Empty;

    [Inject] private NavigationManager NavManager { get; set; } = default!;

    // TODO: reemplazar con [Inject] private CollectionManagementState _state { get; set; } al conectar servicios reales
    private readonly CollectionManagementState _state = new();

    private CobroCasoViewModel?                    _caso;
    private IReadOnlyList<GestionHistorialViewModel> _historial = [];
    private List<KpiCard>                          _kpiCards  = [];
    private string                                 _subtitleDetalle = string.Empty;

    // ── Form state ────────────────────────────────────────────────────────────
    private TipoAccionCobro  _tipoAccion  = TipoAccionCobro.Llamada;
    private ResultadoGestion _resultado   = ResultadoGestion.ContactoEfectivo;
    private string           _comentarios = string.Empty;

    protected override void OnParametersSet()
    {
        _caso      = _state.Casos.FirstOrDefault(c => c.ClienteId == ClienteId)
                     ?? _state.Casos[0]; // fallback al primer caso en mock
        _historial = _state.GetHistorialMockData();

        _subtitleDetalle = $"ID Cliente: {_caso.ClienteId} • {_caso.TipoCredito}";

        BuildKpiCards();
    }

    private void BuildKpiCards()
    {
        _kpiCards =
        [
            new("Monto en Mora",    CollectionManagementState.FormatMonto(_caso!.Monto), Icons.Material.Outlined.AccountBalanceWallet, "negative", string.Empty),
            new("Días de Atraso",   $"{_caso.DiasAtraso} Días",                          Icons.Material.Outlined.AccessTime,           "warning",  _caso.DiasAtraso > 90 ? "Crítico" : "En seguimiento"),
            new("Estatus de Cobro", _caso.Estado.ToLabel(),                              Icons.Material.Outlined.Warning,              GetEstadoBadgeVariant(_caso.Estado), string.Empty),
        ];
    }

    private static string GetEstadoBadgeVariant(CobroEstado estado) => estado switch
    {
        CobroEstado.EnGestionCritica or CobroEstado.Judicial => "warning",
        CobroEstado.Normalizado                              => "primary",
        _                                                    => "info"
    };

    private void GuardarGestion()
    {
        // TODO: reemplazar con llamada a servicio real al conectar backend
        _comentarios = string.Empty;
        NavManager.NavigateTo("/collection-management");
    }
}
