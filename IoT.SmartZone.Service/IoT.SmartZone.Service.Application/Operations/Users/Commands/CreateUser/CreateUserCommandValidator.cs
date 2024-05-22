using FluentValidation;
using IoT.SmartZone.Service.Application.Common.Validation;
using IoT.SmartZone.Service.Infrastructure.FluentApi;

namespace IoT.SmartZone.Service.Application.Operations.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(x=>ValidationMessages.EmptyValueNotAllowed(nameof(x.Name)))
            .MaximumLength(UserConstraints.NameLength)
            .WithMessage(x=>ValidationMessages.MaximumLengthExceeded(nameof(x.Name),UserConstraints.NameLength));
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.EmptyValueNotAllowed(nameof(x.LastName)))
            .MaximumLength(UserConstraints.LastNameLength)
            .WithMessage(x => ValidationMessages.MaximumLengthExceeded(nameof(x.LastName), UserConstraints.LastNameLength));
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(x => ValidationMessages.EmptyValueNotAllowed(nameof(x.Email)))
            .MaximumLength(UserConstraints.EmailLength)
            .WithMessage(x => ValidationMessages.MaximumLengthExceeded(nameof(x.Email), UserConstraints.EmailLength));
    }
}
