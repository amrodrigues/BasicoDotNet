using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;

namespace Bernhoeft.GRT.Teste.Api.Controllers.v1
{
    /// <response code="401">Não Autenticado.</response>
    /// <response code="403">Não Autorizado.</response>
    /// <response code="500">Erro Interno no Servidor.</response>
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
    public class AvisosController : RestApiController
    {
        ///// <summary>
        ///// Retorna um Aviso por ID.
        ///// </summary>
        ///// <param name="request"></param>
        ///// <param name="cancellationToken"></param>
        ///// <returns>Aviso.</returns>
        ///// <response code="200">Sucesso.</response>
        ///// <response code="400">Dados Inválidos.</response>
        ///// <response code="404">Aviso Não Encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAvisosResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[JwtAuthorize(Roles = AuthorizationRoles.CONTRACTLAYOUT_SISTEMA_AVISO_PESQUISAR)]
        public async Task<object> GetAviso([FromModel] GetAvisosRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(request, cancellationToken);

        /// <summary>
        /// Retorna Todos os Avisos Cadastrados para Tela de Edição.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Lista com Todos os Avisos.</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="204">Sem Avisos.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDocumentationRestResult<IEnumerable<GetAvisosResponse>>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<object> GetAvisos(CancellationToken cancellationToken)
            => await Mediator.Send(new GetAvisosRequest(), cancellationToken);
        /// <summary>
        /// Cria um novo Aviso.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>ID do Aviso criado.</returns>
        /// <response code="201">Aviso criado com sucesso.</response>
        /// <response code="400">Dados Inválidos.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IDocumentationRestResult<CreateAvisoResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[JwtAuthorize(Roles = AuthorizationRoles.CONTRACTLAYOUT_SISTEMA_AVISO_CRIAR)]
        public async Task<object> PostAviso([FromBody] CreateAvisoRequest request, CancellationToken cancellationToken)
            => await Mediator.Send(request, cancellationToken);

        /// <summary>
        /// Edita a mensagem de um Aviso existente.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>ID do Aviso editado.</returns>
        /// <response code="200">Aviso editado com sucesso.</response>
        /// <response code="400">Dados Inválidos.</response>
        /// <response code="404">Aviso Não Encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDocumentationRestResult<UpdateAvisoResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[JwtAuthorize(Roles = AuthorizationRoles.CONTRACTLAYOUT_SISTEMA_AVISO_EDITAR)]
        public async Task<object> PutAviso([FromRoute] int id, [FromBody] UpdateAvisoRequest request, CancellationToken cancellationToken)
        {
            request.Id = id;
            return await Mediator.Send(request, cancellationToken);
        }

        /// <summary>
        /// Remove (Soft Delete) um Aviso.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Status de sucesso da remoção.</returns>
        /// <response code="200">Aviso removido com sucesso.</response>
        /// <response code="400">Dados Inválidos.</response>
        /// <response code="404">Aviso Não Encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDocumentationRestResult<DeleteAvisoResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[JwtAuthorize(Roles = AuthorizationRoles.CONTRACTLAYOUT_SISTEMA_AVISO_REMOVER)]
        public async Task<object> DeleteAviso([FromRoute] int id, CancellationToken cancellationToken)
            => await Mediator.Send(new DeleteAvisoRequest { Id = id }, cancellationToken);

        
    }
}