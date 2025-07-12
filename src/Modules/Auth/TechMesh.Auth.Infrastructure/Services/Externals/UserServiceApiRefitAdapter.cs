namespace TechMesh.Auth.Infrastructure.Services.Externals;

public class UserServiceApiRefitAdapter : IUserServiceApiRefitAdapter
{
    private readonly IUserServiceApi _userServiceApi;

    public UserServiceApiRefitAdapter(IUserServiceApi userServiceApi)
        => _userServiceApi = userServiceApi;

    public async Task<Result<bool>> CreateUserAsync(CreateUserRequest request)
    {
        var rawResponse = await _userServiceApi.CreateUserAsync(request);

        if (rawResponse.IsSuccessStatusCode)
            return Result<bool>.Success(true);

        var responseResult = await rawResponse.Content.ReadFromJsonAsync<Result<string>>();

        return Result<bool>.Failure(
            Convert.ToInt32(responseResult?.StatusCode ?? (int?)rawResponse.StatusCode),
            "Failed to create user in external user service!",
            responseResult?.Errors.ToArray() ?? []);
    }
}