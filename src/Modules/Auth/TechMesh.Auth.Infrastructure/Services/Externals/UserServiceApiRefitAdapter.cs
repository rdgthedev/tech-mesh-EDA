namespace TechMesh.Auth.Infrastructure.Services.Externals;

public class UserServiceApiRefitAdapter : IUserServiceApiRefitAdapter
{
    private readonly IUserServiceApi _userServiceApi;

    public UserServiceApiRefitAdapter(IUserServiceApi userServiceApi)
        => _userServiceApi = userServiceApi;

    public async Task<Result<bool>> CreateUserAsync(CreateUserRequest request)
    {
        var response = await _userServiceApi.CreateUserAsync(request);

        return !response.IsSuccessStatusCode
            ? Result<bool>.Failure(Convert.ToInt32(response.StatusCode), "Failed to create user in external service")
            : Result<bool>.Success( true);
    }
}