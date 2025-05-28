using Microsoft.EntityFrameworkCore;
using AuthService.Models;

namespace AuthService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) {}

        public DbSet<User> Users { get; set; }
    }
}
