using FluentValidation;

namespace IoT.SmartZone.Service.Modules.Users.Core.Commands.SignUp;

internal sealed class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email can not be empty.")
            .EmailAddress().WithMessage("Email is invalid.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password can not be empty.")
            .MinimumLength(5).WithMessage("Password too short. Minimum required length: 5")
            .MaximumLength(32).WithMessage("Password too long. Maximum required length: 32");

        RuleFor(x => x.Role)
            .NotEmpty()
            .WithMessage("Role can not be empty");
    }
}
