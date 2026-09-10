using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Modules.Printing.Persistence.Migrations;

/// <inheritdoc />
public partial class Improved_Volume_Accuracy : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<decimal>(
			name: "Volume",
			schema: "Printing",
			table: "Customizations",
			type: "numeric(28,17)",
			precision: 28,
			scale: 17,
			nullable: false,
			comment: "Arbitrary value without measuring unit",
			oldClrType: typeof(decimal),
			oldType: "numeric(18,2)",
			oldPrecision: 18,
			oldScale: 2,
			oldComment: "Measured in m³");

		migrationBuilder.AlterColumn<decimal>(
			name: "Scale",
			schema: "Printing",
			table: "Customizations",
			type: "numeric(28,17)",
			precision: 28,
			scale: 17,
			nullable: false,
			comment: "Floating number representing a percentage",
			oldClrType: typeof(decimal),
			oldType: "numeric(4,2)",
			oldPrecision: 4,
			oldScale: 2,
			oldComment: "Floating number representing a percentage");

		migrationBuilder.AddColumn<decimal>(
			name: "Base",
			schema: "Printing",
			table: "Customizations",
			type: "numeric(28,17)",
			precision: 28,
			scale: 17,
			nullable: false,
			defaultValue: 0m,
			comment: "Normalization factor to help convert the Volume into mm³");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "Base",
			schema: "Printing",
			table: "Customizations");

		migrationBuilder.AlterColumn<decimal>(
			name: "Volume",
			schema: "Printing",
			table: "Customizations",
			type: "numeric(18,2)",
			precision: 18,
			scale: 2,
			nullable: false,
			comment: "Measured in m³",
			oldClrType: typeof(decimal),
			oldType: "numeric(28,17)",
			oldPrecision: 28,
			oldScale: 17,
			oldComment: "Arbitrary value without measuring unit");

		migrationBuilder.AlterColumn<decimal>(
			name: "Scale",
			schema: "Printing",
			table: "Customizations",
			type: "numeric(4,2)",
			precision: 4,
			scale: 2,
			nullable: false,
			comment: "Floating number representing a percentage",
			oldClrType: typeof(decimal),
			oldType: "numeric(28,17)",
			oldPrecision: 28,
			oldScale: 17,
			oldComment: "Floating number representing a percentage");
	}
}
