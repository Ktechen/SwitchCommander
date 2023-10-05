using FluentValidation;

namespace SwitchCommander.Application.Features.SSH.Update.Config;

public class UpdateSSHCommandConfigurationValidator : AbstractValidator<UpdateSSHCommandConfigurationRequest>
{
    public UpdateSSHCommandConfigurationValidator()
    {
        RuleFor(x => x.CommandMinimumLength)
            .GreaterThanOrEqualTo(0)
            .WithMessage("CommandMinimumLength must be greater than or equal to 0")
            .LessThanOrEqualTo(int.MaxValue)
            .WithMessage($"CommandMinimumLength must be less than or equal to {int.MaxValue}");

        RuleFor(x => x.CommandMaximumLength)
            .GreaterThan(x => x.CommandMinimumLength)
            .WithMessage("CommandMaximumLength must be greater than CommandMinimumLength");

        RuleFor(x => x.DescriptionMinimumLength)
            .GreaterThanOrEqualTo(0)
            .WithMessage("DescriptionMinimumLength must be greater than or equal to 0")
            .LessThanOrEqualTo(int.MaxValue)
            .WithMessage($"DescriptionMinimumLength must be less than or equal to {int.MaxValue}");

        RuleFor(x => x.DescriptionMaximumLength)
            .GreaterThan(x => x.DescriptionMinimumLength)
            .WithMessage("DescriptionMaximumLength must be greater than DescriptionMinimumLength");
    }
}