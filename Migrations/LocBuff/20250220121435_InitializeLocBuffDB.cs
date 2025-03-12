using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraitBrowserWebApp.Migrations.LocBuff
{
    /// <inheritdoc />
    public partial class InitializeLocBuffDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocBuff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    SChinese = table.Column<string>(type: "TEXT", nullable: true),
                    TChinese = table.Column<string>(type: "TEXT", nullable: true),
                    English = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocBuff", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocBuff");
        }
    }
}
