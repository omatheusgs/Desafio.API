using Desafio.API.Dominio.Autenticacao;
using Desafio.API.Servicos.Interfaces;
using System.Net;

namespace Desafio.API.Servicos
{
    public class TokenServico : ITokenServico
    {
        public Tuple<HttpStatusCode, object> CriarToken()
        {
            try
            {
                var token = $"Token: {GerarToken.GerarTokenParaAutenticacao()}";
                return new Tuple<HttpStatusCode, object>(HttpStatusCode.OK, token);
            }
            catch (Exception)
            {
                return new Tuple<HttpStatusCode, object>(HttpStatusCode.InternalServerError, "Ocorreu um erro inesperado na tentativa de adicionar o usuário. Contate o administrador do sistema.");
            }
        }
    }
}