using Desafio.API.Entities;
using Desafio.API.Servicos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API.Controllers
{
    /// <summary>
    /// Rotas relacionadas as ações da <see cref="Pessoa"/>.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/Pessoa")]
    [ProducesResponseType(typeof(string), 400)]
    [ProducesResponseType(typeof(string), 401)]
    [ProducesResponseType(typeof(string), 500)]
    public class PessoaController : Controller
    {
        private readonly IPessoaServico _pessoaServico;

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public PessoaController(IPessoaServico pessoaServico)
        {
            _pessoaServico = pessoaServico;
        }

        /// <summary>
        /// Retorna a consulta de uma pessoa com o código informado.
        /// </summary>
        /// <param name="codigo">Código da pessoa.</param>
        /// <returns>A pessoa no cache. Veja: <see cref="Pessoa"/>.</returns>
        [HttpGet("/ConsultarPessoaPeloCodigo/{codigo}")]
        [ProducesResponseType(typeof(Pessoa), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult ConsultarPessoaPeloCodigo([FromRoute] int codigo)
        {
            var retorno = _pessoaServico.ObtenhaPessoaPeloCodigo(codigo);
            return StatusCode((int)retorno.Item1, retorno.Item2);
        }

        /// <summary>
        /// Retorna uma lista de pessoas que residem na UF informada.
        /// </summary>
        /// <param name="uf">UF da pessoa.</param>
        /// <returns>A lista de pessoas no cache para a UF informada. Veja: <see cref="Pessoa"/>.</returns>
        [HttpGet("/ObtenhaPessoasPelaUF/{uf}")]
        [ProducesResponseType(typeof(Pessoa), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult ObtenhaPessoasPelaUF([FromRoute] string uf)
        {
            var retorno = _pessoaServico.ObtenhaPessoasPelaUF(uf);
            return StatusCode((int)retorno.Item1, retorno.Item2);
        }

        /// <summary>
        /// Retorna uma lista de pessoas.
        /// </summary>
        /// <returns>A lista de pessoas no cache. Veja: <see cref="Pessoa"/>.</returns>
        [HttpGet("/ObtenhaTodasAsPessoas")]
        [ProducesResponseType(typeof(Pessoa), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult ObtenhaTodasAsPessoas()
        {
            var retorno = _pessoaServico.ObtenhaTodasAsPessoas();
            return StatusCode((int)retorno.Item1, retorno.Item2);
        }

        /// <summary>
        /// Salvar o cadastro da pessoa informada no cache.
        /// </summary>
        /// <param name="pessoa">Dados da pessoa a ser cadastrada.</param>
        /// <returns>A pessoa que foi registrado no cache. Veja: <see cref="Pessoa"/>.</returns>
        [HttpPost("/AdicionarPessoa")]
        [ProducesResponseType(typeof(Pessoa), 201)]
        [ProducesResponseType(typeof(string), 409)]
        public ActionResult AdicionarPessoa([FromBody] Pessoa pessoa)
        {
            var retorno = _pessoaServico.AdicionarPessoa(pessoa);
            return StatusCode((int)retorno.Item1, retorno.Item2);
        }

        /// <summary>
        /// Atualiza o cadastro de uma pessoa, atrvés do código.
        /// </summary>
        /// <param name="pessoa">Dados da pessoa para ser atualizada.</param>
        /// <returns>Os dados da pessoa atualizada. Veja: <see cref="Pessoa"/>.</returns>
        [HttpPut("/AtualizarPessoa")]
        [ProducesResponseType(typeof(Pessoa), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult AtualizarPessoa([FromBody] Pessoa pessoa)
        {
            var retorno = _pessoaServico.AtualizarPessoa(pessoa);
            return StatusCode((int)retorno.Item1, retorno.Item2);
        }

        /// <summary>
        /// Exclui o cadastro de uma pessoa do cache.
        /// </summary>
        /// <param name="codigo">Código da pessoa a ser excluída do cache.</param>
        /// <returns>Mensagem de sucesso.</returns>
        [HttpDelete("/ExcluirPessoa/{codigo}")]
        [ProducesResponseType(typeof(Pessoa), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public ActionResult ExcluirPessoa([FromRoute] int codigo)
        {
            var retorno = _pessoaServico.ExcluirPessoa(codigo);
            return StatusCode((int)retorno.Item1, retorno.Item2);
        }
    }
}
