using Microsoft.EntityFrameworkCore;
using RehabilitationService.Models;

namespace RehabilitationService.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<RehabPlan> RehabPlans => Set<RehabPlan>();
    public DbSet<RehabProgress> RehabProgress => Set<RehabProgress>();
}
