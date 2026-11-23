using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Modules.Delivery.Persistence.Migrations;

/// <inheritdoc />
public partial class Updated_Shipemnts_Address : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "City",
			schema: "Delivery",
			table: "Shipments");

		migrationBuilder.DropColumn(
			name: "Street",
			schema: "Delivery",
			table: "Shipments");

		migrationBuilder.AlterColumn<string>(
			name: "Country",
			schema: "Delivery",
			table: "Shipments",
			type: "text",
			nullable: true,
			oldClrType: typeof(string),
			oldType: "text");

		migrationBuilder.AddColumn<string>(
			name: "AddressNote",
			schema: "Delivery",
			table: "Shipments",
			type: "text",
			nullable: true);

		migrationBuilder.AddColumn<int>(
			name: "Destination",
			schema: "Delivery",
			table: "Shipments",
			type: "integer",
			nullable: false,
			defaultValue: 0);

		migrationBuilder.AddColumn<double>(
			name: "Latitude",
			schema: "Delivery",
			table: "Shipments",
			type: "double precision",
			precision: 10,
			scale: 7,
			nullable: true);

		migrationBuilder.AddColumn<double>(
			name: "Longitude",
			schema: "Delivery",
			table: "Shipments",
			type: "double precision",
			precision: 10,
			scale: 7,
			nullable: true);

		migrationBuilder.AddColumn<int>(
			name: "OfficeId",
			schema: "Delivery",
			table: "Shipments",
			type: "integer",
			nullable: true);

		migrationBuilder.AddColumn<string>(
			name: "Site",
			schema: "Delivery",
			table: "Shipments",
			type: "text",
			nullable: true);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "AddressNote",
			schema: "Delivery",
			table: "Shipments");

		migrationBuilder.DropColumn(
			name: "Destination",
			schema: "Delivery",
			table: "Shipments");

		migrationBuilder.DropColumn(
			name: "Latitude",
			schema: "Delivery",
			table: "Shipments");

		migrationBuilder.DropColumn(
			name: "Longitude",
			schema: "Delivery",
			table: "Shipments");

		migrationBuilder.DropColumn(
			name: "OfficeId",
			schema: "Delivery",
			table: "Shipments");

		migrationBuilder.DropColumn(
			name: "Site",
			schema: "Delivery",
			table: "Shipments");

		migrationBuilder.AlterColumn<string>(
			name: "Country",
			schema: "Delivery",
			table: "Shipments",
			type: "text",
			nullable: false,
			defaultValue: "",
			oldClrType: typeof(string),
			oldType: "text",
			oldNullable: true);

		migrationBuilder.AddColumn<string>(
			name: "City",
			schema: "Delivery",
			table: "Shipments",
			type: "text",
			nullable: false,
			defaultValue: "");

		migrationBuilder.AddColumn<string>(
			name: "Street",
			schema: "Delivery",
			table: "Shipments",
			type: "text",
			nullable: false,
			defaultValue: "");
	}
}
