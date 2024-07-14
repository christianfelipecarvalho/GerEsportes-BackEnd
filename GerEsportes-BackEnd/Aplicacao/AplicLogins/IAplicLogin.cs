using GerEsportes_BackEnd.Dominios.Dtos;
using System.IdentityModel.Tokens.Jwt;

namespace GerEsportes_BackEnd.Aplicacao.AplicLogins
{
    public interface IAplicLogin
    {
        dynamic Login(LoginDto dto);
        dynamic Refresh(RefreshDto dto);
    }
}
