using FluentValidation;
using SwitchCommander.Application.Repositories.Features;

namespace SwitchCommander.Application.Features.UserManagement.CreateUser;

public sealed class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    private readonly IUserRepository _userRepository;

    public CreateUserValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is not valid")
            .Must(IsUniqueEmail)
            .WithMessage("Email already exists")
            .MaximumLength(50);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50)
            .WithMessage("Name is not valid");
    }

    private bool IsUniqueEmail(string email)
    {
        var result = _userRepository.FindAsync(x => x.Email == email).Result;
        return !result.Any();
    }
}