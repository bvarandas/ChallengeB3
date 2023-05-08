using ChallengeB3.Domain.Commands;
using FluentValidation;

namespace ChallengeB3.Domain.Validations;

public abstract class RegisterValidation<T> : AbstractValidator<T> where T : RegisterCommand
{
    protected void ValidateRegisterId()
    {
        RuleFor(c => c.RegisterId)
            .NotEqual(0);
    }

    protected void ValidateDescription()
    {
        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("É necessário inserir a Descrição!")
            .Length(2, 150).WithMessage("É necessário inserir ao menos 2 caracteres na Descrição!");
    }

    protected void ValidateStatus()
    {
        RuleFor(c => c.Status)
            .NotEmpty()
            .WithMessage("É necessário inserir o Status!")
            .Length(1, 150).WithMessage("É necessário inserir ao menos 1 caracter no Status!");
    }

    protected void ValidateDate()
    {
        RuleFor(c => c.Date)
            .NotEmpty()
            .WithMessage("É necessário inserir a Data !")
            //.Length(1, 150)
            .WithMessage("É necessário inserir ao menos 1 caracter no Status!");
    }
}
