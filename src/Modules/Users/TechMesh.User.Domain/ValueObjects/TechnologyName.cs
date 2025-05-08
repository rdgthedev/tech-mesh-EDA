namespace TechMesh.User.Domain.ValueObjects;

public class TechnologyName
{
    public string Value { get; private set; }

    public TechnologyName(string value)
    {
        DomainException.When(!string.IsNullOrEmpty(value), "Value is required.");

        Value = value;
    }

    public static implicit operator string(TechnologyName technologyName) => technologyName.Value;
    public static implicit operator TechnologyName(string name) => new(name);

    protected bool Equals(TechnologyName technology) => Value == technology.Value;

    public override string ToString() => Value;

    public override int GetHashCode() => Value.GetHashCode();
}