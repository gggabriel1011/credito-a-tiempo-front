using Microsoft.AspNetCore.Components;
using MudBlazor;
using CreditInTimeFront.WebApp.Modules.CollectionManagement.State;
using CreditInTimeFront.WebApp.Modules.CollectionManagement.ViewModels;
using CreditInTimeFront.WebApp.Shared.Themes;

namespace CreditInTimeFront.WebApp.Modules.CollectionManagement.Pages;

public partial class CollectionManagement
{
    [Inject] private NavigationManager NavManager { get; set; } = default!;

    // TODO: reemplazar con [Inject] private CollectionManagementState _state { get; set; } al conectar servicios reales
    private readonly CollectionManagementState _state = new();

    private List<KpiCard> _kpiCards = [];

    private List<ChartSeries<double>>  _barChartSeries    = [];
    private List<ChartSeries<double>>  _donutSeries       = [];
    private StackedBarChartOptions     _barChartOptions   = new();
    private ChartOptions               _donutChartOptions = new();
    private string[]                   _donutPalette      = [];
    private string[]                   _donutLabels       = [];

    private readonly List<int>   _availableYears = [2026, 2025, 2024, 2023];
    private readonly string[]    _barLabels      = CollectionManagementState.Meses;

    protected override void OnInitialized()
    {
        _barChartOptions = new StackedBarChartOptions
        {
            ChartPalette  = [BancoAgricolaTheme.Colors.ChartNeutral, BancoAgricolaTheme.Colors.Primary],
            BarWidthRatio = 0.5,
            ShowLegend    = false
        };

        BuildKpiCards();
        BuildDonutChart();
        UpdateBarChart(_availableYears[0]);
    }

    private void BuildKpiCards()
    {
        _kpiCards =
        [
            new("Monto en Mora Total",       _state.MontoEnMora,              Icons.Material.Outlined.AccountBalanceWallet, "negative", "Cartera vencida total"),
            new("Índice de Morosidad",       _state.IndiceMorosidad,          Icons.Material.Outlined.TrendingUp,           "warning",  "Objetivo: 4.5%"),
            new("Promesas de Pago Hoy",      _state.PromesasPagoHoy,          Icons.Material.Outlined.Handshake,            "info",     $"{_state.PromesasCantidad} Casos"),
            new("Casos Críticos (>90 días)", _state.CasosCriticos.ToString(), Icons.Material.Outlined.Warning,              "warning",  "Acción requerida"),
        ];
    }

    private void UpdateBarChart(int year)
    {
        var rnd    = new Random();
        var values = CollectionManagementState.Meses
            .Select(_ => (double)rnd.Next(800, 3000))
            .ToArray();

        var maxVal = values.Max();

        // Series 0: neutral — all bars except the highest
        // Series 1: primary green — only the highest bar
        // StackedBar stacks them so each slot shows a single solid color
        _barChartSeries =
        [
            new ChartSeries<double> { Name = "", Data = values.Select(v => v == maxVal ? 0.0 : v).ToArray() },
            new ChartSeries<double> { Name = "", Data = values.Select(v => v == maxVal ? v   : 0.0).ToArray() }
        ];
    }

    private void BuildDonutChart()
    {
        var palette = BancoAgricolaTheme.ChartPalettes.DashboardDistribucion;

        _donutSeries =
        [
            new ChartSeries<double> { Name = "", Data = CollectionManagementState.DistribucionAntiguedad }
        ];

        _donutChartOptions = new ChartOptions { ChartPalette = palette, ShowLegend = false };
        _donutPalette      = palette;
        _donutLabels       = CollectionManagementState.DistribucionLabels;
    }

    private void OnYearSelected(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out int year))
            UpdateBarChart(year);
    }

    private void HandleNuevaGestion() =>
        NavManager.NavigateTo("/collection-management/seguimiento");
}
