namespace DiagnosisService.Models;

public class Diagnosis
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string Condition { get; set; } = string.Empty;
    public string Treatment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
