using System.Net;

namespace Locadora.Api.Presentation;

public class ResponseBodyFailure
{
    public HttpStatusCode Status { get; set; }
    public string Mensagem { get; }

    public ResponseBodyFailure(string mensagem, HttpStatusCode status)
    {
        Mensagem = mensagem;
        Status = status;
    }
}