namespace MonitoringService.Models
{
    public class MonitoringRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public double Temperature { get; set; }
        public string BloodPressure { get; set; } = string.Empty;
        public string Symptoms { get; set; } = string.Empty;
        public bool IsCritical { get; set; }
        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
    }
}
