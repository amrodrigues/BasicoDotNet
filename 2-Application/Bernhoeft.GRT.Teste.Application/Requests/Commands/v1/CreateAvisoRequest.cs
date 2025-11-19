using Bernhoeft.GRT.Core.Interfaces;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1
{
    public class CreateAvisoRequest : ICommand<CreateAvisoResponse>
    {
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
    }
}
