using Desafio.API.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API.Controllers
{
    /// <summary>
    /// Rota relacionada ao token.
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    [Route("api/Autenticacao")]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public class AutenticacaoController : Controller
    {
        private readonly ITokenServico _tokenServico;

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public AutenticacaoController(ITokenServico tokenServico)
        {
            _tokenServico = tokenServico;
        }

        /// <summary>
        /// Cria o token de autenticação para acessar a API.
        /// </summary>
        [HttpPost("CriarToken")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult CriarToken()
        {
            var retorno = _tokenServico.CriarToken();
            return StatusCode((int)retorno.Item1, retorno.Item2);
        }
    }
}
