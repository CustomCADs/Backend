using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable


namespace CustomCADs.Modules.Notifications.Persistence.Migrations;

/// <inheritdoc />
public partial class Initial_Migration : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.EnsureSchema(
			name: "Notifications");

		migrationBuilder.CreateTable(
			name: "Notifications",
			schema: "Notifications",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uuid", nullable: false),
				Type = table.Column<string>(type: "text", nullable: false),
				Status = table.Column<string>(type: "text", nullable: false),
				CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
				AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
				ReceiverId = table.Column<Guid>(type: "uuid", nullable: false),
				Description = table.Column<string>(type: "text", nullable: false),
				Link = table.Column<string>(type: "text", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Notifications", x => x.Id);
			});
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "Notifications",
			schema: "Notifications");
	}
}
