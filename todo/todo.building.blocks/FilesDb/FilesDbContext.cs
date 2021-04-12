using System;
using System.Collections.Generic;
using System.Text;
using todo.infrastructure.shared.Interfaces;

namespace todo.infrastructure.shared.Data
{
    public class FilesDbContext
    {
        private readonly string _filesPath;
        public FilesDbContext(string path)
        {
            _filesPath = path;
        }
    }
}
