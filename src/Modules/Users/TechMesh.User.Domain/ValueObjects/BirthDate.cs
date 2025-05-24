namespace TechMesh.User.Domain.ValueObjects;

public class BirthDate
{
    public DateTime Value { get; private set; }

    public BirthDate(DateTime value)
    {
        DomainException.When(value > DateTime.UtcNow, "Birth date cannot be in the future");

        Value = value;
    }

    public static implicit operator BirthDate(DateTime value) => new(value);
    public static implicit operator DateTime(BirthDate birthDate) => birthDate.Value;

    public override string ToString() => Value.ToString("dd-MM-yyyy");

    public override bool Equals(object? obj)
    {
        return obj is BirthDate birthDate
               && birthDate.Value == Value;
    }

    public override int GetHashCode() => Value.GetHashCode();
}