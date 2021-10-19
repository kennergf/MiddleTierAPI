using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MiddleTier.API.Data
{
    internal static class Seed
    {
        internal static void Users(ModelBuilder builder)
        {
            var users = new List<IdentityUser>();
            users.Add(new IdentityUser()
            {
                Id = "d42eb8c8-2232-406b-9bc0-3859863f064c",
                UserName = "admin@api.ie",
                NormalizedUserName = "ADMIN@API.IE",
                Email = "admin@api.ie",
                NormalizedEmail = "ADMIN@API.IE",
                PhoneNumber = "014569145",
                LockoutEnabled = true,
            });
            users.Add(new IdentityUser()
            {
                Id = "160453a8-8e19-4f70-9b61-3f6f37d82a3a",
                UserName = "user@api.ie",
                NormalizedUserName = "USER@API.IE",
                Email = "user@api.ie",
                NormalizedEmail = "USER@API.IE",
                PhoneNumber = "0834564578",
                LockoutEnabled = true,
            });
            
            // REF https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.passwordhasher-1?view=aspnetcore-5.0
            PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();
            foreach (var user in users)
            {
                user.PasswordHash = hasher.HashPassword(user, "Api@2021");
                builder.Entity<IdentityUser>().HasData(user);
            }
        }
    }
}