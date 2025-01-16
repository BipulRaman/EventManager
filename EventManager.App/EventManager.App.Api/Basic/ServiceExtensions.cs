namespace EventManager.App.Api.Basic;

using EventManager.App.Api.Basic.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

/// <summary>
/// The <see cref="ServiceExtensions"/> class provides extension methods for the <see cref="IServiceCollection"/> class.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Adds authentication setup using JWT bearer token.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the authentication setup to.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> containing the JWT configuration.</param>
    public static void AddAuthenticationSetup(this IServiceCollection services, IConfiguration configuration)
    {
        JwtConfig jwtConfig = configuration.GetSection(nameof(JwtConfig)).Get<JwtConfig>();
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidAudience = jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key)),
            };
        });
    }

    /// <summary>
    /// Adds Swagger setup for JWT authentication.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the Swagger setup to.</param>
    public static void AddSwaggerSetup(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                    {
                        jwtSecurityScheme,
                        Array.Empty<string>()
                    }
            });
        });
    }
}
