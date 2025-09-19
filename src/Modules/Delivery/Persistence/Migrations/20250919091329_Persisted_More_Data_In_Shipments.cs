using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Delivery.Persistence.Migrations;

/// <inheritdoc />
public partial class Persisted_More_Data_In_Shipments : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<string>(
			name: "ReferenceId",
			schema: "Delivery",
			table: "Shipments",
			type: "text",
			nullable: true,
			oldClrType: typeof(string),
			oldType: "text");

		migrationBuilder.AddColumn<int>(
			name: "Count",
			schema: "Delivery",
			table: "Shipments",
			type: "integer",
			nullable: false,
			defaultValue: 0);

		migrationBuilder.AddColumn<string>(
			name: "Email",
			schema: "Delivery",
			table: "Shipments",
			type: "text",
			nullable: true);

		migrationBuilder.AddColumn<string>(
			name: "Phone",
			schema: "Delivery",
			table: "Shipments",
			type: "text",
			nullable: true);

		migrationBuilder.AddColumn<string>(
			name: "Recipient",
			schema: "Delivery",
			table: "Shipments",
			type: "text",
			nullable: false,
			defaultValue: "");

		migrationBuilder.AddColumn<string>(
			name: "Service",
			schema: "Delivery",
			table: "Shipments",
			type: "text",
			nullable: false,
			defaultValue: "");

		migrationBuilder.AddColumn<int>(
			name: "Status",
			schema: "Delivery",
			table: "Shipments",
			type: "integer",
			nullable: false,
			defaultValue: 0);

		migrationBuilder.AddColumn<double>(
			name: "Weight",
			schema: "Delivery",
			table: "Shipments",
			type: "double precision",
			nullable: false,
			defaultValue: 0.0);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "Count",
			schema: "Delivery",
			table: "Shipments");

		migrationBuilder.DropColumn(
			name: "Email",
			schema: "Delivery",
			table: "Shipments");

		migrationBuilder.DropColumn(
			name: "Phone",
			schema: "Delivery",
			table: "Shipments");

		migrationBuilder.DropColumn(
			name: "Recipient",
			schema: "Delivery",
			table: "Shipments");

		migrationBuilder.DropColumn(
			name: "Service",
			schema: "Delivery",
			table: "Shipments");

		migrationBuilder.DropColumn(
			name: "Status",
			schema: "Delivery",
			table: "Shipments");

		migrationBuilder.DropColumn(
			name: "Weight",
			schema: "Delivery",
			table: "Shipments");

		migrationBuilder.AlterColumn<string>(
			name: "ReferenceId",
			schema: "Delivery",
			table: "Shipments",
			type: "text",
			nullable: false,
			defaultValue: "",
			oldClrType: typeof(string),
			oldType: "text",
			oldNullable: true);
	}
}
