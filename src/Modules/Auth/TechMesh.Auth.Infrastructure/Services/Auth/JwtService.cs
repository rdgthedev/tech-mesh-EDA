namespace TechMesh.Auth.Infrastructure.Services.Auth;

public class JwtService : IJwtService
{
    private readonly IOptions<Configuration.JwtOptions> _jwtOptions;
    private readonly ITokenService _tokenService;
    private readonly DateTime _expirationTimeRefreshToken = DateTime.UtcNow.AddDays(7);

    public JwtService(
        IOptions<Configuration.JwtOptions> jwtOptions,
        ITokenService tokenService)
    {
        _jwtOptions = jwtOptions;
        _tokenService = tokenService;
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

    public async Task<Result<TokenResponse>> GenerateRefreshToken(Guid userId, CancellationToken cancellationToken)
    {
        var token = new Domain.Entities.Token(userId, _expirationTimeRefreshToken, ETokenType.RefreshToken);

        return await _tokenService.CreateAsync(token, cancellationToken);
    }
}