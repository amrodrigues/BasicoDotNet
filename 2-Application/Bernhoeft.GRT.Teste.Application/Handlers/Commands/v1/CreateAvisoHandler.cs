using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
 
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1.Interfaces;


namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1
{
    public class CreateAvisoHandler : ICommandHandler<CreateAvisoRequest, CreateAvisoResponse>
    {
        private readonly IAvisoRepository _avisoRepository;

        public CreateAvisoHandler(IAvisoRepository avisoRepository)
        {
            _avisoRepository = avisoRepository;
        }

        public async Task<CreateAvisoResponse> Handle(CreateAvisoRequest request, CancellationToken cancellationToken)
        {
            var aviso = new AvisoEntity
            {
                Titulo = request.Titulo,
                Mensagem = request.Mensagem
            };

            await _avisoRepository.AddAsync(aviso, cancellationToken);
          //  await _avisoRepository.UnitOfWork.CommitAsync(cancellationToken);

            return new CreateAvisoResponse { Id = aviso.Id };
        }
    }
}

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1.Interfaces
{
    public interface ICommandHandler<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
