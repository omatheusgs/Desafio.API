using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace Desafio.API.Entities
{
    public class Pessoa
    {
        public Pessoa(int codigo, string nome, string cpf, string uf, DateTime? dataDeNascimento)
        {
            Codigo = codigo;
            Nome = nome;
            CPF = cpf;
            UF = uf;
            DataDeNascimento = dataDeNascimento;
        }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório."), MaxLength(100, ErrorMessage = "Máximo 100 carateres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório."), MaxLength(14, ErrorMessage = "Máximo 14 carateres.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "A UF é obrigatória."), MaxLength(2, ErrorMessage = "Máximo 2 carateres.")]
        public string UF { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatório.")]
        public DateTime? DataDeNascimento { get; set; }

        public static bool CPFValido(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        public static string FormatarCPF(string cpf)
        {
            if (cpf.IsNullOrEmpty()) 
                return string.Empty;

            return Convert.ToUInt64(cpf.Replace(".", "").Replace("-", "")).ToString(@"000\.000\.000\-00");
        }
    }
}