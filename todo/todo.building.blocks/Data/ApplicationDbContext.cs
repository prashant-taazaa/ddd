using Microsoft.EntityFrameworkCore;
using todo.domain.Models;
using todo.infrastructure.shared.Interfaces;

namespace todo.infrastructure.shared.Data
{
    public class ApplicationDbContext : DbContext, IDbContext<ApplicationDbContext>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
       
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
