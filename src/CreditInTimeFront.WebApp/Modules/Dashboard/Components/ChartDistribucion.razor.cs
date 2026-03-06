using Microsoft.AspNetCore.Components;
using MudBlazor;
using CreditInTimeFront.WebApp.Shared.Themes;

namespace CreditInTimeFront.WebApp.Modules.Dashboard.Components;

public partial class ChartDistribucion
{
    [Parameter] public string Title { get; set; } = "Distribución";
    [Parameter] public string CenterText { get; set; } = "";
    [Parameter] public List<ChartItem> DataItems { get; set; } = new();

    private readonly string[] _palette = BancoAgricolaTheme.ChartPalettes.DashboardDistribucion;

    private readonly ChartOptions _chartOptions = new()
    {
        ChartPalette = BancoAgricolaTheme.ChartPalettes.DashboardDistribucion,
        ShowLegend = false
    };

    // Donut chart: ONE ChartSeries<double> whose Data array holds all segment values.
    // ChartLabels provides the label for each segment.
    private List<ChartSeries<double>> DonutSeries => new()
    {
        new ChartSeries<double>
        {
            Name = "",
            Data = InputData
        }
    };

    private double[] InputData => DataItems.Select(d => d.Percentage).ToArray();
    private string[] InputLabels => DataItems.Select(d => d.Label).ToArray();

    public class ChartItem
    {
        public string Label { get; set; } = "";
        public double Percentage { get; set; }
    }
}
