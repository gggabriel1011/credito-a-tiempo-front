using Microsoft.AspNetCore.Components;
using MudBlazor;
using CreditInTimeFront.WebApp.Modules.CRM.State;
using CreditInTimeFront.WebApp.Modules.CRM.ViewModels;
using CreditInTimeFront.WebApp.Shared.Themes;

namespace CreditInTimeFront.WebApp.Modules.CRM.Pages;

public partial class CRM
{
    [Inject] private NavigationManager NavManager { get; set; } = default!;

    // TODO: reemplazar con [Inject] private CrmState _state { get; set; } al conectar servicios reales
    private readonly CrmState _state = new();

    private List<KpiCard> _kpiCards = [];

    private List<ChartSeries<double>> _lineChartSeries    = [];
    private List<ChartSeries<double>> _donutSeries        = [];
    private ChartOptions             _lineChartOptions    = new();
    private ChartOptions             _donutChartOptions   = new();
    private string[]                 _donutPalette        = [];
    private string[]                 _donutLabels         = [];

    protected override void OnInitialized()
    {
        BuildKpiCards();
        BuildCharts();
    }

    private void BuildKpiCards()
    {
        _kpiCards =
        [
            new("Total Clientes",       _state.TotalClientes.ToString("N0"),      Icons.Material.Outlined.Group,        "crm-detail--primary", "Clientes activos en cartera"),
            new("Prospectos Nuevos",    _state.ProspectosNuevos.ToString(),        Icons.Material.Outlined.PersonAdd,    "crm-detail--info",    "Nuevos este mes"),
            new("Tasa de Conversión",   $"{_state.TasaConversion}%",               Icons.Material.Outlined.TrendingUp,   "crm-detail--warning", "Meta mensual"),
            new("Retención Clientes",   $"{_state.RetencionClientes}%",            Icons.Material.Outlined.Loyalty,      "crm-detail--primary", "Índice de fidelización"),
        ];
    }

    private void BuildCharts()
    {
        var palette = BancoAgricolaTheme.ChartPalettes.DashboardDistribucion;

        _lineChartSeries =
        [
            new ChartSeries<double>
            {
                Name = "Adquisición",
                Data = CrmState.AdquisicionMensual
            }
        ];

        _lineChartOptions = new ChartOptions
        {
            ChartPalette = palette,
            ShowLegend = false
        };

        _donutSeries =
        [
            new ChartSeries<double>
            {
                Name = "",
                Data = CrmState.SegmentacionCartera
            }
        ];

        _donutChartOptions = new ChartOptions
        {
            ChartPalette = palette,
            ShowLegend = false
        };

        _donutPalette = BancoAgricolaTheme.ChartPalettes.DashboardDistribucion;
        _donutLabels  = CrmState.SegmentacionLabels;
    }

    private void HandleNuevoCliente() => NavManager.NavigateTo("/crm/agregar");

    private record KpiCard(string Label, string Value, string Icon, string DetailColorClass, string Detail);
}
