namespace TechMesh.User.Infrastructure.Mappings;

public class UserMapping : IEntityTypeConfiguration<Domain.Entities.User>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.User> builder)
    {
        builder.ToTable("users", "user");

        builder.HasKey(u => u.Id);

        builder.OwnsOne(u => u.FullName, fullName =>
        {
            fullName.Property(fn => fn.Value)
                .HasColumnName(nameof(FullName))
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(128);
        });

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Address)
                .HasColumnName(nameof(Email))
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(256);
        });

        builder.OwnsOne(u => u.BirthDate, birthDate =>
        {
            birthDate.Property(b => b.Value)
                .HasColumnName(nameof(BirthDate))
                .HasColumnType("TIMESTAMP")
                .IsRequired();
        });

        builder.OwnsOne(u => u.PhoneNumber, phoneNumber =>
        {
            phoneNumber.Property(pn => pn.Value)
                .HasColumnName(nameof(PhoneNumber))
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(11);
        });

        builder.OwnsOne(u => u.Address, address =>
        {
            address.Property(a => a.Street)
                .HasColumnName(nameof(Address.Street))
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(128);

            address.Property(a => a.Neighborhood)
                .HasColumnName(nameof(Address.Neighborhood))
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(128);

            address.Property(a => a.City)
                .HasColumnName(nameof(Address.City))
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(128);

            address.Property(a => a.Country)
                .HasColumnName(nameof(Address.Country))
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(128);

            address.Property(a => a.Number)
                .HasColumnName(nameof(Address.Number))
                .HasColumnType("INTEGER")
                .IsRequired();

            address.Property(a => a.State)
                .HasColumnName(nameof(Address.State))
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(50);

            address.Property(a => a.ZipCode)
                .HasColumnName(nameof(Address.ZipCode))
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(50);

            address.Property(a => a.Complement)
                .HasColumnName(nameof(Address.Complement))
                .HasColumnType("VARCHAR")
                .IsRequired()
                .HasMaxLength(50);
        });

        builder.Property(u => u.Status)
            .HasColumnName(nameof(Domain.Entities.User.Status))
            .HasConversion<string>()
            .IsRequired();

        builder.Property(u => u.Level)
            .HasColumnName(nameof(Domain.Entities.User.Level))
            .HasConversion<string>()
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .HasColumnName(nameof(Entity.CreatedAt))
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .HasColumnName(nameof(Entity.UpdatedAt))
            .IsRequired(false);

        builder.Ignore(u => u.Events);
    }
}