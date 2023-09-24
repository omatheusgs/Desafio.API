using Desafio.API.Entities;
using System.Net;

namespace Desafio.API.Servicos.Interfaces
{
    public interface IPessoaServico
    {
        /// <summary>
        /// Retorna os dados da pessoa pelo seu código.
        /// </summary>
        /// <param name="codigo">Código para consulta.</param>
        /// <returns>O código HTTP para a requisição, juntamente com os dados da <see cref="Pessoa"/> (se existir).</returns>
        Tuple<HttpStatusCode, object> ObtenhaPessoaPeloCodigo(int codigo);

        /// <summary>
        /// Retorna os dados das pessoas através da UF.
        /// </summary>
        /// <param name="uf">UF para consulta.</param>
        /// <returns>O código HTTP para a requisição, juntamente com os dados das pessoas encontradas. Veja: <see cref="Pessoa"/>.</returns>
        Tuple<HttpStatusCode, object> ObtenhaPessoasPelaUF(string uf);

        /// <summary>
        /// Retorna todas as pessoas cadastradas.
        /// </summary>
        /// <returns>O código HTTP para a requisição, juntamente com os dados das pessoas encontradas. Veja: <see cref="Pessoa"/>.</returns>
        Tuple<HttpStatusCode, object> ObtenhaTodasAsPessoas();

        /// <summary>
        /// Adiciona os dados da pessoa.
        /// </summary>
        /// <param name="pessoa">Dados da pessoa a ser adicionada.</param>
        /// <returns>O código HTTP para a requisição, juntamente com os dados da <see cref="Pessoa"/> informada.</returns>
        Tuple<HttpStatusCode, object> AdicionarPessoa(Pessoa pessoa);

        /// <summary>
        /// Atualiza os dados da pessoa.
        /// </summary>
        /// <param name="pessoa">Dados da pessoa a ser atualizada.</param>
        /// <returns>O código HTTP para a requisição, juntamente com os dados da <see cref="Pessoa"/> atualizada.</returns>
        Tuple<HttpStatusCode, object> AtualizarPessoa(Pessoa pessoa);

        /// <summary>
        /// Exclui os dados da pessoa.
        /// </summary>
        /// <param name="codigo">Código da pessoa a ser excluída.</param>
        /// <returns>O código HTTP para a requisição, juntamente com uma mensagem informativa da ação.</returns>
        Tuple<HttpStatusCode, object> ExcluirPessoa(int codigo);
    }
}