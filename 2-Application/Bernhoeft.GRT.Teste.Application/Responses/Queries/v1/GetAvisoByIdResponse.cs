namespace Bernhoeft.GRT.Teste.Application.Responses.Queries.v1
{
    public class GetAvisoByIdResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataEdicao { get; set; }
    }
}
