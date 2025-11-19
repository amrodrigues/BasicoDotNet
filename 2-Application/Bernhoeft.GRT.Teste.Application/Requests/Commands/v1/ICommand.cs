using MediatR;
namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
        // Esta interface continua vazia, servindo como seu marcador de arquitetura.
    }
}