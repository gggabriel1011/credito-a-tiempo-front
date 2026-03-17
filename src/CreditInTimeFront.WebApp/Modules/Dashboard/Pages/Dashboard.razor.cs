using CreditInTimeFront.WebApp.Modules.Dashboard.Components;

namespace CreditInTimeFront.WebApp.Modules.Dashboard.Pages;

public partial class Dashboard
{
    private readonly List<int> _years = new() { 2026, 2025, 2024, 2023, 2022 };
    private List<ChartDesembolsos.ChartDataPoint> _desembolsos = new();

    private readonly List<ChartDistribucion.ChartItem> _dataDistribucion = new()
    {
        new() { Label = "Para Pollo", Percentage = 40 },
        new() { Label = "Para Miel",  Percentage = 25 },
        new() { Label = "Para Maíz",  Percentage = 20 },
        new() { Label = "Para Café",  Percentage = 15 }
    };

    private List<NotificationData> _notificaciones = new();

    protected override void OnInitialized()
    {
        ActualizarGrafico(2026);
        GenerarNotificaciones();
    }

    private void ActualizarGrafico(int year)
    {
        var rnd = new Random();
        var meses = new[]
        {
            "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
            "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
        };

        _desembolsos = meses.Select(mes => new ChartDesembolsos.ChartDataPoint
        {
            Month = mes,
            Value = rnd.Next(500, 3000),
            IsHighlighted = false
        }).ToList();

        var maxVal = _desembolsos.Max(x => x.Value);
        var mesMax = _desembolsos.FirstOrDefault(x => x.Value == maxVal);
        if (mesMax != null) mesMax.IsHighlighted = true;
    }

    private void GenerarNotificaciones()
    {
        var rnd = new Random();
        var opciones = new List<NotificationData>
        {
            new() { Title = "Gran Desembolso Pendiente",   Icon = "priority_high", Type = "warning",
                    Description = "El expediente EXP-2024-{0} requiere validación final para RD$ {1:N0}." },
            new() { Title = "Incremento en Mora Regional", Icon = "trending_up",   Type = "danger",
                    Description = "Se detectó un incremento de {0}% en el índice de mora en la Región {1}." },
            new() { Title = "Meta de Aprobación Alcanzada",Icon = "task_alt",      Type = "success",
                    Description = "La sucursal No. {0} ha cumplido el 100% de su cuota de aprobaciones." },
            new() { Title = "Nueva Solicitud Recibida",    Icon = "description",   Type = "info",
                    Description = "El cliente #{0} ha enviado una nueva solicitud por RD$ {1:N0}." }
        };

        _notificaciones = Enumerable.Range(1, 5).Select(_ =>
        {
            var base_ = opciones[rnd.Next(opciones.Count)];
            return new NotificationData
            {
                Icon        = base_.Icon,
                Title       = base_.Title,
                Type        = base_.Type,
                TimeAgo     = $"Hace {rnd.Next(5, 59)} min",
                Description = string.Format(base_.Description, rnd.Next(1, 100), rnd.Next(100_000, 5_000_000))
            };
        }).ToList();
    }

    public class NotificationData
    {
        public string Icon        { get; set; } = "";
        public string Title       { get; set; } = "";
        public string Description { get; set; } = "";
        public string TimeAgo     { get; set; } = "";
        public string Type        { get; set; } = "info";
    }
}
