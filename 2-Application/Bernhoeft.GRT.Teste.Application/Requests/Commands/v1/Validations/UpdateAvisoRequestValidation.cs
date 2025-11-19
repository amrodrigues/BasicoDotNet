using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations
{
    public class UpdateAvisoRequestValidation : AbstractValidator<UpdateAvisoRequest>
    {
        public UpdateAvisoRequestValidation()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("O Id do aviso deve ser maior que zero.");

            RuleFor(x => x.Mensagem)
                .NotEmpty().WithMessage("A mensagem do aviso é obrigatória.");
        }
    }
}
