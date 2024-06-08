using Microsoft.EntityFrameworkCore;
using APPHDotNetCore.NLayer.DataAccess.Models;

namespace APPHDotNetCore.NLayer.DataAccess.Db
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringbuilder.ConnectionString);
        }
        public DbSet<BlogModel> Blogs { get; set; }
    }
}
