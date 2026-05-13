using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Modules.Identity.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Added_RefreshToken_Fingerprints : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<string>(
			name: "Device",
			schema: "Identity",
			table: "AppRefreshToken",
			type: "text",
			nullable: false,
			defaultValue: "");

		migrationBuilder.AddColumn<string>(
			name: "Location",
			schema: "Identity",
			table: "AppRefreshToken",
			type: "text",
			nullable: true);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "Device",
			schema: "Identity",
			table: "AppRefreshToken");

		migrationBuilder.DropColumn(
			name: "Location",
			schema: "Identity",
			table: "AppRefreshToken");
	}
}
