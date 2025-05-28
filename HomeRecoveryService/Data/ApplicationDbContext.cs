using Microsoft.EntityFrameworkCore;
using HomeRecoveryService.Models;

namespace HomeRecoveryService.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<HomeRecoveryPlan> Plans => Set<HomeRecoveryPlan>();
    public DbSet<HomeRecoveryProgress> Progresses => Set<HomeRecoveryProgress>();
}
