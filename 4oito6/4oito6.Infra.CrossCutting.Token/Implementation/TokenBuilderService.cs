using _4oito6.Infra.CrossCutting.Configuration.Token.Interfaces;
using _4oito6.Infra.CrossCutting.Token.Interfaces;
using _4oito6.Infra.CrossCutting.Token.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;

namespace _4oito6.Infra.CrossCutting.Token.Implementation
{
    public class TokenBuilderService : ITokenBuilderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenConfiguration _tokenConfiguration;
        private readonly ISigningConfiguration _signingConfiguration;

        public TokenBuilderService(IHttpContextAccessor httpContextAccessor, ITokenConfiguration tokenConfiguration, ISigningConfiguration signingConfiguration)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenConfiguration = tokenConfiguration;
            _signingConfiguration = signingConfiguration;
        }

        private string GenerateRefreshToken()
        {
            string token;
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                token = Convert.ToBase64String(randomNumber);
            }

            return token.Replace("+", string.Empty)
                .Replace("=", string.Empty)
                .Replace("/", string.Empty);
        }

        public object BuildToken(int id, string email, string image)
        {
            var model = new TokenModel(id, email, image);

            var createDate = DateTime.UtcNow;
            var expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.TokenTime);

            var identity = new ClaimsIdentity
            (
                new GenericIdentity(model.Id.ToString(), "Id"),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(typeof(TokenModel).ToString(), JsonConvert.SerializeObject(model))
                }
            );

            _httpContextAccessor.HttpContext.User.AddIdentity(identity);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken
            (
                new SecurityTokenDescriptor
                {
                    Issuer = _tokenConfiguration.Issuer,
                    Audience = _tokenConfiguration.Audience,
                    SigningCredentials = _signingConfiguration.SigningCredentials,
                    Subject = identity,
                    NotBefore = createDate,
                    Expires = expirationDate
                }
            );

            return handler.WriteToken(securityToken);
        }

        public RefreshTokenModel BuildRefreshToken(int id, string email, string image) 
            => new RefreshTokenModel
            (
                refreshToken: GenerateRefreshToken(),
                data: new RefreshTokenData
                (
                    id: id,
                    email: email,
                    image: image,
                    expiresOn: DateTime.UtcNow.AddSeconds(_tokenConfiguration.RefreshTokenTime)
                )
            );
    }
}