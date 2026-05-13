using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Modules.Accounts.Persistence.Migrations;

/// <inheritdoc />
public partial class Added_ViewedProducts_ViewedAt : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<DateTimeOffset>(
			name: "ViewedAt",
			schema: "Accounts",
			table: "ViewedProducts",
			type: "timestamp with time zone",
			nullable: false,
			defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "ViewedAt",
			schema: "Accounts",
			table: "ViewedProducts");
	}
}
