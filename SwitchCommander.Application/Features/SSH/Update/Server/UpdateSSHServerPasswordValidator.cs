using FluentValidation;

namespace SwitchCommander.Application.Features.SSH.Update.Server;

public class UpdateSSHServerPasswordValidator : AbstractValidator<UpdateSSHServerPasswordRequest>
{
    public UpdateSSHServerPasswordValidator()
    {
        RuleFor(request => request.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}