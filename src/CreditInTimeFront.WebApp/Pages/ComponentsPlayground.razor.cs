using MudBlazor;
using CreditInTimeFront.WebApp.Components;

namespace CreditInTimeFront.WebApp.Pages;

public partial class ComponentsPlayground
{
    // ── AlertMessage ──────────────────────────────────────────────────
    private Severity? _alertSeverity;

    private void ShowAlert(Severity severity) => _alertSeverity = severity;

    // ── ChartCard / charts ────────────────────────────────────────────
    private readonly List<ChartSeries<double>> _barSeries = new()
    {
        new ChartSeries<double> { Name = "Desembolsos", Data = new double[] { 320, 450, 280, 510, 390, 470 } }
    };

    private readonly string[] _barLabels = ["Ene", "Feb", "Mar", "Abr", "May", "Jun"];

    private readonly List<ChartSeries<double>> _donutSeries = new()
    {
        new ChartSeries<double> { Name = "Distribución", Data = new double[] { 40, 25, 20, 15 } }
    };

    private readonly string[] _donutLabels = ["Personales", "Hipotecarios", "Comerciales", "Otros"];

    // ── ConfirmDialog ─────────────────────────────────────────────────
    private string? _dialogResult;

    private async Task OpenConfirmDefault()
    {
        var parameters = new DialogParameters<ConfirmDialog>
        {
            { x => x.Title,       "Guardar cambios" },
            { x => x.Message,     "¿Estás seguro de que deseas guardar los cambios realizados?" },
            { x => x.ConfirmText, "Guardar" },
            { x => x.ConfirmColor, Color.Primary },
            { x => x.Icon,        Icons.Material.Outlined.Save },
        };

        var dialog = await DialogService.ShowAsync<ConfirmDialog>("", parameters);
        var result = await dialog.Result;
        _dialogResult = result is { Canceled: false } ? "Confirmado" : "Cancelado";
    }

    private async Task OpenConfirmDestructive()
    {
        var parameters = new DialogParameters<ConfirmDialog>
        {
            { x => x.Title,        "Eliminar registro" },
            { x => x.Message,      "Esta acción es irreversible. ¿Confirmas la eliminación?" },
            { x => x.ConfirmText,  "Eliminar" },
            { x => x.ConfirmColor, Color.Error },
            { x => x.Icon,         Icons.Material.Outlined.DeleteForever },
        };

        var dialog = await DialogService.ShowAsync<ConfirmDialog>("", parameters);
        var result = await dialog.Result;
        _dialogResult = result is { Canceled: false } ? "Confirmado" : "Cancelado";
    }
}
