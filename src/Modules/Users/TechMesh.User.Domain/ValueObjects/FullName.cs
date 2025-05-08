namespace TechMesh.User.Domain.ValueObjects;

public class FullName
{
    public string Value { get; private set; }

    public FullName(string value)
    {
        DomainException.When(string.IsNullOrEmpty(value), "Full name cannot be empty.");

        Value = value;
    }

    public static implicit operator FullName(string value) => new(value);
    public static implicit operator string(FullName fullName) => fullName.Value;

    public override bool Equals(object? obj)
    {
        return obj is FullName fullName
               && fullName.Value == Value;
    }

    public override string ToString() => Value;

    public override int GetHashCode() => Value.GetHashCode();
}