using Microsoft.EntityFrameworkCore;
using TechMesh.User.Domain.Entities;

namespace TechMesh.User.Infrastructure.Context;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Entities.User> Users { get; set; }
    public DbSet<Technology> Technologies { get; set; }
    public DbSet<UserTechnology> UserTechnologies { get; set; }
}