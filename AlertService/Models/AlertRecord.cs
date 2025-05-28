namespace AlertService.Models
{
    public class AlertRecord
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
    }
}
