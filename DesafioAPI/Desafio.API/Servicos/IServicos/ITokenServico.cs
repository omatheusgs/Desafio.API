using System.Net;

namespace Desafio.API.Servicos.Interfaces
{
    public interface ITokenServico
    {
        /// <summary>
        /// Cria um token para autenticação.
        /// </summary>
        Tuple<HttpStatusCode, object> CriarToken();
    }
}
