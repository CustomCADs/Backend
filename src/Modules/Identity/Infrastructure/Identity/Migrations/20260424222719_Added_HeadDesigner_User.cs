using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Modules.Identity.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Added_HeadDesigner_User : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.UpdateData(
			schema: "Identity",
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("4337a774-2c5c-4c27-d28b-08dd11623eb9"),
			columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
			values: new object[] { "c11d44ef-29c5-43ac-89f9-a2e7cd482ec8", "AQAAAAIAAYagAAAAEJGRCbKUsSh9BwxPXoIQRG1AVXYDfmWsY5vEA4aEnqBBQAzdcgLFCHUtpwd86+B4mA==", "MHG763AUJVOJPKKWUC64FQSF7DIVVBOU" });

		migrationBuilder.InsertData(
			schema: "Identity",
			table: "AspNetUsers",
			columns: new[] { "Id", "AccessFailedCount", "AccountId", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsSSO", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Provider", "SecurityStamp", "TwoFactorEnabled", "UserName" },
			values: new object[] { new Guid("a8145f5f-a3a4-4f06-9461-9f24b9f23fde"), 0, new Guid("8d477999-0580-4770-8864-e9ba4bed9cd1"), "c5940d6f-d5c0-4f84-a262-da9b07525c3c", "john.cad@gmail.com", true, false, true, null, "JOHN.CAD@GMAIL.COM", "JOHN_CAD", "AQAAAAIAAYagAAAAEEUe31maWfuZY6V8MQBzUWKerMKobDukREinVfML3Yl2z+Nr6IIQZKvX4WKqbTUw6w==", null, false, null, "FNNIT3NPOZKZK2E67WFLV5R3RGVBX7LV", false, "John_CAD" });

		migrationBuilder.InsertData(
			schema: "Identity",
			table: "AspNetUserRoles",
			columns: new[] { "RoleId", "UserId" },
			values: new object[] { new Guid("f3ad41d3-ee90-4988-9195-8b2a8f4f2733"), new Guid("a8145f5f-a3a4-4f06-9461-9f24b9f23fde") });
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DeleteData(
			schema: "Identity",
			table: "AspNetUserRoles",
			keyColumns: new[] { "RoleId", "UserId" },
			keyValues: new object[] { new Guid("f3ad41d3-ee90-4988-9195-8b2a8f4f2733"), new Guid("a8145f5f-a3a4-4f06-9461-9f24b9f23fde") });

		migrationBuilder.DeleteData(
			schema: "Identity",
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("a8145f5f-a3a4-4f06-9461-9f24b9f23fde"));

		migrationBuilder.UpdateData(
			schema: "Identity",
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("4337a774-2c5c-4c27-d28b-08dd11623eb9"),
			columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
			values: new object[] { "c5940d6f-d5c0-4f84-a262-da9b07525c3c", "AQAAAAIAAYagAAAAEEUe31maWfuZY6V8MQBzUWKerMKobDukREinVfML3Yl2z+Nr6IIQZKvX4WKqbTUw6w==", "FNNIT3NPOZKZK2E67WFLV5R3RGVBX7LV" });
	}
}
