namespace CreditInTimeFront.WebApp.Modules.CreditFactory.ViewModels;

public enum CreditRequestStatus { Pendiente, EnAnalisis, Aprobado, Rechazado }

public static class CreditRequestStatusExtensions
{
    public static string ToLabel(this CreditRequestStatus status) => status switch
    {
        CreditRequestStatus.Pendiente  => "Pendiente",
        CreditRequestStatus.EnAnalisis => "En Análisis",
        CreditRequestStatus.Aprobado   => "Aprobado",
        CreditRequestStatus.Rechazado  => "Rechazado",
        _                              => status.ToString()
    };
}

public class CreditRequestViewModel
{
    public string CaseNumber    { get; set; } = string.Empty;
    public string ClientName    { get; set; } = string.Empty;
    public string Identification { get; set; } = string.Empty;
    public string Product       { get; set; } = string.Empty;
    public decimal Amount       { get; set; }
    public int Progress         { get; set; }
    public bool IsCompany       { get; set; }
    public DateTime SubmittedAt { get; set; }
    public CreditRequestStatus Status { get; set; }
}
