namespace TechMesh.Auth.Infrastructure.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Token> Tokens { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        modelBuilder.Entity<User>(config =>
        {
            config.HasOne(u => u.Role)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}