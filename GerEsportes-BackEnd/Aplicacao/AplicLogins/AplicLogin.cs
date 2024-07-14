using GerEsportes_BackEnd.Aplicacao.Servicos;
using GerEsportes_BackEnd.Dominios.Dtos;
using GerEsportes_BackEnd.Dominios.Enuns;
using GerEsportes_BackEnd.Repositorios.Usuarios;
using Microsoft.IdentityModel.Tokens;

namespace GerEsportes_BackEnd.Aplicacao.AplicLogins
{
    public class AplicLogin : IAplicLogin
    {
        private readonly IRepUsuario _repUsuario;
        private readonly IServicoToken _servicoToken;

        public AplicLogin(IRepUsuario repUsuario, IServicoToken servicoToken)
        {
            _repUsuario = repUsuario;
            _servicoToken = servicoToken;
        }

        public dynamic Login(LoginDto dto)
        {
            dto.Email = FormatacaoEmail(dto.Email);

            var usuario = _repUsuario.RecuperarTodos().Find(p => p.Email == dto.Email);

            if (usuario == null)
                throw new Exception($"Usuário com o email {dto.Email} não encontrado!");

            if (!usuario.Senha.Equals(dto.Senha))
                throw new Exception("Senha incorreta!");

            var userAuth = new UserAuth()
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Password = usuario.Senha,
                Roles = ResolvedorRegrasTipoUsuario(usuario.TipoUsuario)
            };

            var token = _servicoToken.GenerateToken(userAuth);
            var refreshToken = _servicoToken.GenerateRefreshToken();
            _servicoToken.SaveRefreshToken(userAuth.Email, refreshToken);

            userAuth.Password = "";

            return new
            {
                userAuth = userAuth,
                token = token,
                refreshToken = refreshToken,
                codigoUsuario = usuario.Id
            }; 
        }

        public dynamic Refresh(RefreshDto dto)
        {
            var principal = _servicoToken.GetClaimsPrincipalFromExpiredToken(dto.Token);
            var username = principal.Identity.Name;
            var savedRefreshToken = _servicoToken.GetRefreshTokenAtualizar(username, dto.RefreshToken);
            if (savedRefreshToken != dto.RefreshToken)
                throw new SecurityTokenException("Token Inválido!");

            var newJwtToken = _servicoToken.GenerateToken(principal.Claims);
            var newRefreshToken = _servicoToken.GenerateRefreshToken();
            _servicoToken.DeleteRefreshToken(username, dto.RefreshToken);
            _servicoToken.SaveRefreshToken(username, newRefreshToken);

            return new 
            {
                token = newJwtToken,
                refreshToken = newRefreshToken
            };
        }   

        private string[] ResolvedorRegrasTipoUsuario(EnumTipoUsuario tipoUsuario)
        {
            if (tipoUsuario == EnumTipoUsuario.ADMINISTRADOR)
            {
                var ret = new string[3];

                ret[0] = EnumTipoUsuario.ADMINISTRADOR.ToString();
                ret[1] = EnumTipoUsuario.TECNICO.ToString();
                ret[2] = EnumTipoUsuario.ATLETA.ToString();

                return ret;
            }

            if (tipoUsuario == EnumTipoUsuario.TECNICO)
            {
                var ret = new string[1];

                ret[0] = EnumTipoUsuario.TECNICO.ToString();

                return ret;
            }

            if (tipoUsuario == EnumTipoUsuario.ATLETA)
            {
                var ret = new string[1];
                ret[0] = EnumTipoUsuario.ATLETA.ToString();

                return ret;
            
            }

            return null;
        }

        private string FormatacaoEmail(string email)
        {
            string[] partes = email.Split(' ');

            string emailFormatado = string.Join("", partes);

            return emailFormatado.ToLower();
        }
    }
}
