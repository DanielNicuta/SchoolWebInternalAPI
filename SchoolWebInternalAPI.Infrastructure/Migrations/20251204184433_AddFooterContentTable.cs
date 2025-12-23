using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolWebInternalAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFooterContentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FooterPage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FooterText = table.Column<string>(type: "TEXT", nullable: true),
                    UsefulLinksJson = table.Column<string>(type: "TEXT", nullable: true),
                    SocialLinksJson = table.Column<string>(type: "TEXT", nullable: true),
                    NewsletterTitle = table.Column<string>(type: "TEXT", nullable: true),
                    NewsletterText = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterPage", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FooterPage");
        }
    }
}
