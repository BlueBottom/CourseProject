using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertBoard.Hosts.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class Added_Rating_Address : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Image");

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "User",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Advert",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Advert");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Image",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
