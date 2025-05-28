using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeRecoveryService.Data;
using HomeRecoveryService.DTOs;
using HomeRecoveryService.Models;

namespace HomeRecoveryService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HomeRecoveryController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public HomeRecoveryController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("{patientId}/plan")]
    [Authorize(Roles = "Physician")]
    public async Task<IActionResult> CreatePlan(int patientId, HomeRecoveryPlanDto dto)
    {
        var plan = new HomeRecoveryPlan
        {
            PatientId = patientId,
            Description = dto.Description
        };

        _context.Plans.Add(plan);
        await _context.SaveChangesAsync();
        return Ok(plan);
    }

    [HttpGet("{patientId}/plan")]
    [Authorize(Roles = "Physician,Nurse,Patient")]
    public async Task<IActionResult> GetPlan(int patientId)
    {
        var plan = await _context.Plans.FirstOrDefaultAsync(p => p.PatientId == patientId);
        return plan == null ? NotFound() : Ok(plan);
    }

    [HttpPost("{patientId}/progress")]
    [Authorize(Roles = "Patient,Nurse")]
    public async Task<IActionResult> AddProgress(int patientId, HomeRecoveryProgressDto dto)
    {
        var progress = new HomeRecoveryProgress
        {
            PatientId = patientId,
            Note = dto.Note,
            UpdatedAt = dto.UpdatedAt
        };

        _context.Progresses.Add(progress);
        await _context.SaveChangesAsync();
        return Ok(progress);
    }

    [HttpGet("{patientId}/progress")]
    [Authorize(Roles = "Physician,Nurse,Patient")]
    public async Task<IActionResult> GetProgress(int patientId)
    {
        var list = await _context.Progresses
            .Where(p => p.PatientId == patientId)
            .OrderByDescending(p => p.UpdatedAt)
            .ToListAsync();

        return Ok(list);
    }
}
