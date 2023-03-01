using FluentValidation;
using Prestadito.Setting.Application.Dto.Parameter;

namespace Prestadito.Setting.Application.Manager.Validators
{
    public class CreateParameterValidator : AbstractValidator<CreateParameterDTO>
    {
        public CreateParameterValidator()
        {
            RuleFor(x => x.StrCode)
                .NotEmpty().WithMessage("{PropertyName} is empty");

            RuleFor(x => x.StrName)
                .NotEmpty().WithMessage("{PropertyName} is empty");

            RuleFor(x => x.StrDescription)
                .NotEmpty().WithMessage("{PropertyName} is empty");
        }
    }

    public class UpdateParameterValidator : AbstractValidator<UpdateParameterDTO>
    {
        public UpdateParameterValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("{PropertyName} is empty");

            RuleFor(x => x.StrCode)
                .NotEmpty().WithMessage("{PropertyName} is empty");

            RuleFor(x => x.StrName)
                .NotEmpty().WithMessage("{PropertyName} is empty");

            RuleFor(x => x.StrDescription)
                .NotEmpty().WithMessage("{PropertyName} is empty");
        }
    }
}