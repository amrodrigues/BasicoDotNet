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
    public class UpdateAvisoHandler : ICommandHandler<UpdateAvisoRequest, UpdateAvisoResponse>
    {
        private readonly IAvisoRepository _avisoRepository;

        public UpdateAvisoHandler(IAvisoRepository avisoRepository)
        {
            _avisoRepository = avisoRepository;
        }

        public async Task<UpdateAvisoResponse> Handle(UpdateAvisoRequest request, CancellationToken cancellationToken)
        {
            var aviso = await _avisoRepository.GetByIdAsync(request.Id, cancellationToken);

            if (aviso == null || !aviso.Ativo)
            {
                throw new NotFoundException($"Aviso com Id {request.Id} não encontrado ou inativo.");
            }

            aviso.AtualizarMensagem(request.Mensagem);

            await _avisoRepository.UpdateAsync(aviso, cancellationToken);
          //  await _avisoRepository.UnitOfWork.CommitAsync(cancellationToken);

            return new UpdateAvisoResponse { Id = aviso.Id };
        }
    }
}
