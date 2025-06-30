namespace TechMesh.User.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;
        public UserRepository(UserDbContext context) => _context = context;

        public async Task CreateAsync(Domain.Entities.User user, CancellationToken cancellationToken)
            => await _context.AddAsync(user, cancellationToken);

        public async Task<Domain.Entities.User?> GetByEmail(string email, CancellationToken cancellationToken)
            => await _context.Users.FirstOrDefaultAsync(
                u => u.Email.Address.ToUpper() == email.ToUpper(), cancellationToken);
    }
}