using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertBoard.Hosts.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class Added_Index_Category_Name_Unique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Category_Title",
                table: "Category",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Category_Title",
                table: "Category");
        }
    }
}
