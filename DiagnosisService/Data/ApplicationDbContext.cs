using Microsoft.EntityFrameworkCore;
using DiagnosisService.Models;

namespace DiagnosisService.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Diagnosis> Diagnoses => Set<Diagnosis>();
}
