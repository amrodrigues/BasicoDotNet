using Bernhoeft.GRT.Core.Interfaces;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1
{
    public class GetAvisoByIdRequest : IQuery<GetAvisoByIdResponse>
    {
        public int Id { get; set; }
    }
}
