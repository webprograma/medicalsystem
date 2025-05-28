using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientService.DTOs;
using PatientService.Models;
using PatientService.Services.Interfaces;

namespace PatientService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // JWT talab qilinadi
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _service;

        public PatientsController(IPatientService service)
        {
            _service = service;
        }

        // GET: api/patients
        [HttpGet]
        [Authorize(Roles = "Admin,Physician")]
        public async Task<IActionResult> GetAll()
        {
            var patients = await _service.GetAllAsync();
            return Ok(patients);
        }

        // GET: api/patients/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Physician,Nurse,Patient")]
        public async Task<IActionResult> Get(int id)
        {
            var patient = await _service.GetByIdAsync(id);
            if (patient == null) return NotFound();
            return Ok(patient);
        }

        // POST: api/patients
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] PatientDto dto)
        {
            var patient = new Patient
            {
                FullName = dto.FullName,
                Diagnosis = dto.Diagnosis,
                AdmissionDate = dto.AdmissionDate,
                Discharged = dto.Discharged
            };

            var created = await _service.CreateAsync(patient);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PUT: api/patients/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Physician")]
        public async Task<IActionResult> Update(int id, [FromBody] PatientDto dto)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.FullName = dto.FullName;
            existing.Diagnosis = dto.Diagnosis;
            existing.AdmissionDate = dto.AdmissionDate;
            existing.Discharged = dto.Discharged;

            var updated = await _service.UpdateAsync(existing);
            return updated ? NoContent() : StatusCode(500, "Yangilashda xatolik");
        }

        // DELETE: api/patients/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
