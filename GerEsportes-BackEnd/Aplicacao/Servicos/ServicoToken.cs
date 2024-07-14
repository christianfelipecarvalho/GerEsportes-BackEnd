using GerEsportes_BackEnd.Dominios.Dtos;
using GerEsportes_BackEnd.Infra;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GerEsportes_BackEnd.Aplicacao.Servicos
{
    public class ServicoToken : IServicoToken
    {
        public string GenerateToken(UserAuth user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(ConfigurationSettingsToken.PrivateKey);

            var credencials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaimsRole(user),
                SigningCredentials = credencials,
                Expires = DateTime.UtcNow.AddHours(2),
            };

            var token = handler.CreateJwtSecurityToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        public string GenerateToken(IEnumerable<Claim> user)
        {
            var handler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(ConfigurationSettingsToken.PrivateKey);

            var credencials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(user),
                SigningCredentials = credencials,
                Expires = DateTime.UtcNow.AddHours(2),
            };

            var token = handler.CreateJwtSecurityToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaimsRole(UserAuth user)
        {
            var ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(ClaimTypes.Name, user.Email));

            foreach (var role in user.Roles)
                ci.AddClaim(new Claim(ClaimTypes.Role, role));

            return ci;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationSettingsToken.PrivateKey)),
                ValidateLifetime = false,
            };

            var tokenHandelr = new JwtSecurityTokenHandler();
            var principal = tokenHandelr.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token inválido!");

            return principal;
        }

        private static List<(string, string)> _refreshTokens = new();

        public void SaveRefreshToken(string username, string refreshToken)
        {
            _refreshTokens.Add(new(username, refreshToken));
        }

        public string GetRefreshToken(string username)
        {
            return _refreshTokens.FirstOrDefault(x => x.Item1 == username).Item2;
        }

        public string GetRefreshTokenAtualizar(string username, string refreshToken)
        {
            return _refreshTokens.FirstOrDefault(x => x.Item1 == username && x.Item2.Equals(refreshToken)).Item2;
        }

        public void DeleteRefreshToken(string username, string refreshToken)
        {
            var item = _refreshTokens.FirstOrDefault(x => x.Item1 != username && x.Item2 == refreshToken);
            _refreshTokens.Remove(item);
        }
    }
}
