using Microsoft.EntityFrameworkCore;
using MonitoringService.Data;
using MonitoringService.Models;
using MonitoringService.Services.Interfaces;
using MonitoringService.Services.Messaging;

namespace MonitoringService.Services
{
    public class MonitoringService : IMonitoringService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMessagePublisher _publisher;

        public MonitoringService(ApplicationDbContext context, IMessagePublisher publisher)
        {
            _context = context;
            _publisher = publisher;
        }

        public async Task<List<MonitoringRecord>> GetAllForPatientAsync(int patientId)
        {
            return await _context.MonitoringRecords
                .Where(r => r.PatientId == patientId)
                .OrderByDescending(r => r.RecordedAt)
                .ToListAsync();
        }

        public async Task<MonitoringRecord> CreateAsync(MonitoringRecord record)
        {
            // IsCritical aniqlash (sodda qoidalar bilan)
            record.IsCritical = record.Temperature > 38 || record.BloodPressure.Contains("180");

            _context.MonitoringRecords.Add(record);
            await _context.SaveChangesAsync();

            if (record.IsCritical)
            {
                var alert = $"🚨 ALERT: PatientId {record.PatientId} is in critical condition!";
                _publisher.PublishAlert(alert);
            }

            return record;
        }
    }
}
