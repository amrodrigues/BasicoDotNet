using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1.Interfaces;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;


namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1
{
    public class DeleteAvisoHandler : ICommandHandler<DeleteAvisoRequest, DeleteAvisoResponse>
    {
        private readonly IAvisoRepository _avisoRepository;

        public DeleteAvisoHandler(IAvisoRepository avisoRepository)
        {
            _avisoRepository = avisoRepository;
        }

        public async Task<DeleteAvisoResponse> Handle(DeleteAvisoRequest request, CancellationToken cancellationToken)
        {
            var aviso = await _avisoRepository.GetByIdAsync(request.Id, cancellationToken);

            if (aviso == null || !aviso.Ativo)
            {
                throw new NotFoundException($"Aviso com Id {request.Id} não encontrado ou já inativo.");
            }

            await _avisoRepository.DeleteAsync(aviso, cancellationToken);
            //await _avisoRepository.UnitOfWork.CommitAsync(cancellationToken);

            return new DeleteAvisoResponse { Sucesso = true };
        }
    }
}
