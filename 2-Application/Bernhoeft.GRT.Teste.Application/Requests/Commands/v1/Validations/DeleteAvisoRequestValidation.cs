using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations
{
    public class DeleteAvisoRequestValidation : AbstractValidator<DeleteAvisoRequest>
    {
        public DeleteAvisoRequestValidation()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("O Id do aviso deve ser maior que zero.");
        }
    }
}
