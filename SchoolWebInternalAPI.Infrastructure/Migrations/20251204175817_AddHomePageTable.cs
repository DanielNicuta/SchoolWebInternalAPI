using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolWebInternalAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHomePageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactPageContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Subtitle = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    MapEmbedUrl = table.Column<string>(type: "TEXT", nullable: true),
                    InfoHtml = table.Column<string>(type: "TEXT", nullable: true),
                    SeoTitle = table.Column<string>(type: "TEXT", nullable: true),
                    SeoDescription = table.Column<string>(type: "TEXT", nullable: true),
                    OgImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPageContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FooterContents",
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
                    table.PrimaryKey("PK_FooterContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoryPageContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Subtitle = table.Column<string>(type: "TEXT", nullable: true),
                    ContentHtml = table.Column<string>(type: "TEXT", nullable: true),
                    SideImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    SeoTitle = table.Column<string>(type: "TEXT", nullable: true),
                    SeoDescription = table.Column<string>(type: "TEXT", nullable: true),
                    OgImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryPageContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomePageContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HeroTitle = table.Column<string>(type: "TEXT", nullable: false),
                    HeroSubtitle = table.Column<string>(type: "TEXT", nullable: true),
                    HeroButtonText = table.Column<string>(type: "TEXT", nullable: true),
                    HeroButtonUrl = table.Column<string>(type: "TEXT", nullable: true),
                    HeroImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    AboutTitle = table.Column<string>(type: "TEXT", nullable: true),
                    AboutSubtitle = table.Column<string>(type: "TEXT", nullable: true),
                    AboutHtml = table.Column<string>(type: "TEXT", nullable: true),
                    AboutImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Highlight1Title = table.Column<string>(type: "TEXT", nullable: true),
                    Highlight1Text = table.Column<string>(type: "TEXT", nullable: true),
                    Highlight1Icon = table.Column<string>(type: "TEXT", nullable: true),
                    Highlight2Title = table.Column<string>(type: "TEXT", nullable: true),
                    Highlight2Text = table.Column<string>(type: "TEXT", nullable: true),
                    Highlight2Icon = table.Column<string>(type: "TEXT", nullable: true),
                    Highlight3Title = table.Column<string>(type: "TEXT", nullable: true),
                    Highlight3Text = table.Column<string>(type: "TEXT", nullable: true),
                    Highlight3Icon = table.Column<string>(type: "TEXT", nullable: true),
                    SeoTitle = table.Column<string>(type: "TEXT", nullable: true),
                    SeoDescription = table.Column<string>(type: "TEXT", nullable: true),
                    OgImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePageContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomePages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HeroTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    HeroSubtitle = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    HeroButtonText = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    HeroButtonUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    HeroImageUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    AboutTitle = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    AboutSubtitle = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    AboutHtml = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: true),
                    AboutImageUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Highlight1Title = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    Highlight1Text = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Highlight1Icon = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Highlight2Title = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    Highlight2Text = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Highlight2Icon = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Highlight3Title = table.Column<string>(type: "TEXT", maxLength: 150, nullable: true),
                    Highlight3Text = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Highlight3Icon = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    SeoTitle = table.Column<string>(type: "TEXT", maxLength: 60, nullable: true),
                    SeoDescription = table.Column<string>(type: "TEXT", maxLength: 160, nullable: true),
                    OgImageUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    LastUpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    IsPublished = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinksPageContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    IntroHtml = table.Column<string>(type: "TEXT", nullable: true),
                    LinksHtml = table.Column<string>(type: "TEXT", nullable: true),
                    SeoTitle = table.Column<string>(type: "TEXT", nullable: true),
                    SeoDescription = table.Column<string>(type: "TEXT", nullable: true),
                    OgImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinksPageContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MissionPageContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    IntroHtml = table.Column<string>(type: "TEXT", nullable: true),
                    MissionHtml = table.Column<string>(type: "TEXT", nullable: true),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    SeoTitle = table.Column<string>(type: "TEXT", nullable: true),
                    SeoDescription = table.Column<string>(type: "TEXT", nullable: true),
                    OgImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionPageContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationPageContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    DescriptionHtml = table.Column<string>(type: "TEXT", nullable: true),
                    OrgChartImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    OrgChartFileUrl = table.Column<string>(type: "TEXT", nullable: true),
                    SeoTitle = table.Column<string>(type: "TEXT", nullable: true),
                    SeoDescription = table.Column<string>(type: "TEXT", nullable: true),
                    OgImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationPageContents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SiteName = table.Column<string>(type: "TEXT", nullable: false),
                    LogoUrl = table.Column<string>(type: "TEXT", nullable: true),
                    FaviconUrl = table.Column<string>(type: "TEXT", nullable: true),
                    DefaultLanguage = table.Column<string>(type: "TEXT", nullable: true),
                    ContactEmail = table.Column<string>(type: "TEXT", nullable: true),
                    ContactPhone = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    FacebookUrl = table.Column<string>(type: "TEXT", nullable: true),
                    YoutubeUrl = table.Column<string>(type: "TEXT", nullable: true),
                    TwitterUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CookieBannerText = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: true),
                    Bio = table.Column<string>(type: "TEXT", nullable: true),
                    PhotoUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeachersPageContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PageTitle = table.Column<string>(type: "TEXT", nullable: false),
                    PageSubtitle = table.Column<string>(type: "TEXT", nullable: true),
                    IntroHtml = table.Column<string>(type: "TEXT", nullable: true),
                    HeroImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    SeoTitle = table.Column<string>(type: "TEXT", nullable: true),
                    SeoDescription = table.Column<string>(type: "TEXT", nullable: true),
                    OgImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachersPageContents", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactPageContents");

            migrationBuilder.DropTable(
                name: "FooterContents");

            migrationBuilder.DropTable(
                name: "HistoryPageContents");

            migrationBuilder.DropTable(
                name: "HomePageContents");

            migrationBuilder.DropTable(
                name: "HomePages");

            migrationBuilder.DropTable(
                name: "LinksPageContents");

            migrationBuilder.DropTable(
                name: "MissionPageContents");

            migrationBuilder.DropTable(
                name: "OrganizationPageContents");

            migrationBuilder.DropTable(
                name: "SiteSettings");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "TeachersPageContents");
        }
    }
}
