namespace TechMesh.Application.Results;

public class Result<TData>
{
    public int? StatusCode { get; set; }
    public string? Message { get; set; }
    public bool IsSuccess { get; set; }
    public TData? Data { get; set; }
    public List<string> Errors { get; set; } = [];

    public Result()
    {
    }

    private Result(TData data, string message, int statusCode = (int)HttpStatusCode.OK)
    {
        IsSuccess = true;
        StatusCode = statusCode;
        Data = data;
        Message = message;
    }

    private Result(
        int? statusCode,
        TData? data = default,
        string? message = null,
        params string[] errors)
    {
        StatusCode = statusCode;
        Data = data;
        Message = message;
        Errors.AddRange(errors);
    }

    private Result(int statusCode, bool isSuccess = true)
    {
        StatusCode = statusCode;
        IsSuccess = isSuccess;
    }

    public static Result<TData> Failure(int? statusCode, params string[] errors) =>
        new(statusCode, default, null, errors);

    public static Result<TData> Failure(int? statusCode, string? message, params string[] errors) =>
        new(statusCode, default, message, errors);

    public static Result<TData> Success(int? statusCode, TData data, string message = "")
        => new(data, message, statusCode ?? (int)HttpStatusCode.OK);

    public static Result<TData> Success(int statusCode) => new(statusCode);

    public static Result<TData> Success(TData data, string message = "") => new(data, message);
}