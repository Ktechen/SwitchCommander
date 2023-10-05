using FluentValidation;

namespace SwitchCommander.Application.Features.SSH.UpdateSSHCommand;

public class UpdateSSHServerValidator : AbstractValidator<UpdateSSHServerRequest>
{
    public UpdateSSHServerValidator()
    {
        RuleFor(request => request.Hostname)
            .NotEmpty().WithMessage("Hostname is required.")
            .MaximumLength(255).WithMessage("Hostname cannot exceed 255 characters.");

        RuleFor(request => request.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(255).WithMessage("Username cannot exceed 255 characters.");
    }
}