namespace TechMesh.Application.Results;

public class Result<TData>
{
    public int? StatusCode { get; private set; }
    public string? Message { get; private set; }
    public bool IsSuccess { get; private set; }
    public TData? Data { get; private set; }
    public List<string> Errors { get; private set; } = [];


    private Result(TData data, string message, int statusCode = (int)HttpStatusCode.OK)
    {
        IsSuccess = true;
        StatusCode = statusCode;
        Data = data;
        Message = message;
    }

    private Result(int? statusCode, TData? data = default, params string[] errors)
    {
        StatusCode = statusCode;
        Data = data;
        Errors.AddRange(errors);
    }

    private Result(int statusCode, bool isSuccess = true)
    {
        IsSuccess = isSuccess;
    }

    public static Result<TData> Failure(int? statusCode, params string[] errors) =>
        new(statusCode, default, errors);

    public static Result<TData> Success(int? statusCode, TData data, string message = "")
        => new(data, message, statusCode ?? (int)HttpStatusCode.OK);

    public static Result<TData> Success(int statusCode) => new(statusCode);

    public static Result<TData> Success(TData data, string message = "") => new(data, message);
}