using Microsoft.AspNetCore.Mvc;
using AlertService.Data;
using Microsoft.AspNetCore.Authorization;
using AlertService.Models;
using Microsoft.EntityFrameworkCore;


namespace AlertService.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AlertsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlertsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Physician,Nurse")]
        public async Task<IActionResult> GetAll()
        {
            var records = await _context.AlertRecords
                .OrderByDescending(r => r.ReceivedAt)
                .ToListAsync();
            return Ok(records);
        }

    }
}
