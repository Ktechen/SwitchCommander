using FluentValidation;
using SwitchCommander.Application.Repositories.Features.SSH;
using SwitchCommander.Domain.Dtos;

namespace SwitchCommander.Application.Features.SSH.Create.Command;

public class CreateSSHCommandValidator : AbstractValidator<CreateSSHCommandRequest>
{
    private readonly ISshCommandConfigurationMongoRepository _mongoRepository;

    public CreateSSHCommandValidator(ISshCommandConfigurationMongoRepository mongoRepository)
    {
        _mongoRepository = mongoRepository;

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
        var result = _mongoRepository.GetDefaultConfig(default).Result;
        return result ?? new SShCommandConfiguration();
    }
}