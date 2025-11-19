using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1
{
    public class GetAvisoByIdHandler : IQueryHandler<GetAvisoByIdRequest, GetAvisoByIdResponse>
    {
        private readonly IAvisoRepository _avisoRepository;

        public GetAvisoByIdHandler(IAvisoRepository avisoRepository)
        {
            _avisoRepository = avisoRepository;
        }

        public async Task<GetAvisoByIdResponse> Handle(GetAvisoByIdRequest request, CancellationToken cancellationToken)
        {
            var aviso = await _avisoRepository.GetByIdAsync(request.Id, cancellationToken);

            if (aviso == null || !aviso.Ativo)
            {
                throw new NotFoundException($"Aviso com Id {request.Id} não encontrado ou inativo.");
            }

            return new GetAvisoByIdResponse
            {
                Id = aviso.Id,
                Titulo = aviso.Titulo,
                Mensagem = aviso.Mensagem,
                DataCriacao = aviso.DataCriacao,
                DataEdicao = aviso.DataEdicao
            };
        }
    }
}
