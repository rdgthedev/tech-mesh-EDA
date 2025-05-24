namespace TechMesh.User.Application.Validators.User;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    private const string PhoneNumberPattern = @"^\+?[1-9]\d{1,14}$";
    private const string ZipCodePattern = "^[0-9]{5}(?:-[0-9]{4})?$";

    public CreateUserValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .Length(3, 128)
            .WithMessage("First name must be between 2 and 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.BirthDate)
            .Must(date => date.IsAfterToday())
            .WithMessage("Birth date must be before today.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .Matches(PhoneNumberPattern)
            .WithMessage("Invalid phone number format.");

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid status.");

        RuleFor(x => x.Level)
            .IsInEnum()
            .WithMessage("Invalid level.");

        RuleFor(x => x.Street)
            .NotEmpty()
            .WithMessage("Street is required.");

        RuleFor(x => x.Neighborhood)
            .NotEmpty()
            .WithMessage("Neighborhood is required.");

        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City is required.");

        RuleFor(x => x.Country)
            .NotEmpty()
            .WithMessage("Country is required.");

        RuleFor(x => x.Number)
            .GreaterThan(0)
            .WithMessage("Number must be greater than 0.");

        RuleFor(x => x.ZipCode)
            .NotEmpty()
            .WithMessage("ZipCode is required.")
            .Matches(ZipCodePattern)
            .WithMessage("Invalid zip code format.");

        RuleFor(x => x.Technologies)
            .Must(x => x.Count > 0)
            .WithMessage("At least one technology is required.");
    }
}