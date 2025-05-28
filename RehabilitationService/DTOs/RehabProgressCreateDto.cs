namespace RehabilitationService.DTOs;

public class RehabProgressCreateDto
{
    public string Notes { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
