namespace TechMesh.User.Infrastructure.Mappings;

public class UserTechnologyMapping : IEntityTypeConfiguration<UserTechnology>
{
    public void Configure(EntityTypeBuilder<UserTechnology> builder)
    {
        builder.ToTable("users_technologies", "user");

        builder.HasKey(ut => new { ut.UserId, ut.TechnologyId });

        builder.Property(ut => ut.UserId).HasColumnName("user_id");

        builder.Property(ut => ut.TechnologyId).HasColumnName("technology_id");

        builder.HasOne(ut => ut.User)
            .WithMany(ut => ut.Technologies)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ut => ut.Technology)
            .WithMany(ut => ut.Users)
            .HasForeignKey(u => u.TechnologyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}