namespace EventManager.App.Api.Basic.Middleware;

using EventManager.App.Api.Basic.Constants;
using EventManager.App.Api.Basic.Interfaces;
using EventManager.App.Api.Basic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The <see cref="JwtMiddleware"/> class represents a middleware for handling JWT operations.
/// </summary>
public class JwtMiddleware
{
    private readonly RequestDelegate next;
    private readonly JwtConfig jwtConfig;
    private readonly IUserRepository userService;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="jwtConfigOptions">The JWT configuration options.</param>
    /// <param name="userRepository">The user repository.</param>
    public JwtMiddleware(RequestDelegate next, IOptions<JwtConfig> jwtConfigOptions, IUserRepository userRepository)
    {
        this.next = next;
        jwtConfig = jwtConfigOptions.Value;
        userService = userRepository;
    }

    /// <summary>
    /// Invokes the middleware.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task Invoke(HttpContext context)
    {
        string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token is not null)
        {
            await ValidateRoleAndAttachUserToContext(context, token).ConfigureAwait(false);
        }
        else
        {
            await next(context).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Represents an asynchronous operation that produces a result of type <see cref="Task"/>.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="token">The token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    private async Task ValidateRoleAndAttachUserToContext(HttpContext context, string token)
    {
        try
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(jwtConfig.Key);
            _ = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidAudience = jwtConfig.Audience,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;

            // Get roles from jwt token claims
            List<string> userRoles = jwtToken.Claims.Where(x => x.Type == "role").Select(x => x.Value).ToList();

            // Get AuthRef from jwt token claims
            string userSecurityKey = jwtToken.Claims.First(x => x.Type == "certserialnumber").Value;

            // Get user id from jwt token claims
            string userId = jwtToken.Claims.First(x => x.Type == "primarysid").Value;

            // attach user to context on successful jwt validation
            User user = userService.GetById(userId);

            // Revalidate roles from token.
            if (userRoles.Except(user.Roles.Split(",")).Any() || userRoles.Count != user.Roles.Split(",").Length || userSecurityKey != user.SecurityKey)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else
            {
                context.Items[NameConstants.USER_KEY] = user;
                await next(context).ConfigureAwait(false);
            }
        }
        catch
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
    }
}
