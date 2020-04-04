using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Workshop.IntegrationTests.Platform.Jwt;

namespace Workshop.IntegrationTests.Api.Configuration
{
    public static class SecurityConfiguration
    {
        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            // Configures asp.net to use JWT authentication
            services
                .AddAuthentication(opts =>
                {
                    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(opts =>
                {
                    // TODO: Remove from prod
                    opts.RequireHttpsMetadata = false;

                    opts.SaveToken = true;
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = JwtSettings.Key,
                        ValidAudience = JwtSettings.Audience,
                        ValidIssuer = JwtSettings.Issuer,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
    }
}
