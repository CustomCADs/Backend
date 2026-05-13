using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable


namespace CustomCADs.Modules.Accounts.Persistence.Migrations;

/// <inheritdoc />
public partial class Added_HeadDesigner_Account : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.UpdateData(
			schema: "Accounts",
			table: "Accounts",
			keyColumn: "Id",
			keyValue: new Guid("0fb3212f-7d51-4586-8fc2-0f333ec9fbc1"),
			column: "CreatedAt",
			value: new DateTimeOffset(new DateTime(2024, 3, 17, 2, 17, 32, 789, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)));

		migrationBuilder.InsertData(
			schema: "Accounts",
			table: "Accounts",
			columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "RoleName", "TrackViewedProducts", "Username" },
			values: new object[] { new Guid("8d477999-0580-4770-8864-e9ba4bed9cd1"), new DateTimeOffset(new DateTime(2025, 1, 9, 13, 15, 28, 789, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), "john.cad@gmail.com", null, null, "Designer", true, "John_CAD" });
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DeleteData(
			schema: "Accounts",
			table: "Accounts",
			keyColumn: "Id",
			keyValue: new Guid("8d477999-0580-4770-8864-e9ba4bed9cd1"));

		migrationBuilder.UpdateData(
			schema: "Accounts",
			table: "Accounts",
			keyColumn: "Id",
			keyValue: new Guid("0fb3212f-7d51-4586-8fc2-0f333ec9fbc1"),
			column: "CreatedAt",
			value: new DateTimeOffset(new DateTime(2025, 1, 9, 13, 15, 28, 789, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)));
	}
}
