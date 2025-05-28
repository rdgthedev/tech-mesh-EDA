namespace TechMesh.User.Infrastructure.Mappings;

public class UserTechnologyMapping : IEntityTypeConfiguration<UserTechnology>
{
    public void Configure(EntityTypeBuilder<UserTechnology> builder)
    {
        builder.ToTable("UserTechnology", "user");

        builder.HasKey(ut => new { ut.UserId, ut.TechnologyId });

        builder.HasOne(u => u.User)
            .WithMany(u => u.Technologies)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.Technology)
            .WithMany(u => u.Users)
            .HasForeignKey(u => u.TechnologyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}