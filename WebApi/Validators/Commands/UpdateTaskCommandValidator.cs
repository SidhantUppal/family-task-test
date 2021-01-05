using Domain.Commands;
using FluentValidation;

namespace WebApi.Validators.Commands
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.Subject).NotNull().NotEmpty();
        }
    }
}