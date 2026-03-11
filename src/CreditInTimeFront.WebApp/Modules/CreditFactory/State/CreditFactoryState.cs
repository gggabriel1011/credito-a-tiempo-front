using CreditInTimeFront.WebApp.Modules.CreditFactory.ViewModels;

namespace CreditInTimeFront.WebApp.Modules.CreditFactory.State;

public class CreditFactoryState
{
    public static class ReportUrls
    {
        public const string EvaluacionCrediticia    = "http://192.168.41.115/ReportServer?/Productos/Prestamos/Fabrica/EvaluacionCrediticia&rs:Command=Render&rs:Format=PDF";
        public const string AutorizacionBuro        = "http://192.168.41.115/ReportServer?/Productos/Prestamos/Fabrica/AutorizacionBuroCredito&rs:Command=Render&rs:Format=PDF";
        public const string PerfilCliente           = "http://192.168.41.115/ReportServer?/Productos/Prestamos/Fabrica/PerfilCliente_PersonaFisica&rs:Command=Render&rs:Format=PDF";
        public const string PerfilClienteJuridica   = "http://192.168.41.115/ReportServer?/Productos/Prestamos/Fabrica/PerfilCliente_PersonaJuridica&rs:Command=Render&rs:Format=PDF";
        public const string PreAprobacion           = "http://192.168.41.115/ReportServer?/Productos/Prestamos/Fabrica/PreAprobacionDeCredito&rs:Command=Render&rs:Format=PDF";
        public const string ConozcaSuClienteFisico  = "http://192.168.41.115/ReportServer?/Productos/Prestamos/Fabrica/ConozcaSuCliente_PersonaFisica&rs:Command=Render&rs:Format=PDF";
        public const string ConozcaSuClienteJuridico = "http://192.168.41.115/ReportServer?/Productos/Prestamos/Fabrica/ConozcaSuCliente_PersonaJuridica&rs:Command=Render&rs:Format=PDF";
    }

    public static readonly IReadOnlyList<string> ProductTypes =
    [
        "Préstamo Agrícola",
        "Línea Pyme",
        "Préstamo Personal",
        "Línea de Crédito Comercial",
    ];

    public IReadOnlyList<CreditRequestViewModel> Requests { get; } = LoadMockData();

    // KPIs — computed from Status field
    public int TotalPending  => Requests.Count(r => r.Status == CreditRequestStatus.Pendiente);
    public int InAnalysis    => Requests.Count(r => r.Status == CreditRequestStatus.EnAnalisis);
    public int ApprovedToday => Requests.Count(r => r.Status == CreditRequestStatus.Aprobado);
    public decimal TotalAmount => Requests.Sum(r => r.Amount);

    private static List<CreditRequestViewModel> LoadMockData() =>
    [
        new() { CaseNumber = "EXP-9942", ClientName = "Ramón Antonio Santos",       Identification = "001-0000000-0",     Product = "Préstamo Agrícola",          Amount = 850_000,   Progress = 92,  IsCompany = false, SubmittedAt = new DateTime(2026, 1, 3),  Status = CreditRequestStatus.Aprobado   },
        new() { CaseNumber = "EXP-9945", ClientName = "Constructora Delta, SAS",    Identification = "RNC: 1-31-44556-2", Product = "Línea de Crédito Comercial", Amount = 4_200_000, Progress = 45,  IsCompany = true,  SubmittedAt = new DateTime(2026, 1, 7),  Status = CreditRequestStatus.EnAnalisis },
        new() { CaseNumber = "EXP-9948", ClientName = "María Elena Guzmán",         Identification = "002-1234567-8",     Product = "Préstamo Personal",          Amount = 150_000,   Progress = 100, IsCompany = false, SubmittedAt = new DateTime(2026, 1, 10), Status = CreditRequestStatus.Aprobado   },
        new() { CaseNumber = "EXP-9951", ClientName = "Agrícola Los Pinos, SRL",    Identification = "RNC: 1-30-22334-5", Product = "Préstamo Agrícola",          Amount = 2_500_000, Progress = 25,  IsCompany = true,  SubmittedAt = new DateTime(2026, 1, 13), Status = CreditRequestStatus.Pendiente  },
        new() { CaseNumber = "EXP-9953", ClientName = "José Miguel Peña",           Identification = "003-9876543-2",     Product = "Línea Pyme",                 Amount = 750_000,   Progress = 78,  IsCompany = false, SubmittedAt = new DateTime(2026, 1, 15), Status = CreditRequestStatus.EnAnalisis },
        new() { CaseNumber = "EXP-9956", ClientName = "Ana Patricia Mejía",         Identification = "004-5678901-3",     Product = "Préstamo Personal",          Amount = 200_000,   Progress = 65,  IsCompany = false, SubmittedAt = new DateTime(2026, 1, 18), Status = CreditRequestStatus.EnAnalisis },
        new() { CaseNumber = "EXP-9959", ClientName = "Distribuidora Central, SRL", Identification = "RNC: 1-32-55667-8", Product = "Línea de Crédito Comercial", Amount = 3_500_000, Progress = 88,  IsCompany = true,  SubmittedAt = new DateTime(2026, 1, 21), Status = CreditRequestStatus.Aprobado   },
        new() { CaseNumber = "EXP-9962", ClientName = "Carlos Eduardo Núñez",       Identification = "005-4321098-7",     Product = "Préstamo Agrícola",          Amount = 1_200_000, Progress = 55,  IsCompany = false, SubmittedAt = new DateTime(2026, 1, 24), Status = CreditRequestStatus.EnAnalisis },
        new() { CaseNumber = "EXP-9965", ClientName = "Ferretería El Progreso, SA", Identification = "RNC: 1-33-77889-0", Product = "Línea Pyme",                 Amount = 980_000,   Progress = 70,  IsCompany = true,  SubmittedAt = new DateTime(2026, 1, 27), Status = CreditRequestStatus.Pendiente  },
        new() { CaseNumber = "EXP-9968", ClientName = "Luisa Fernanda Castro",      Identification = "006-8765432-1",     Product = "Préstamo Personal",          Amount = 350_000,   Progress = 40,  IsCompany = false, SubmittedAt = new DateTime(2026, 1, 30), Status = CreditRequestStatus.Rechazado  },
    ];
}
