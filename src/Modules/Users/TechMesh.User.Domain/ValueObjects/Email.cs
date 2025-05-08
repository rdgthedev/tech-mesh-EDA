using TechMesh.Domain.Exception;

namespace TechMesh.User.Domain.ValueObjects;

public class Email
{
    public string Address { get; private set; }

    public Email(string address)
    {
        DomainException.When(string.IsNullOrEmpty(address), "Birth date cannot be in the future");

        Address = address;
    }

    public static implicit operator string(Email email) => email.Address;
    public static implicit operator Email(string address) => new(address);

    public override bool Equals(object? obj)
    {
        return obj is Email email
               && email.Address == Address;
    }

    public override string ToString() => Address;

    public override int GetHashCode() => Address.GetHashCode();
}