namespace TechMesh.User.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;
        public UserRepository(UserDbContext context) => _context = context;

        public async Task CreateAsync(Domain.Entities.User user, CancellationToken cancellationToken)
        {
            var entry = await _context.Users.AddAsync(user, cancellationToken);
        }

        public async Task<Domain.Entities.User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
            => await _context
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    u => u.Email.Address.ToUpper() == email.ToUpper(), cancellationToken);
    }
}