namespace HomeRecoveryService.Models;

public class HomeRecoveryProgress
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string Note { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
