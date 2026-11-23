using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Modules.Delivery.Persistence.Migrations;

/// <inheritdoc />
public partial class Required_Phone_And_Email : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<string>(
			name: "Phone",
			schema: "Delivery",
			table: "Shipments",
			type: "text",
			nullable: false,
			defaultValue: "",
			oldClrType: typeof(string),
			oldType: "text",
			oldNullable: true);

		migrationBuilder.AlterColumn<string>(
			name: "Email",
			schema: "Delivery",
			table: "Shipments",
			type: "text",
			nullable: false,
			defaultValue: "",
			oldClrType: typeof(string),
			oldType: "text",
			oldNullable: true);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<string>(
			name: "Phone",
			schema: "Delivery",
			table: "Shipments",
			type: "text",
			nullable: true,
			oldClrType: typeof(string),
			oldType: "text");

		migrationBuilder.AlterColumn<string>(
			name: "Email",
			schema: "Delivery",
			table: "Shipments",
			type: "text",
			nullable: true,
			oldClrType: typeof(string),
			oldType: "text");
	}
}
