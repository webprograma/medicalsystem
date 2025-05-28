using DiagnosisService.Data;
using DiagnosisService.DTOs;
using DiagnosisService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiagnosisService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DiagnosisController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DiagnosisController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("{patientId}")]
    [Authorize(Roles = "Physician")]
    public async Task<IActionResult> CreateDiagnosis(int patientId, [FromBody] DiagnosisCreateDto dto)
    {
        var diagnosis = new Diagnosis
        {
            PatientId = patientId,
            Condition = dto.Condition,
            Treatment = dto.Treatment
        };

        _context.Diagnoses.Add(diagnosis);
        await _context.SaveChangesAsync();

        return Ok(diagnosis);
    }

    [HttpGet("{patientId}/history")]
    [Authorize(Roles = "Physician,Nurse")]
    public async Task<IActionResult> GetHistory(int patientId)
    {
        var list = await _context.Diagnoses
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

        return Ok(list);
    }

    [HttpGet("recommendations")]
    [Authorize(Roles = "Physician")]
    public IActionResult GetRecommendations()
    {
        // Hozircha statik qaytaramiz
        var tips = new[]
        {
            "Stay hydrated and rest.",
            "Take prescribed medication regularly.",
            "Schedule follow-up appointments."
        };

        return Ok(tips);
    }
}
