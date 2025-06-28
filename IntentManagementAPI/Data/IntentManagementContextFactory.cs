using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace IntentManagementAPI.Data
{
    public class IntentManagementContextFactory : IDesignTimeDbContextFactory<IntentManagementContext>
    {
        public IntentManagementContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<IntentManagementContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlite(connectionString);

            return new IntentManagementContext(optionsBuilder.Options);
        }
    }
} 