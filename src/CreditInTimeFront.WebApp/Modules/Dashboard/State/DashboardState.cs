namespace CreditInTimeFront.WebApp.Modules.Dashboard.State;

public class DashboardState
{
    public int SelectedYear { get; set; } = 2026;
    public List<int> AvailableYears { get; } = new() { 2026, 2025, 2024, 2023, 2022 };
}
