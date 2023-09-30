using FluentValidation;
using SwitchCommander.Application.Repositories.Features;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.SSH.CreateSSHCommand;

public class CreateSSHCommandValidator : AbstractValidator<CreateSSHCommandRequest>
{
    private readonly ISSHCommandConfigurationRepository _repository;
    
    public CreateSSHCommandValidator(ISSHCommandConfigurationRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is not valid");

        RuleFor(x => x.Command)
            .NotEmpty()
            .MinimumLength(GetDefaultConfig().CommandMinimumLength)
            .MaximumLength(GetDefaultConfig().CommandMaximumLength)
            .WithMessage("Command is not valid");

        RuleFor(x => x.Description)
            .NotEmpty()
            .MinimumLength(GetDefaultConfig().DescriptionMinimumLength)
            .MaximumLength(GetDefaultConfig().DescriptionMaximumLength)
            .WithMessage("Description is not valid");
    }

    private SShCommandConfiguration GetDefaultConfig()
    {
        var result = _repository.GetDefaultConfig(default).Result;
        return result ?? new SShCommandConfiguration();
    }
    
}