using PatientService.Models;

namespace PatientService.Services.Interfaces
{
    public interface IPatientService
    {
        Task<List<Patient>> GetAllAsync();
        Task<Patient?> GetByIdAsync(int id);
        Task<Patient> CreateAsync(Patient patient);
        Task<bool> UpdateAsync(Patient patient);
        Task<bool> DeleteAsync(int id);
    }
}
