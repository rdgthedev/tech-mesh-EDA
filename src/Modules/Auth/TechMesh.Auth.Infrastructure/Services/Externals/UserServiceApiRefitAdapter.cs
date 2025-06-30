namespace TechMesh.Auth.Infrastructure.Services.Externals;

public class UserServiceApiRefitAdapter : IUserServiceApiRefitAdapter
{
    private readonly IUserServiceApi _userServiceApi;

    public UserServiceApiRefitAdapter(IUserServiceApi userServiceApi)
        => _userServiceApi = userServiceApi;

    public async Task<Result<bool>> CreateUserAsync(CreateUserRequest request)
    {
        var rawResponse = await _userServiceApi.CreateUserAsync(request);

        return await GetResponseFormatted(rawResponse);
    }

    private static async Task<Result<bool>> GetResponseFormatted(HttpResponseMessage rawResponse)
    {
        if (rawResponse.IsSuccessStatusCode)
            return Result<bool>.Success(true);

        var responseConverted = await rawResponse.Content.ReadFromJsonAsync<Result<string>>();

        return Result<bool>.Failure(
            Convert.ToInt32(responseConverted?.StatusCode ?? (int?)rawResponse.StatusCode),
            "Failed to create user in external user service!",
            responseConverted?.Errors.ToArray() ?? []);
    }
}