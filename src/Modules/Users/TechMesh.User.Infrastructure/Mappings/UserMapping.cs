namespace TechMesh.User.Infrastructure.Mappings;

public class UserMapping : IEntityTypeConfiguration<Domain.Entities.User>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
    {
        builder.ToTable("users", "user");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnName("id");

        builder.OwnsOne(u => u.FullName, fullName =>
        {
            fullName.Property(fn => fn.Value)
                .HasColumnName("full_name")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(128);
        });

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Address)
                .HasColumnName("email")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(256);
        });

        builder.OwnsOne(u => u.BirthDate, birthDate =>
        {
            birthDate.Property(b => b.Value)
                .HasColumnName("birth_date")
                .HasColumnType("TIMESTAMP")
                .IsRequired();
        });

        builder.OwnsOne(u => u.PhoneNumber, phoneNumber =>
        {
            phoneNumber.Property(pn => pn.Value)
                .HasColumnName("phone_number")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(11);
        });

        builder.OwnsOne(u => u.Address, address =>
        {
            address.Property(a => a.Street)
                .HasColumnName("street")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(128);

            address.Property(a => a.Neighborhood)
                .HasColumnName("neighborhood")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(128);

            address.Property(a => a.City)
                .HasColumnName("city")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(128);

            address.Property(a => a.Country)
                .HasColumnName("country")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(128);

            address.Property(a => a.Number)
                .HasColumnName("number")
                .HasColumnType("INTEGER")
                .IsRequired();

            address.Property(a => a.State)
                .HasColumnName("state")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(50);

            address.Property(a => a.ZipCode)
                .HasColumnName("zip_code")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(50);

            address.Property(a => a.Complement)
                .HasColumnName("complement")
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(50);
        });

        builder.Property(u => u.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(u => u.Level)
            .HasColumnName("level")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("TIMESTAMP")
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("TIMESTAMP")
            .IsRequired(false);

        builder.Property(u => u.DeletedAt)
            .HasColumnName("deleted_at")
            .HasColumnType("TIMESTAMP")
            .IsRequired(false);

        builder.Ignore(u => u.Events);
    }
}