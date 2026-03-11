using MudBlazor;

namespace CreditInTimeFront.WebApp.Shared.Themes;

/// <summary>
/// Banco Agrícola Theme Configuration
/// 
/// Define los colores y estilos corporativos de Banco Agrícola.
/// Los colores son consistentes con los estándares de diseño.
/// 
/// Paleta de colores:
/// - Primary: #6CB324 (Verde corporativo)
/// - Secondary: #D1D5DB (Gris)
/// - Background Light: #F3F4F6 (Gris claro)
/// - Background Dark: #111827 (Casi negro)
/// </summary>
public static class BancoAgricolaTheme
{
    public static class Colors
    {
        public const string Primary = "#6CB324";
        public const string PrimaryDark = "#8ACD5F";
        public const string Secondary = "#D1D5DB";
        public const string Background = "#F3F4F6";
        public const string Surface = "#FFFFFF";
        public const string Error = "#EF4444";
        public const string Warning = "#F59E0B";
        public const string Info = "#3B82F6";
        public const string Success = "#10B981";

        public const string ChartNeutral = "#CBD5E1";
    }

    public static class ChartPalettes
    {
        public static readonly string[] DashboardDesembolsos =
        {
            Colors.ChartNeutral,
            Colors.Info
        };

        public static readonly string[] DashboardDistribucion =
        {
            Colors.Primary,
            Colors.Info,
            Colors.Warning,
            Colors.Error
        };
    }

    /// <summary>
    /// Retorna la configuración del tema MudBlazor para Banco Agrícola
    /// </summary>
    public static MudTheme GetTheme()
    {
        return new MudTheme
        {
            PaletteLight = new PaletteLight
            {
                Primary = Colors.Primary,
                Secondary = Colors.Secondary,
                AppbarBackground = Colors.Surface,
                Background = Colors.Background,
                Surface = Colors.Surface,
                Error = Colors.Error,
                Warning = Colors.Warning,
                Info = Colors.Info,
                Success = Colors.Success,
            },
            PaletteDark = new PaletteDark
            {
                Primary = Colors.PrimaryDark,
                Secondary = "#374151",         // Gris más oscuro
                AppbarBackground = "#1F2937",  // Gris oscuro
                Background = "#111827",        // Casi negro
                Surface = "#1F2937",           // Gris oscuro
                DrawerBackground = "#1F2937",  // Mismo que Surface — consistente con body
                DrawerText = "#E5E7EB",        // Mismo que text-body dark
                Error = "#FCA5A5",             // Rojo suave
                Warning = "#FCD34D",           // Ámbar suave
                Info = "#93C5FD",              // Azul suave
                Success = "#6EE7B7",           // Verde suave
            },
            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "8px",
            },
        };
    }
}

