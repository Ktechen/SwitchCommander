using FluentValidation;
using SwitchCommander.Application.Features.UserFeatures.CreateUser.Records;

namespace SwitchCommander.Application.Features.UserFeatures.CreateUser;

public sealed class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(50)
            .EmailAddress()
            .WithMessage("Email is not valid");
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50)
            .WithMessage("Name is not valid");
    }
}