using Microsoft.EntityFrameworkCore;
using PatientService.Models;

namespace PatientService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
    }
}
