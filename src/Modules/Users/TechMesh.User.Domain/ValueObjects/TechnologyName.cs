namespace TechMesh.User.Domain.ValueObjects;

public class TechnologyName
{
    public string Value { get; init; }

    public TechnologyName(string value)
    {
        DomainException.When(string.IsNullOrEmpty(value), "Value is required.");

        Value = value;
    }

    public static implicit operator string(TechnologyName technologyName) => technologyName.Value;
    public static implicit operator TechnologyName(string name) => new(name);

    public override bool Equals(object? obj) => obj is TechnologyName technologyName && technologyName.Value == Value;

    public override string ToString() => Value;

    public override int GetHashCode() => Value.GetHashCode();
}