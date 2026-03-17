using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using CreditInTimeFront.WebApp.Modules.CreditFactory.State;
using CreditInTimeFront.WebApp.Modules.CreditFactory.ViewModels;

namespace CreditInTimeFront.WebApp.Modules.CreditFactory.Pages;

public partial class CreditFactory
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    // TODO: reemplazar con [Inject] private CreditFactoryState _state { get; set; } al conectar servicios reales
    private readonly CreditFactoryState _state = new();
    private bool _modalOpen;
    private CreditRequestViewModel? _selectedRequest;
    private List<KpiCard> _kpiCards = [];

    protected override void OnInitialized() => BuildKpiCards();

    private void BuildKpiCards()
    {
        _kpiCards =
        [
            new("Pendientes",           _state.TotalPending.ToString(),   Icons.Material.Outlined.PendingActions, "warning", "Requieren atención inmediata"),
            new("En Análisis",          _state.InAnalysis.ToString(),     Icons.Material.Outlined.Analytics,     "info",    "En proceso de evaluación"),
            new("Aprobadas Hoy",        _state.ApprovedToday.ToString(),  Icons.Material.Outlined.CheckCircle,   "primary", "Meta diaria alcanzada"),
            new("Monto Total en Turno", FormatAmount(_state.TotalAmount), Icons.Material.Outlined.Payments,      "muted",   "Valor total acumulado"),
        ];
    }

    private void HandlePrintRequest(CreditRequestViewModel request)
    {
        _selectedRequest = request;
        _modalOpen       = true;
    }

    private void CloseModal()
    {
        _modalOpen       = false;
        _selectedRequest = null;
    }

    private async Task OpenReport(string url)
    {
        await JsRuntime.InvokeVoidAsync("window.open", url);
    }

    private static string FormatAmount(decimal amount) =>
        amount >= 1_000_000
            ? $"RD$ {amount / 1_000_000:N1}M"
            : $"RD$ {amount:N0}";

    private record KpiCard(string Label, string Value, string Icon, string BadgeVariant, string Detail);
}
