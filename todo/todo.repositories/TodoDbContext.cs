using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using todo.domain.Models;

namespace todo.repositories
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
