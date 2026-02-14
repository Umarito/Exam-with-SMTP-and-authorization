using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.HasKey(g => g.Id);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(80);
        builder.Property(a=>a.CreatedAt).HasDefaultValueSql("NOW()");
    }
}