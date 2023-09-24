using Desafio.API.Entities;
using Desafio.API.Servicos.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace Desafio.API.Servicos
{
    public class PessoaServico : IPessoaServico
    {
        /// <summary>
        /// Lista de pessoas salvas na memória da aplicação.
        /// </summary>
        public static List<Pessoa> Pessoas = new List<Pessoa>()
        {
            new Pessoa(1, "Matheus Gomes Brandão", "944.190.680-86", "GO", new DateTime(1998, 09, 08))
        };

        public Tuple<HttpStatusCode, object> ObtenhaPessoaPeloCodigo(int codigo)
        {
            try
            {
                var pessoa = Pessoas.FirstOrDefault(p => p.Codigo == codigo);
                if (pessoa == null)
                    return new Tuple<HttpStatusCode, object>(HttpStatusCode.NotFound, "Nenhum registro encontrado com o código informado.");

                return new Tuple<HttpStatusCode, object>(HttpStatusCode.OK, pessoa);
            }
            catch (Exception)
            {
                return new Tuple<HttpStatusCode, object>(HttpStatusCode.InternalServerError, "Ocorreu um erro inesperado na tentativa de adicionar o usuário. Contate o administrador do sistema.");
            }
        }

        public Tuple<HttpStatusCode, object> ObtenhaPessoasPelaUF(string uf)
        {
            try
            {
                var pessoas = Pessoas.Where(c => c.UF.ToUpper() == uf.Trim().ToUpper()).ToList();
                if (!pessoas.Any())
                    return new Tuple<HttpStatusCode, object>(HttpStatusCode.NotFound, "Nenhum registro encontrado com a uf informada.");

                return new Tuple<HttpStatusCode, object>(HttpStatusCode.OK, pessoas);
            }
            catch (Exception)
            {
                return new Tuple<HttpStatusCode, object>(HttpStatusCode.InternalServerError, "Ocorreu um erro inesperado na tentativa de adicionar o usuário. Contate o administrador do sistema.");
            }
        }

        public Tuple<HttpStatusCode, object> ObtenhaTodasAsPessoas()
        {
            try
            {
                if (!Pessoas.Any())
                    return new Tuple<HttpStatusCode, object>(HttpStatusCode.NotFound, "Nenhum registro encontrado.");

                return new Tuple<HttpStatusCode, object>(HttpStatusCode.OK, Pessoas);
            }
            catch (Exception)
            {
                return new Tuple<HttpStatusCode, object>(HttpStatusCode.InternalServerError, "Ocorreu um erro inesperado na tentativa de adicionar o usuário. Contate o administrador do sistema.");
            }
        }

        public Tuple<HttpStatusCode, object> AdicionarPessoa(Pessoa pessoa)
        {
            try
            {
                if (Pessoas.Any(x => x.Codigo == pessoa.Codigo))
                    return new Tuple<HttpStatusCode, object>(HttpStatusCode.Conflict, "Já existe uma pessoa adicionada com o código informado.");

                var erros = ValidacoesPessoa(pessoa);
                if (erros.Any())
                    return new Tuple<HttpStatusCode, object>(HttpStatusCode.BadRequest, erros);

                if (pessoa.Codigo == 0 && Pessoas.Any())
                    pessoa.Codigo = Pessoas.Max(c => c.Codigo) + 1;
                else
                    pessoa.Codigo = 1;

                pessoa.UF = pessoa.UF.Trim().ToUpper();
                pessoa.CPF = Pessoa.FormatarCPF(pessoa.CPF);

                Pessoas.Add(pessoa);

                return new Tuple<HttpStatusCode, object>(HttpStatusCode.Created, pessoa);
            }
            catch (Exception)
            {
                return new Tuple<HttpStatusCode, object>(HttpStatusCode.InternalServerError, "Ocorreu um erro inesperado na tentativa de adicionar o usuário. Contate o administrador do sistema.");
            }
        }

        public Tuple<HttpStatusCode, object> AtualizarPessoa(Pessoa pessoa)
        {
            try
            {
                var pessoaPersistida = Pessoas.FirstOrDefault(x => x.Codigo == pessoa.Codigo);
                if (pessoaPersistida == null)
                    return new Tuple<HttpStatusCode, object>(HttpStatusCode.NotFound, "Nenhum registro encontrado.");

                var erros = ValidacoesPessoa(pessoa);
                if (erros.Any())
                    return new Tuple<HttpStatusCode, object>(HttpStatusCode.BadRequest, erros);

                AtualizarDadosDaPessoa(pessoa, pessoaPersistida);

                return new Tuple<HttpStatusCode, object>(HttpStatusCode.OK, pessoaPersistida);
            }
            catch (Exception)
            {
                return new Tuple<HttpStatusCode, object>(HttpStatusCode.InternalServerError, "Ocorreu um erro inesperado na tentativa de adicionar o usuário. Contate o administrador do sistema.");
            }
        }

        public Tuple<HttpStatusCode, object> ExcluirPessoa(int codigo)
        {
            try
            {
                var pessoaPersistida = Pessoas.FirstOrDefault(x => x.Codigo == codigo);
                if (pessoaPersistida == null)
                    return new Tuple<HttpStatusCode, object>(HttpStatusCode.NotFound, "Nenhum registro encontrado com o código informado.");

                Pessoas.Remove(pessoaPersistida);

                return new Tuple<HttpStatusCode, object>(HttpStatusCode.OK, "Registro excluído com sucesso.");
            }
            catch (Exception)
            {
                return new Tuple<HttpStatusCode, object>(HttpStatusCode.InternalServerError, "Ocorreu um erro inesperado na tentativa de adicionar o usuário. Contate o administrador do sistema.");
            }
        }

        private static void AtualizarDadosDaPessoa(Pessoa pessoa, Pessoa pessoaPersistida)
        {
            pessoaPersistida.Nome = pessoa.Nome;
            pessoaPersistida.CPF = Pessoa.FormatarCPF(pessoa.CPF);
            pessoaPersistida.UF = pessoa.UF.Trim().ToUpper();
            pessoaPersistida.DataDeNascimento = pessoa.DataDeNascimento;
        }

        private Dictionary<string, string[]> ValidacoesPessoa(Pessoa pessoa)
        {
            var errosDeValidacao = new Dictionary<string, string[]>();

            if (!ListaDeUFs().Contains(pessoa.UF.Trim().ToUpper()))
                errosDeValidacao.Add(nameof(pessoa.UF), new string[] { "A UF informada é inválida." });

            if (!Pessoa.CPFValido(pessoa.CPF))
                errosDeValidacao.Add(nameof(pessoa.CPF), new string[] { "O CPF informado é inválido." });

            if (pessoa.DataDeNascimento?.Date >= DateTime.Now.Date)
                errosDeValidacao.Add(nameof(pessoa.DataDeNascimento), new string[] { "A data de nascimento não pode ser maior que a data atual." });

            return errosDeValidacao;
        }

        private List<string> ListaDeUFs()
        {
            return new List<string> { "AC", "AL", "AP", "AM", "BA", "CE", "DF", "GO", "ES", "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI", "RJ", "RN", "RS", "RO", "RR", "SP", "SC", "SE", "TO" };
        }
    }
}