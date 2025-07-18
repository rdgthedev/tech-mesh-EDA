﻿namespace TechMesh.User.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task CreateAsync(Entities.User user, CancellationToken cancellationToken);
    Task<Entities.User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}