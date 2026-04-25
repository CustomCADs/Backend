using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Modules.Accounts.Persistence.Migrations;

/// <inheritdoc />
public partial class Implemented_Accounts_SoftDelete : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<DateTimeOffset>(
			name: "DeletedAt",
			schema: "Accounts",
			table: "Accounts",
			type: "timestamp with time zone",
			nullable: true);

		migrationBuilder.AddColumn<bool>(
			name: "IsDeleted",
			schema: "Accounts",
			table: "Accounts",
			type: "boolean",
			nullable: false,
			defaultValue: false);

		migrationBuilder.UpdateData(
			schema: "Accounts",
			table: "Accounts",
			keyColumn: "Id",
			keyValue: new Guid("0fb3212f-7d51-4586-8fc2-0f333ec9fbc1"),
			columns: new[] { "DeletedAt", "IsDeleted" },
			values: new object[] { null, false });

		migrationBuilder.UpdateData(
			schema: "Accounts",
			table: "Accounts",
			keyColumn: "Id",
			keyValue: new Guid("2da61b05-1a27-4af9-9df2-be4f1f4e835f"),
			columns: new[] { "DeletedAt", "IsDeleted" },
			values: new object[] { null, false });

		migrationBuilder.UpdateData(
			schema: "Accounts",
			table: "Accounts",
			keyColumn: "Id",
			keyValue: new Guid("6d963818-23dc-4e9a-aaa8-b4c77252bc97"),
			columns: new[] { "DeletedAt", "IsDeleted" },
			values: new object[] { null, false });

		migrationBuilder.UpdateData(
			schema: "Accounts",
			table: "Accounts",
			keyColumn: "Id",
			keyValue: new Guid("8d477999-0580-4770-8864-e9ba4bed9cd1"),
			columns: new[] { "DeletedAt", "IsDeleted" },
			values: new object[] { null, false });

		migrationBuilder.UpdateData(
			schema: "Accounts",
			table: "Accounts",
			keyColumn: "Id",
			keyValue: new Guid("e995039c-a535-4f20-8288-7aadcb71b252"),
			columns: new[] { "DeletedAt", "IsDeleted" },
			values: new object[] { null, false });
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "DeletedAt",
			schema: "Accounts",
			table: "Accounts");

		migrationBuilder.DropColumn(
			name: "IsDeleted",
			schema: "Accounts",
			table: "Accounts");
	}
}
