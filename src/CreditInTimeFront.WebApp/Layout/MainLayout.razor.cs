using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CreditInTimeFront.WebApp.Layout;

public partial class MainLayout
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    private bool _isDarkMode;

    protected override async Task OnInitializedAsync()
    {
        var theme = await JsRuntime.InvokeAsync<string>("themeManager.getTheme");
        _isDarkMode = string.Equals(theme, "dark", StringComparison.OrdinalIgnoreCase);
    }
}
