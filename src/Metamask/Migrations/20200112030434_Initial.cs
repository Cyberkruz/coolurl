using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Metamask.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "page_masks",
                columns: table => new
                {
                    page_mask_id = table.Column<Guid>(nullable: false),
                    target_url = table.Column<string>(maxLength: 2083, nullable: false),
                    title = table.Column<string>(maxLength: 60, nullable: true),
                    description = table.Column<string>(maxLength: 160, nullable: true),
                    image = table.Column<string>(maxLength: 2083, nullable: true),
                    create_date_utc = table.Column<DateTime>(nullable: false),
                    update_date_utc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_page_masks", x => x.page_mask_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "page_masks");
        }
    }
}
