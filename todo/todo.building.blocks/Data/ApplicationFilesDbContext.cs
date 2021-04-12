using System;
using System.Collections.Generic;
using System.Text;
using todo.domain.Models;
using todo.infrastructure.shared.FilesDb;
using todo.infrastructure.shared.Interfaces;

namespace todo.infrastructure.shared.Data
{
    public class ApplicationFilesDbContext : FilesDbContext, IDbContext<ApplicationFilesDbContext>
    {
        public ApplicationFilesDbContext(string path) : base(path)
        {
            Tasks = new FilesDbSet<Task>(path);
            Users = new FilesDbSet<User>(path);
        }
 
        public FilesDbSet<Task> Tasks { get; set; }
        public FilesDbSet<User> Users { get; set; }
    }
}
