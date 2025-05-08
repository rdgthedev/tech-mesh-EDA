namespace TechMesh.Api.Controllers;

public abstract class BaseController : ControllerBase
{
    private readonly IMediator _mediator;

    protected BaseController(IMediator mediator)
        => _mediator = mediator;

    protected async Task<TResponse> DispatchAsync<TRequest, TResponse>(
        TRequest request,
        CancellationToken cancellationToken)
        where TRequest : IRequest<TResponse>
    {
        return await _mediator.Send(request, cancellationToken);
    }

    protected async Task DispatchAsync<TRequest>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest
    {
        await _mediator.Send(request, cancellationToken);
    }

    protected ActionResult<Results> CustomResponse(Results results, string? actionName = null)
        => results.StatusCode switch
        {
            (int)HttpStatusCode.OK => Ok(results),
            (int)HttpStatusCode.Created when actionName is not null => CreatedAtAction(actionName, results),
            (int)HttpStatusCode.NoContent => NoContent(),
            (int)HttpStatusCode.BadRequest => BadRequest(results),
            (int)HttpStatusCode.Unauthorized => Unauthorized(),
            (int)HttpStatusCode.Forbidden => Forbid(),
            (int)HttpStatusCode.NotFound => NotFound(results),
            _ => StatusCode(results.StatusCode, results)
        };
}