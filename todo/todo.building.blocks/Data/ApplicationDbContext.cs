using Microsoft.EntityFrameworkCore;
using todo.domain.Models;
using todo.infrastructure.shared.Interfaces;

namespace todo.infrastructure.shared.Data
{
    public class ApplicationDbContext : DbContext, IDbContext<ApplicationDbContext>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 
       
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                 //.ToTable("User")
                 .HasMany<Task>(u => u.Tasks);


            modelBuilder.Entity<Task>()
                 //.ToTable("Task")
                 .HasOne<User>(t => t.CreatedBy);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
