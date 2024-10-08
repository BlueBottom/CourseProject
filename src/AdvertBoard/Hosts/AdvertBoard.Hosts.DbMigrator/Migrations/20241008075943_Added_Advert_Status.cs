using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdvertBoard.Hosts.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class Added_Advert_Status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Advert");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Advert",
                type: "integer",
                nullable: false,
                defaultValue: 3);

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Undefined" },
                    { 1, "Draft" },
                    { 2, "InChecking" },
                    { 3, "Published" },
                    { 4, "Archived" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Advert");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Advert",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
