using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Infrastructure.Data.Configurations
{
    public class HomePageConfiguration : IEntityTypeConfiguration<HomePage>
    {
        public void Configure(EntityTypeBuilder<HomePage> builder)
        {
            builder.ToTable("HomePages");

            builder.Property(x => x.HeroTitle)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.HeroSubtitle)
                .HasMaxLength(300);

            builder.Property(x => x.HeroButtonText)
                .HasMaxLength(100);

            builder.Property(x => x.HeroButtonUrl)
                .HasMaxLength(500);

            builder.Property(x => x.HeroImageUrl)
                .HasMaxLength(500);

            builder.Property(x => x.AboutTitle)
                .HasMaxLength(200);

            builder.Property(x => x.AboutSubtitle)
                .HasMaxLength(300);

            builder.Property(x => x.AboutHtml)
                .HasMaxLength(5000);

            builder.Property(x => x.AboutImageUrl)
                .HasMaxLength(500);

            builder.Property(x => x.Highlight1Title).HasMaxLength(150);
            builder.Property(x => x.Highlight1Text).HasMaxLength(500);
            builder.Property(x => x.Highlight1Icon).HasMaxLength(100);

            builder.Property(x => x.Highlight2Title).HasMaxLength(150);
            builder.Property(x => x.Highlight2Text).HasMaxLength(500);
            builder.Property(x => x.Highlight2Icon).HasMaxLength(100);

            builder.Property(x => x.Highlight3Title).HasMaxLength(150);
            builder.Property(x => x.Highlight3Text).HasMaxLength(500);
            builder.Property(x => x.Highlight3Icon).HasMaxLength(100);

            builder.Property(x => x.SeoTitle).HasMaxLength(60);
            builder.Property(x => x.SeoDescription).HasMaxLength(160);
            builder.Property(x => x.OgImageUrl).HasMaxLength(500);

            builder.Property(x => x.LastUpdatedAt)
                .IsRequired();

            builder.Property(x => x.LastUpdatedBy)
                .HasMaxLength(100);
        }
    }
}
