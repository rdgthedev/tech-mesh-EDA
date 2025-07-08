namespace TechMesh.User.Domain.ValueObjects;

public class Address
{
    public string Street { get; init; }
    public string Neighborhood { get; init; }
    public string City { get; init; }
    public string Country { get; init; }
    public int Number { get; init; }
    public string State { get; init; }
    public string ZipCode { get; init; }
    public string Complement { get; init; }

    public Address(
        string street,
        string neighborhood,
        string city,
        string country,
        int number,
        string state,
        string zipCode,
        string complement)
    {
        DomainException.When(string.IsNullOrEmpty(street.Trim()), "Street cannot be empty.");
        DomainException.When(string.IsNullOrEmpty(neighborhood.Trim()), "Neighborhood cannot be empty.");
        DomainException.When(string.IsNullOrEmpty(city), "City cannot be empty.");
        DomainException.When(string.IsNullOrEmpty(country), "Country cannot be empty.");
        DomainException.When(number <= 0, "Number must be greater than zero.");
        DomainException.When(string.IsNullOrEmpty(state), "State is required.");
        DomainException.When(string.IsNullOrEmpty(zipCode), "Zip code cannot be empty.");
        DomainException.When(string.IsNullOrEmpty(complement), "Complement cannot be empty.");

        Street = street;
        Neighborhood = neighborhood;
        City = city;
        Country = country;
        Number = number;
        State = state;
        ZipCode = zipCode;
        Complement = complement;
    }

    public static implicit operator string(Address address) => address.ToString();

    public override bool Equals(object? obj)
    {
        return obj is Address address
               && address.Street == Street
               && address.Neighborhood == Neighborhood
               && address.City == City
               && address.Country == Country
               && address.Number == Number
               && address.State == State
               && address.ZipCode == ZipCode
               && address.Complement == Complement;
    }

    public override string ToString() => $"{Street}," +
                                         $" {Number}" +
                                         $" - {Neighborhood}," +
                                         $" {City}," +
                                         $" {State}" +
                                         $"- {Country} " +
                                         $"- {ZipCode} " +
                                         $"- {Complement}";

    public override int GetHashCode()
        => HashCode.Combine(Street, Neighborhood, City, Country, Number, State, ZipCode, Complement);
}