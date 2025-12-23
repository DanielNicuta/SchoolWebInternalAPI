using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Infrastructure.Data.Configurations;

public class ContactPageConfiguration : IEntityTypeConfiguration<ContactPage>
{
    public void Configure(EntityTypeBuilder<ContactPage> builder)
    {
        builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Subtitle).HasMaxLength(300);

        builder.Property(p => p.Address).HasMaxLength(300);
        builder.Property(p => p.Phone).HasMaxLength(100);
        builder.Property(p => p.Email).HasMaxLength(200);

        builder.Property(p => p.SeoTitle).HasMaxLength(200);
        builder.Property(p => p.SeoDescription).HasMaxLength(300);
        builder.Property(p => p.OgImageUrl).HasMaxLength(500);
    }
}
