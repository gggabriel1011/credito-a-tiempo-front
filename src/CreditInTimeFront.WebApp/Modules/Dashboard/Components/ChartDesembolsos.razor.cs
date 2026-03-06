using Microsoft.AspNetCore.Components;
using MudBlazor;
using CreditInTimeFront.WebApp.Shared.Themes;

namespace CreditInTimeFront.WebApp.Modules.Dashboard.Components;

public partial class ChartDesembolsos
{
    [Parameter] public string Title { get; set; } = "Gráfico";
    [Parameter] public List<ChartDataPoint> DataPoints { get; set; } = new();
    [Parameter] public List<int> AvailableYears { get; set; } = new();
    [Parameter] public EventCallback<int> OnYearChanged { get; set; }

    // StackedBarChartOptions includes ChartPalette + BarWidthRatio + ShowLegend
    // Palette: [0] gray for normal bars, [1] blue for the highlighted (max) bar
    private readonly StackedBarChartOptions _chartOptions = new()
    {
        ChartPalette = BancoAgricolaTheme.ChartPalettes.DashboardDesembolsos,
        BarWidthRatio = 0.5,
        ShowLegend = false
    };

    // Series 0: gray — all values except the highlighted bar
    // Series 1: blue  — only the highlighted bar, 0 elsewhere
    // StackedBar stacks them so each position shows a single solid color
    private List<ChartSeries<double>> BarSeries => new()
    {
        new ChartSeries<double>
        {
            Name = "",
            Data = DataPoints.Select(d => d.IsHighlighted ? 0.0 : d.Value).ToArray()
        },
        new ChartSeries<double>
        {
            Name = "",
            Data = DataPoints.Select(d => d.IsHighlighted ? d.Value : 0.0).ToArray()
        }
    };

    // "Enero" → "Ene", "Febrero" → "Feb", etc. (title case, 3 chars)
    private string[] Labels => DataPoints
        .Select(d => d.Month[..Math.Min(3, d.Month.Length)])
        .ToArray();

    private async Task OnYearSelected(ChangeEventArgs e)
    {
        if (int.TryParse(e.Value?.ToString(), out int year))
            await OnYearChanged.InvokeAsync(year);
    }

    public class ChartDataPoint
    {
        public string Month { get; set; } = "";
        public double Value { get; set; }
        public bool IsHighlighted { get; set; }
    }
}
