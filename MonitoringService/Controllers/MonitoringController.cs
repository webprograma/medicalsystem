using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonitoringService.DTOs;
using MonitoringService.Models;
using MonitoringService.Services.Interfaces;

namespace MonitoringService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/patients/{patientId}/monitoring")]
    public class MonitoringController : ControllerBase
    {
        private readonly IMonitoringService _service;

        public MonitoringController(IMonitoringService service)
        {
            _service = service;
        }

        // GET: /api/patients/2/monitoring
        [HttpGet]
        [Authorize(Roles = "Physician,Nurse")]
        public async Task<IActionResult> Get(int patientId)
        {
            var records = await _service.GetAllForPatientAsync(patientId);
            return Ok(records);
        }

        // POST: /api/patients/2/monitoring
        [HttpPost]
        [Authorize(Roles = "Patient,Nurse")]
        public async Task<IActionResult> Create(int patientId, [FromBody] MonitoringRecordDto dto)
        {
            var record = new MonitoringRecord
            {
                PatientId = patientId,
                Temperature = dto.Temperature,
                BloodPressure = dto.BloodPressure,
                Symptoms = dto.Symptoms
            };

            var created = await _service.CreateAsync(record);
            return CreatedAtAction(nameof(Get), new { patientId = created.PatientId }, created);
        }
    }
}
