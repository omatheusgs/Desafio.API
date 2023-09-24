using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Desafio.API.Dominio.Autenticacao
{
    public class GerarToken
    {
        public static string GerarTokenParaAutenticacao()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var chaveParaTokenDeAutenticacao = Encoding.ASCII.GetBytes(TokenDeAutenticacao);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(chaveParaTokenDeAutenticacao), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public const string TokenDeAutenticacao = "8o5g1456-4e0a-73tt-qo31-5217ac671041";
    }
}