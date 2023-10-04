using FluentValidation;

namespace SwitchCommander.Application.Features.SSH.CreateSSHCommand;

public class CreateSSHServerValidator : AbstractValidator<CreateSSHServerRequest>
{
    public CreateSSHServerValidator()
    {
        RuleFor(request => request.Hostname)
            .NotEmpty().WithMessage("Hostname is required.")
            .MaximumLength(255).WithMessage("Hostname cannot exceed 255 characters.");

        RuleFor(request => request.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(255).WithMessage("Username cannot exceed 255 characters.");

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}