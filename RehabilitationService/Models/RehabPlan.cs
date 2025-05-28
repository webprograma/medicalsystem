namespace RehabilitationService.Models;

public class RehabPlan
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
