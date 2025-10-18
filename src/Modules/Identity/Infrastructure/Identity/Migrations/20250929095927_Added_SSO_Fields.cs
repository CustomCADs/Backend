using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable


namespace CustomCADs.Identity.Infrastructure.Identity.Migrations;

/// <inheritdoc />
public partial class Added_SSO_Fields : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<bool>(
			name: "IsSSO",
			schema: "Identity",
			table: "AspNetUsers",
			type: "boolean",
			nullable: false,
			defaultValue: false);

		migrationBuilder.AddColumn<string>(
			name: "Provider",
			schema: "Identity",
			table: "AspNetUsers",
			type: "text",
			nullable: true);

		migrationBuilder.UpdateData(
			schema: "Identity",
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("4337a774-2c5c-4c27-d28b-08dd11623eb9"),
			columns: new[] { "IsSSO", "Provider" },
			values: new object[] { false, null });

		migrationBuilder.UpdateData(
			schema: "Identity",
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("af840410-f3f2-4a3b-d28a-08dd11623eb9"),
			columns: new[] { "IsSSO", "Provider" },
			values: new object[] { false, null });

		migrationBuilder.UpdateData(
			schema: "Identity",
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("cb7749fb-3fff-4902-d28c-08dd11623eb9"),
			columns: new[] { "IsSSO", "Provider" },
			values: new object[] { false, null });

		migrationBuilder.UpdateData(
			schema: "Identity",
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("e38c495f-b1f3-4226-d289-08dd11623eb9"),
			columns: new[] { "IsSSO", "Provider" },
			values: new object[] { false, null });
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "IsSSO",
			schema: "Identity",
			table: "AspNetUsers");

		migrationBuilder.DropColumn(
			name: "Provider",
			schema: "Identity",
			table: "AspNetUsers");
	}
}
