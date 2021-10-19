using Microsoft.EntityFrameworkCore.Migrations;

namespace MiddleTier.API.Migrations
{
    public partial class CreatedSeedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d42eb8c8-2232-406b-9bc0-3859863f064c", 0, "0a7c1c08-dd8e-4299-ba85-c1ea0a76c8e7", "admin@api.ie", false, true, null, "ADMIN@API.IE", "ADMIN@API.IE", "AQAAAAEAACcQAAAAEHJp8YimqXqDLHIv7ZUYeANrzcDsRxtrDiUJVwJ92N95oLckE2TK5AkmTSqLEH9PTQ==", "014569145", false, "c2b81ffe-8458-4830-addf-635e3152f5cc", false, "admin@api.ie" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "160453a8-8e19-4f70-9b61-3f6f37d82a3a", 0, "886ab0b2-ff7a-430b-86ed-b549cc114f3e", "user@api.ie", false, true, null, "USER@API.IE", "USER@API.IE", "AQAAAAEAACcQAAAAEKmh8/5waivtMwOV0OhRs+WlIBssh4k7vbgP+1BTT7HahoXapYnHhpIqTvDw8kSETA==", "0834564578", false, "fffea7f6-2c49-48bb-a3e6-df965955797e", false, "user@api.ie" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "160453a8-8e19-4f70-9b61-3f6f37d82a3a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d42eb8c8-2232-406b-9bc0-3859863f064c");
        }
    }
}
