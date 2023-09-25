using FluentValidation;

namespace SwitchCommander.Application.Features.SSHManagementFeatures.CreateSSHCommand;

public class CreateSSHCommandValidatior : AbstractValidator<CreateSSHCommandRequest>
{
    public CreateSSHCommandValidatior()
    {
        RuleFor(x => x.Hash)
            .NotEmpty()
            .WithMessage("Hash is not valid");

        RuleFor(x => x.command)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(5000)
            .WithMessage("Command is not valid");
    }
}