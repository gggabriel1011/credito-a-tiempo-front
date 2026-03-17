namespace CreditInTimeFront.WebApp.Modules.Dashboard.ViewModels;

public class DashboardViewModel
{
    public string CarteraTotal { get; set; } = "RD$ 1.2B";
    public string CarteraTrend { get; set; } = "+12%";
    public bool CarteraTrendPositive { get; set; } = true;

    public string IndiceMora { get; set; } = "3.2%";
    public string MoraTrend { get; set; } = "-0.4%";
    public bool MoraTrendPositive { get; set; } = false;

    public string SolicitudesMes { get; set; } = "450";
    public string TasaAprobacion { get; set; } = "78%";
}
