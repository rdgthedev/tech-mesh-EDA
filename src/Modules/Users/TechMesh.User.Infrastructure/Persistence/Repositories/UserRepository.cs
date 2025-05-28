using TechMesh.User.Domain.Interfaces.Repositories;
using TechMesh.User.Infrastructure.Context;

namespace TechMesh.User.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;
        public UserRepository(UserDbContext context) => _context = context;

        public async Task CreateAsync(Domain.Entities.User user, CancellationToken cancellationToken)
           => await _context.AddAsync(user, cancellationToken);
    }
}
