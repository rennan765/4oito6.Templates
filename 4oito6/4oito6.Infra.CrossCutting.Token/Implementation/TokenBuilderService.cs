using _4oito6.Infra.CrossCutting.Configuration.Token.Interfaces;
using _4oito6.Infra.CrossCutting.Token.Interfaces;
using _4oito6.Infra.CrossCutting.Token.Models;
using _4oito6.Infra.Data.Cache.Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading.Tasks;

namespace _4oito6.Infra.CrossCutting.Token.Implementation
{
    public class TokenBuilderService : ITokenBuilderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenConfiguration _tokenConfiguration;
        private readonly ISigningConfiguration _signingConfiguration;
        private readonly ICacheRepository _cacheRepository;

        public TokenBuilderService(IHttpContextAccessor httpContextAccessor, ITokenConfiguration tokenConfiguration, ISigningConfiguration signingConfiguration, ICacheRepository cacheRepository)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _tokenConfiguration = tokenConfiguration ?? throw new ArgumentNullException(nameof(tokenConfiguration));
            _signingConfiguration = signingConfiguration ?? throw new ArgumentNullException(nameof(signingConfiguration));
            _cacheRepository = cacheRepository ?? throw new ArgumentNullException(nameof(cacheRepository));
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

        public Task<object> BuildTokenAsync(int id, string email, string image)
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

            return Task.FromResult((object)handler.WriteToken(securityToken));
        }

        public async Task<RefreshTokenModel> BuildRefreshTokenAsync(int id)
        {
            var token = new RefreshTokenModel
            (
                refreshToken: GenerateRefreshToken(),
                data: new RefreshTokenData
                (
                    id: id,
                    expiresOn: DateTime.UtcNow.AddSeconds(_tokenConfiguration.RefreshTokenTime)
                )
            );

            await _cacheRepository
                .SetAsync
                (
                    key: token.RefreshToken,
                    value: JsonConvert.SerializeObject(token.Data)
                )
                .ConfigureAwait(false);

            return token;
        }

        public Task<TokenModel> GetTokenAsync()
        {
            if (_httpContextAccessor.HttpContext.Items[typeof(TokenModel).ToString()] == null)
            {
                var user = _httpContextAccessor.HttpContext.User.FindFirst(typeof(TokenModel).ToString());

                if (user != null)
                {
                    if (_httpContextAccessor.HttpContext.Items[typeof(TokenModel).ToString()] is null)
                        _httpContextAccessor.HttpContext.Items[typeof(TokenModel).ToString()] = JsonConvert.DeserializeObject<TokenModel>(user.Value);
                }
            }

            return Task.FromResult((TokenModel)_httpContextAccessor.HttpContext.Items[typeof(TokenModel).ToString()]);
        }

        public async Task<RefreshTokenModel> GetRefreshTokenAsync(string key)
        {
            var data = await _cacheRepository.GetAsync(key).ConfigureAwait(false);

            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            return new RefreshTokenModel
            (
                refreshToken: key,
                data: JsonConvert.DeserializeObject<RefreshTokenData>(data)
            );
        }

        public async Task<bool> IsRefreshTokenValid(string key)
        {
            var data = await _cacheRepository.GetAsync(key).ConfigureAwait(false);

            if (string.IsNullOrEmpty(data))
            {
                return false;
            }

            return _tokenConfiguration.RefreshTokenTime >=
                (DateTime.UtcNow - (JsonConvert.DeserializeObject<RefreshTokenData>(data)).ExpiresOn)
                    .TotalSeconds;
        }

        public Task RemoveRefreshTokenAsync(string key) => _cacheRepository.RemoveAsync(key);
    }
}