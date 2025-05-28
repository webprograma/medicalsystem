using MonitoringService.Models;

namespace MonitoringService.Services.Interfaces
{
    public interface IMonitoringService
    {
        Task<List<MonitoringRecord>> GetAllForPatientAsync(int patientId);
        Task<MonitoringRecord> CreateAsync(MonitoringRecord record);
    }
}
