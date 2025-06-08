using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Mas_Project.Data;

namespace Mas_Project
{
    public class DBContextFactory : IDesignTimeDbContextFactory<DBContext>
    {
        public DBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
            optionsBuilder.UseSqlite("Data Source=mas_project.db"); // or your actual connection string

            return new DBContext(optionsBuilder.Options);
        }
    }
}