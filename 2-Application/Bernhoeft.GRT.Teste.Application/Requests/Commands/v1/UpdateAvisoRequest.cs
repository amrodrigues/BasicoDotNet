using Bernhoeft.GRT.Core.Interfaces;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using MediatR;
namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1
{
    public class UpdateAvisoRequest : ICommand<UpdateAvisoResponse>
    {
        public int Id { get; set; }
        public string Mensagem { get; set; }
    }
}
