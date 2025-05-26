using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechMesh.Domain.Entities;
using TechMesh.User.Domain.Entities;

namespace TechMesh.User.Infrastructure.Mappings;

public class TechnologyMapping : IEntityTypeConfiguration<Technology>
{
    public void Configure(EntityTypeBuilder<Technology> builder)
    {
        builder.ToTable("technologies", "user");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

        builder.OwnsOne(t => t.Name, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName(nameof(Technology.Name))
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(128);
        });

        builder.Property(u => u.CreatedAt)
            .HasColumnName(nameof(Entity.CreatedAt))
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .HasColumnName(nameof(Entity.UpdatedAt))
            .IsRequired();
    }
}