using System;
using Microsoft.EntityFrameworkCore;

namespace UploadFilesBackend.Models
{
    // A context configuration of the file.
    // Serving as a bridge between the program and the database.
    // TODO Could be transformed into an interface repository in a generic manner.
    public class FileContext : DbContext, IFileRepository
    {

        public FileContext(DbContextOptions<FileContext> options) : base(options)
        {
        }

        public FileContext()
        {

        }
        public DbSet<FileModel> Files { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var server = "192.168.65.0/24";
            var port = "5432";
            var name = "your_db";
            var user = "your_user";
            var password = "your_password";

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql($"Host={server};Port={port};Database={name};Username={user};Password={password}");
            }
        }

        public Boolean isFileModelValid(FileModel fileModel)
        {
            return fileModel.CreatedAt != null && fileModel.FileContent != null
                && fileModel.FileName != null;
        }

    }
}
