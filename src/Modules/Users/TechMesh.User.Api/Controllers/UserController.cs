namespace TechMesh.User.Api.Controllers
{
    [ApiController]
    [Route("v1")]
    public class UserController : BaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("users")]
        public async Task<ActionResult<Result<string>>> CreateUser(
            [FromBody] CreateUserCommand command,
            CancellationToken cancellationToken)
        {
            var result = await DispatchAsync(command, cancellationToken);

            return CustomResponse(result);
        }
    }
}