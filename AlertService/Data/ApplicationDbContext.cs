using Microsoft.EntityFrameworkCore;
using AlertService.Models;

namespace AlertService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<AlertRecord> AlertRecords { get; set; }
    }
}
