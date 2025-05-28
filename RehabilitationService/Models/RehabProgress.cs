namespace RehabilitationService.Models;

public class RehabProgress
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
