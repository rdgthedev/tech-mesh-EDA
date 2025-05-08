namespace TechMesh.Auth.Infrastructure.Services.Token;

public class TokenService : ITokenService
{
    private readonly ITokenRepository _tokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TokenService(
        ITokenRepository tokenRepository,
        IUnitOfWork unitOfWork)
    {
        _tokenRepository = tokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Domain.Entities.Token> CreateAsync(
        Domain.Entities.Token token,
        CancellationToken cancellationToken)
    {
        if (token.ExpirationTime < DateTime.Now)
            throw new Exception("The expiration time cannot less than that current time!");

        await _tokenRepository.CreateAsync(token, cancellationToken);

        return token;
    }

    public async Task<Results> DeleteAsync(string token, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(token))
            return Results.Failure(Convert.ToInt32(HttpStatusCode.BadRequest), "Token is empty!");

        var tokenResult = await _tokenRepository.GetByTokenAsync(token, cancellationToken);

        if (tokenResult is null)
            return Results.Failure(Convert.ToInt32(HttpStatusCode.NotFound), "Token not exist!");

        await _tokenRepository.DeleteAsync(tokenResult, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Results.Success(204);
    }
}