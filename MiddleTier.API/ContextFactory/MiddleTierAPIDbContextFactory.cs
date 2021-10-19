using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MiddleTier.Api.Data;

namespace Stock.Companies.Data.ContextFactory
{
    /// <summary>
    /// The Context Factory will configure our context with the connection string to be used in the Migration for example.
    /// This Context Factory is only necessary for Design-time DbContext Creation required to run Migrations
    /// The string connection should not be stored within the code.
    /// </summary>
    public class MiddleTierAPIDbContextFactory : IDesignTimeDbContextFactory<MiddleTierAPIDbContext>
    {
        private IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        public MiddleTierAPIDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MiddleTierAPIDbContext>();
            builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            return new MiddleTierAPIDbContext(builder.Options);
        }
    }
}