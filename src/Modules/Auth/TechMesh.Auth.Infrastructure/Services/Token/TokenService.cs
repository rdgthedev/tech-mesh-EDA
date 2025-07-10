using System.Net;
using TechMesh.Auth.Application.Abstracts.Services.Infra;

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

    public async Task<Result<TokenResponse>> CreateAsync(
        Domain.Entities.Token token,
        CancellationToken cancellationToken)
    {
        await _tokenRepository.CreateAsync(token, cancellationToken);

        var tokenResponse = new TokenResponse(token.Value.ToString(), token.ExpirationTime, token.Type);

        return Result<TokenResponse>.Success(tokenResponse);
    }

    public async Task<Result<bool>> DeleteAsync(string token, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(token))
            return Result<bool>.Failure(400, "Token is empty!");

        var tokenResult = await _tokenRepository.GetByTokenAsync(token, cancellationToken);

        if (tokenResult is null)
            return Result<bool>.Failure(404, "Token not exist!");

        await _tokenRepository.DeleteAsync(tokenResult, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<bool>.Success(Convert.ToInt32(HttpStatusCode.NoContent));
    }
}