using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MiddleTier.Api.Data
{
    public class MiddleTierAPIDbContext : IdentityDbContext
    {
        public MiddleTierAPIDbContext(DbContextOptions<MiddleTierAPIDbContext> options) : base(options) { }
    }
}