using _4oito6.Infra.CrossCutting.Configuration.Token.Implementation;
using _4oito6.Infra.CrossCutting.Configuration.Token.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace _4oito6.Infra.CrossCutting.IoC.Token
{
    public static class TokenResolver
    {
        private static IServiceCollection ResolveTokenConfigurations(this IServiceCollection services)
            => services
                .AddSingleton<ITokenConfiguration, TokenConfiguration>()
                .AddSingleton<ISigningConfiguration, SigningConfiguration>();

        public static IServiceCollection ResolveToken(this IServiceCollection services)
        {
            services.ResolveTokenConfigurations();

            var provider = services.BuildServiceProvider();

            ITokenConfiguration token = provider.GetService<ITokenConfiguration>();
            ISigningConfiguration signing = provider.GetService<ISigningConfiguration>();

            services.AddAuthentication
            (
                authOptions =>
                {
                    authOptions.DefaultAuthenticateScheme =
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            )
            .AddJwtBearer
            (
                bearerOptions =>
                {
                    var paramsValidation = bearerOptions.TokenValidationParameters;

                    paramsValidation.IssuerSigningKey = signing.SigningCredentials.Key;
                    paramsValidation.ValidAudience = token.Audience;
                    paramsValidation.ValidIssuer = token.Issuer;

                    // Valida a assinatura de um token recebido
                    paramsValidation.ValidateIssuerSigningKey = true;

                    // Verifica se um token recebido ainda é válido
                    paramsValidation.ValidateLifetime = true;

                    // Tempo de tolerância para a expiração de um token (utilizado caso haja problemas de sincronismo
                    //de horário entre diferentes computadores envolvidos no processo de comunicação)
                    paramsValidation.ClockSkew = TimeSpan.Zero;
                }
            );

            services.AddAuthorization
            (
                auth =>
                {
                    auth.AddPolicy
                    (
                        "Bearer",
                        new AuthorizationPolicyBuilder()
                            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                            .RequireAuthenticatedUser().Build()
                    );
                }
            );

            return services;
        }
    }
}