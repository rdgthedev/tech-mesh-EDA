namespace TechMesh.User.Infrastructure.Context;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(UserMapping).Assembly);
    }

    public DbSet<Domain.Entities.User> Users { get; set; }
    public DbSet<Technology> Technologies { get; set; }
    public DbSet<UserTechnology> UserTechnologies { get; set; }
}