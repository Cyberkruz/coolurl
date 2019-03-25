using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Metamask.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PageMasks",
                columns: table => new
                {
                    PageMaskId = table.Column<Guid>(nullable: false),
                    TargetUrl = table.Column<string>(maxLength: 2083, nullable: false),
                    Title = table.Column<string>(maxLength: 60, nullable: true),
                    Description = table.Column<string>(maxLength: 160, nullable: true),
                    Image = table.Column<string>(maxLength: 2083, nullable: true),
                    CreateDateUtc = table.Column<DateTime>(nullable: false),
                    UpdateDateUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageMasks", x => x.PageMaskId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageMasks");
        }
    }
}
