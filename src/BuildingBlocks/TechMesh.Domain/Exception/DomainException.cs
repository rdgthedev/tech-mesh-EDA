namespace TechMesh.Domain.Exception;

public class DomainException(string message) : System.Exception(message)
{
    public static void ThrowIfNull(object? value, string message)
    {
        if (value is null)
            throw new DomainException(message);
    }
    
    public static void When(bool condition, string message)
    {
        if (condition)
            throw new DomainException(message);
    }
}