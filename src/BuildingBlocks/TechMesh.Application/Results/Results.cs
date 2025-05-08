namespace TechMesh.Application.Results;

public class Results
{
    public int StatusCode { get; private set; }
    public string? Message { get; private set; }
    public object? Data { get; private set; }
    public List<string> Errors { get; set; } = new();

    private Results(object data, string message)
    {
        StatusCode = Convert.ToInt32(HttpStatusCode.OK);
        Data = data;
        Message = message;
    }

    private Results(int statusCode, object data, string message)
    {
        StatusCode = statusCode;
        Data = data;
        Message = message;
    }

    private Results(int statusCode, string error)
    {
        StatusCode = statusCode;
        Errors.Add(error);
    }

    private Results(string error)
    {
        Errors.Add(error);
    }

    public static Results Failure(int statusCode, string error) => new(statusCode, error);
    public static Results Failure(string error) => new(error);
    public static Results Success(int statusCode, object data, string message = "") => new(statusCode, data, message);
    public static Results Success(object data, string message = "") => new(data, message);
}