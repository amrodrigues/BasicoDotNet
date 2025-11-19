using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations
{
    public class CreateAvisoRequestValidation : AbstractValidator<CreateAvisoRequest>
    {
        public CreateAvisoRequestValidation()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("O título do aviso é obrigatório.")
                .MaximumLength(50).WithMessage("O título deve ter no máximo 50 caracteres.");

            RuleFor(x => x.Mensagem)
                .NotEmpty().WithMessage("A mensagem do aviso é obrigatória.");
        }
    }
}
