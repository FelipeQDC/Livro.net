using Livro.Models;
using Microsoft.EntityFrameworkCore;

namespace Livro.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<LivroMd> Livros { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("DataSource=app.db;Cache=Shared");
        }
    }
}