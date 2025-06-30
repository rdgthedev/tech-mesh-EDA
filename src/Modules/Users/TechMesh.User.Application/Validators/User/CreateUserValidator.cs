namespace TechMesh.User.Application.Validators.User;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required.")
            .Length(3, 128)
            .WithMessage("First name must be between 2 and 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.BirthDate)
            .Must(date => date.IsBeforeToday())
            .WithMessage("Birth date must be before today.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.");

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

        RuleFor(x => x.State)
            .NotEmpty()
            .WithMessage("State is required.");

        RuleFor(x => x.ZipCode)
            .NotEmpty()
            .WithMessage("ZipCode is required.");

        RuleFor(x => x.Technologies)
            .NotEmpty()
            .WithMessage("At least one technology is required.");
    }
}