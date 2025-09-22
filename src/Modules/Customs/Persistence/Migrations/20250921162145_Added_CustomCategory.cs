using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Customs.Persistence.Migrations;

/// <inheritdoc />
public partial class Added_CustomCategory : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "CustomCategory",
			schema: "Customs",
			columns: table => new
			{
				Id = table.Column<int>(type: "integer", nullable: false),
				CustomId = table.Column<Guid>(type: "uuid", nullable: false),
				SetAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
				Setter = table.Column<string>(type: "text", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_CustomCategory", x => new { x.Id, x.CustomId });
				table.ForeignKey(
					name: "FK_CustomCategory_Customs_CustomId",
					column: x => x.CustomId,
					principalSchema: "Customs",
					principalTable: "Customs",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateIndex(
			name: "IX_CustomCategory_CustomId",
			schema: "Customs",
			table: "CustomCategory",
			column: "CustomId",
			unique: true);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "CustomCategory",
			schema: "Customs");
	}
}
