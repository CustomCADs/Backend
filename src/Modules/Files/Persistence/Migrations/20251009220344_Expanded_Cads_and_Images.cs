using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Modules.Files.Persistence.Migrations;

/// <inheritdoc />
public partial class Expanded_Cads_and_Images : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<Guid>(
			name: "OwnerId",
			schema: "Files",
			table: "Images",
			type: "uuid",
			nullable: false,
			defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

		migrationBuilder.AddColumn<Guid>(
			name: "OwnerId",
			schema: "Files",
			table: "Cads",
			type: "uuid",
			nullable: false,
			defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

		migrationBuilder.UpdateData(
			schema: "Files",
			table: "Images",
			keyColumn: "Id",
			keyValue: new Guid("190a69a3-1b02-43f0-a4f9-cab22826abf3"),
			column: "OwnerId",
			value: new Guid("e995039c-a535-4f20-8288-7aadcb71b252"));

		migrationBuilder.UpdateData(
			schema: "Files",
			table: "Images",
			keyColumn: "Id",
			keyValue: new Guid("38deab9b-8791-4147-9958-64e9f7ec6d78"),
			column: "OwnerId",
			value: new Guid("e995039c-a535-4f20-8288-7aadcb71b252"));

		migrationBuilder.UpdateData(
			schema: "Files",
			table: "Images",
			keyColumn: "Id",
			keyValue: new Guid("3fe2472c-d2c6-434c-a013-ef117319bed3"),
			column: "OwnerId",
			value: new Guid("e995039c-a535-4f20-8288-7aadcb71b252"));

		migrationBuilder.UpdateData(
			schema: "Files",
			table: "Images",
			keyColumn: "Id",
			keyValue: new Guid("9a35cbea-806c-4561-ae71-bb21824f2432"),
			column: "OwnerId",
			value: new Guid("e995039c-a535-4f20-8288-7aadcb71b252"));

		migrationBuilder.UpdateData(
			schema: "Files",
			table: "Images",
			keyColumn: "Id",
			keyValue: new Guid("bed27a31-107a-4b3f-a50a-cb9cc6f376f1"),
			column: "OwnerId",
			value: new Guid("e995039c-a535-4f20-8288-7aadcb71b252"));
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "OwnerId",
			schema: "Files",
			table: "Images");

		migrationBuilder.DropColumn(
			name: "OwnerId",
			schema: "Files",
			table: "Cads");
	}
}
