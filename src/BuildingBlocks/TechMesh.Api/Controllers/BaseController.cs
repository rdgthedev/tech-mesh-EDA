namespace TechMesh.Api.Controllers;

public abstract class BaseController : ControllerBase
{
    protected readonly IMediator _mediator = null!;

    protected BaseController()
    {
    }

    protected BaseController(IMediator mediator)
        => _mediator = mediator;

    protected async Task<TResponse> DispatchAsync<TResponse>(
        IRequest<TResponse> request,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(request, cancellationToken);
    }

    protected ActionResult<Result<TData>> CustomResponse<TData>(Result<TData> result, string? actionName = null)
        => result.StatusCode switch
        {
            (int)HttpStatusCode.OK => Ok(result),
            (int)HttpStatusCode.Created => string.IsNullOrEmpty(actionName)
                ? CreatedAtAction(actionName, result)
                : Created(string.Empty, result),
            (int)HttpStatusCode.NoContent => NoContent(),
            (int)HttpStatusCode.BadRequest => BadRequest(result),
            (int)HttpStatusCode.Unauthorized => Unauthorized(),
            (int)HttpStatusCode.Forbidden => Forbid(),
            (int)HttpStatusCode.NotFound => NotFound(result),
            _ => StatusCode(result.StatusCode ?? 0, result)
        };
}