using FluentValidation;
using SwitchCommander.Application.Repositories.Features;

namespace SwitchCommander.Application.Features.User.CreateUser;

public sealed class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    private readonly IUserMongoRepository _userMongoRepository;

    public CreateUserValidator(IUserMongoRepository userMongoRepository)
    {
        _userMongoRepository = userMongoRepository;

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is not valid")
            .Must(IsUniqueEmail)
            .WithMessage("Email already exists")
            .MaximumLength(50);

        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(50)
            .WithMessage("Name is not valid");
    }

    private bool IsUniqueEmail(string email)
    {
        var result = _userMongoRepository.FindAsync(x => x.Email == email).Result;
        return !result.Any();
    }
}