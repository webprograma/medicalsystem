using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RehabilitationService.Data;
using RehabilitationService.DTOs;
using RehabilitationService.Models;

namespace RehabilitationService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RehabilitationController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RehabilitationController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("{patientId}/plan")]
    [Authorize(Roles = "Physician")]
    public async Task<IActionResult> CreatePlan(int patientId, [FromBody] RehabPlanCreateDto dto)
    {
        var plan = new RehabPlan
        {
            PatientId = patientId,
            Description = dto.Description
        };

        _context.RehabPlans.Add(plan);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPlan), new { patientId = plan.PatientId }, plan);
    }

    [HttpGet("{patientId}/plan")]
    [Authorize(Roles = "Physician,Nurse,Patient")]
    public async Task<IActionResult> GetPlan(int patientId)
    {
        var plan = await _context.RehabPlans
            .Where(p => p.PatientId == patientId)
            .OrderByDescending(p => p.CreatedAt)
            .FirstOrDefaultAsync();

        return plan == null ? NotFound("Rehab plan not found") : Ok(plan);
    }

    [HttpPost("{patientId}/progress")]
    [Authorize(Roles = "Patient,Nurse")]
    public async Task<IActionResult> AddProgress(int patientId, [FromBody] RehabProgressCreateDto dto)
    {
        var progress = new RehabProgress
        {
            PatientId = patientId,
            Notes = dto.Notes,
            UpdatedAt = dto.UpdatedAt
        };

        _context.RehabProgress.Add(progress);
        await _context.SaveChangesAsync();
        return Ok(progress);
    }

    [HttpGet("{patientId}/progress")]
    [Authorize(Roles = "Physician,Nurse,Patient")]
    public async Task<IActionResult> GetProgress(int patientId)
    {
        var progressList = await _context.RehabProgress
            .Where(p => p.PatientId == patientId)
            .OrderByDescending(p => p.UpdatedAt)
            .ToListAsync();

        return Ok(progressList);
    }
}
