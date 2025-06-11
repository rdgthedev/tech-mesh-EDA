namespace TechMesh.Application.Results;

public class Result<TData>
{
    public int? StatusCode { get; private set; }
    public string? Message { get; private set; }
    public bool IsSuccess { get; private set; }
    public TData? Data { get; private set; }
    public List<string> Errors { get; private set; } = [];


    private Result(TData data, string message)
    {
        StatusCode = Convert.ToInt32(HttpStatusCode.OK);
        IsSuccess = true;
        Data = data;
        Message = message;
    }

    private Result(HttpStatusCode statusCode, string? message = null)
    {
        StatusCode = Convert.ToInt32(statusCode);
        IsSuccess = true;
        Message = message;
    }

    private Result(int statusCode, TData data, string message)
    {
        StatusCode = statusCode;
        IsSuccess = true;
        Data = data;
        Message = message;
    }

    private Result(int? statusCode, TData? data = default, params string[] errors)
    {
        StatusCode = statusCode;
        Data = data;
        Errors.AddRange(errors);
    }

    private Result(bool isSuccess = true)
    {
        IsSuccess = isSuccess;
    }

    public static Result<TData> Failure(int? statusCode, params string[] errors) =>
        new(statusCode, default, errors);

    public static Result<TData> Success(int statusCode, TData data, string message = "")
        => new(statusCode, data, message);

    public static Result<TData> Success() => new();

    public static Result<TData> Success(TData data, string message = "") => new(data, message);
}