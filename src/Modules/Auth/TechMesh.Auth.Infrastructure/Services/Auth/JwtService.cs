

namespace TechMesh.Auth.Infrastructure.Services.Auth;

public class JwtService : IJwtService
{
    private readonly IOptions<Configuration.JwtOptions> _jwtOptions;
    private readonly ITokenRepository _tokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public JwtService(
        IOptions<Configuration.JwtOptions> jwtOptions,
        ITokenRepository tokenRepository,
        IUnitOfWork unitOfWork)
    {
        _jwtOptions = jwtOptions;
        _tokenRepository = tokenRepository;
        _unitOfWork = unitOfWork;
    }

    public string GenerateAccessToken(Guid userId, string roleName)
    {
        var credentials = new SigningCredentials(
            new RsaSecurityKey(
                JwtHelper.ExtractRsaPrivateKey(_jwtOptions.Value.PrivateKey)),
            SecurityAlgorithms.RsaSha256);

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtOptions.Value.Issuer,
            audience: _jwtOptions.Value.Audience,
            claims: new List<Claim>
            {
                new(JwtRegisteredClaimNames.NameId, userId.ToString()),
                new(JwtRegisteredClaimNames.Profile, roleName),
            },
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    public Domain.Entities.Token GenerateRefreshToken(
        Guid userId,
        DateTime expirationTime)
    {
        var token = new Domain.Entities.Token(userId, expirationTime, ETokenType.RefreshToken);

        return token;
    }

    public Results ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token))
            return Results.Failure(Convert.ToInt32(HttpStatusCode.Unauthorized), "Token is empty!");

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new RsaSecurityKey(JwtHelper.ExtractRsaPublicKey(_jwtOptions.Value.PrivateKey)),
            ValidateIssuer = true,
            ValidIssuer = _jwtOptions.Value.Issuer,
            ValidateAudience = true,
            ValidAudience = _jwtOptions.Value.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(5)
        };

        new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out _);

        return Results.Success(Convert.ToInt32(HttpStatusCode.OK), true, "Token is valid!");
    }
}