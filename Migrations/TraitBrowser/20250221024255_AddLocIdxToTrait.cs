using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraitBrowserWebApp.Migrations.TraitBrowser
{
    /// <inheritdoc />
    public partial class AddLocIdxToTrait : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocName",
                table: "Trait",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "locInfo",
                table: "Trait",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocName",
                table: "Trait");

            migrationBuilder.DropColumn(
                name: "locInfo",
                table: "Trait");
        }
    }
}
