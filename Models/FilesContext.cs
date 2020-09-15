using Microsoft.EntityFrameworkCore;

namespace TryingWebApi.Models
{ 
    public class FilesContext : DbContext
    {
        public DbSet<FileModel> Files { get; set; }
        public FilesContext(DbContextOptions<FilesContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}