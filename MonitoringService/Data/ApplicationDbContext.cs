using Microsoft.EntityFrameworkCore;
using MonitoringService.Models;

namespace MonitoringService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<MonitoringRecord> MonitoringRecords { get; set; }
    }
}
