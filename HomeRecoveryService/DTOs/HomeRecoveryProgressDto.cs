namespace HomeRecoveryService.DTOs;

public class HomeRecoveryProgressDto
{
    public string Note { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
