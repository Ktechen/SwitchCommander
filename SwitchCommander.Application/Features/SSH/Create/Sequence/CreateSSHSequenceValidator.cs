using FluentValidation;

namespace SwitchCommander.Application.Features.SSH.Create.Sequence;

public class CreateSSHSequenceValidator : AbstractValidator<CreateSSHSequenceRequest>
{
    public CreateSSHSequenceValidator()
    {
        RuleFor(x => x.SequenceName)
            .NotNull()
            .NotEmpty()
            .WithMessage("SequenceName is null or empty");
    }
    
}

public class CreateSSHCommandSequenceValidator : AbstractValidator<CreateSSHCommandSequenceRequest>
{
    public CreateSSHCommandSequenceValidator()
    {
        RuleFor(x => x.SequenceName)
            .NotNull()
            .NotEmpty()
            .WithMessage("SequenceName is null or empty");
    }
    
}