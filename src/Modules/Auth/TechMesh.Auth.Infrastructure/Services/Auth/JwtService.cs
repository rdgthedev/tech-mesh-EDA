namespace TechMesh.Auth.Infrastructure.Services.Auth;

public class JwtService : IJwtService
{
    private readonly IOptions<Configuration.JwtOptions> _jwtOptions;

    public JwtService(IOptions<Configuration.JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

    public async Task<string> GenerateAccessToken(Guid userId, string roleName)
    {
        var credentials = new SigningCredentials(
            new RsaSecurityKey(
                JwtHelper.ExtractRsaKey(_jwtOptions.Value.PrivateKey)),
            SecurityAlgorithms.RsaSha256);

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtOptions.Value.Issuer,
            audience: _jwtOptions.Value.Audience,
            claims: new List<Claim>
            {
                new(JwtRegisteredClaimNames.NameId, userId.ToString()),
                new(JwtRegisteredClaimNames.Profile, roleName),
            },
            expires: DateTime.UtcNow.AddMinutes(15),
            signingCredentials: credentials);

        return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwtToken));
    }
}