using GerEsportes_BackEnd.Dominios.Dtos;
using System.Security.Claims;

namespace GerEsportes_BackEnd.Aplicacao.Servicos
{
    public interface IServicoToken
    {
        string GenerateToken(UserAuth user);
        string GenerateRefreshToken();
        void SaveRefreshToken(string username, string refreshToken);
        void DeleteRefreshToken(string username, string refreshToken);
        string GetRefreshToken(string username);
        ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token);
        string GenerateToken(IEnumerable<Claim> user);
        string GetRefreshTokenAtualizar(string username, string refreshToken);
    }
}
