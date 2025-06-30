namespace TechMesh.User.Infrastructure.Mappings;

public class TechnologyMapping : IEntityTypeConfiguration<Technology>
{
    public void Configure(EntityTypeBuilder<Technology> builder)
    {
        builder.ToTable("technologies", "user");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("id");

        builder.OwnsOne(t => t.Name, name =>
        {
            name.Property(n => n.Value)
                .HasColumnName("name")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(128);
        });

        builder.Property(t => t.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("TIMESTAMP")
            .IsRequired();

        builder.Property(t => t.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("TIMESTAMP")
            .IsRequired(false);
        
        builder.Property(t => t.DeletedAt)
            .HasColumnName("deleted_at")
            .HasColumnType("TIMESTAMP")
            .IsRequired(false);

        builder.Ignore(t => t.Events);
    }
}