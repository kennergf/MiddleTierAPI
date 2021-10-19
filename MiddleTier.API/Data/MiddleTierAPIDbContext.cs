using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiddleTier.API.Data;

namespace MiddleTier.Api.Data
{
    public class MiddleTierAPIDbContext : IdentityDbContext
    {
        public MiddleTierAPIDbContext(DbContextOptions<MiddleTierAPIDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            Seed.Users(builder);
        }
    }
}