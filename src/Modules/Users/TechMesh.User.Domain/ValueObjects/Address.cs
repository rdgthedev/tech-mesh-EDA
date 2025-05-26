namespace TechMesh.User.Domain.ValueObjects;

public class Address
{
    public string Street { get; private set; }
    public string Neighborhood  { get; private set; }
    public string City { get; private set; }
    public string Country { get; private set; }
    public int Number { get; private set; }
    public string ZipCode { get; private set; }
    public string Complement { get; private set; }

    public Address(
        string street,
        string neighborhood,
        string city,
        string country,
        int number,
        string zipCode,
        string complement)
    {
        DomainException.When(string.IsNullOrEmpty(street.Trim()), "Street cannot be empty.");
        DomainException.When(string.IsNullOrEmpty(neighborhood.Trim()), "Neighborhood cannot be empty.");
        DomainException.When(string.IsNullOrEmpty(city), "City cannot be empty.");
        DomainException.When(string.IsNullOrEmpty(country), "Country cannot be empty.");
        DomainException.When(number <= 0, "Number must be greater than zero.");
        DomainException.When(string.IsNullOrEmpty(zipCode), "Zip code cannot be empty.");
        DomainException.When(string.IsNullOrEmpty(complement), "Complement cannot be empty.");

        Street = street;
        Neighborhood  = neighborhood;
        City = city;
        Country = country;
        Number = number;
        ZipCode = zipCode;
        Complement = complement;
    }

    public static implicit operator string(Address address) => address.ToString();

    public override bool Equals(object? obj)
    {
        return obj is Address address
               && address.Street == Street
               && address.Neighborhood  == Neighborhood 
               && address.City == City
               && address.Country == Country
               && address.Number == Number;
    }

    public override string ToString() => $"{Street}," +
                                         $" {Number}" +
                                         $" - {Neighborhood }," +
                                         $" {City}," +
                                         $" {Country} " +
                                         $"- {ZipCode} " +
                                         $"- {Complement}";

    public override int GetHashCode() => HashCode.Combine(Street, Neighborhood , City, Country, Number);
}