namespace TechMesh.Auth.Application.Mappers;

public static class Mapper
{
    public static List<RoleResponse> Map(List<Role> roles)
        => roles.Select(r => new RoleResponse(r.Id, r.Name, r.Status)).ToList();

    public static RoleResponse Map(Role role) => new(role.Id, role.Name, role.Status);

    public static List<UserDetailsResponse> Map(List<User> users)
        => users
            .Select(u => new UserDetailsResponse(u.Id, u.Email, u.Password, u.Role))
            .ToList();

    public static UserDetailsResponse Map(User user) => new(user.Id, user.Email, user.Password, user.Role);
    
    public static TokenResponse? Map(Token? token)
        => token is null
            ? null
            : new TokenResponse(token.Value.ToString(), token.ExpirationTime, token.Type);
}