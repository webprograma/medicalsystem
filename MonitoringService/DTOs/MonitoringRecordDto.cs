namespace MonitoringService.DTOs
{
    public class MonitoringRecordDto
    {
        public int PatientId { get; set; }
        public double Temperature { get; set; }
        public string BloodPressure { get; set; } = string.Empty;
        public string Symptoms { get; set; } = string.Empty;
    }
}
