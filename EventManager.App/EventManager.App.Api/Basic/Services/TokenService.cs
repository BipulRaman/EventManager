namespace EventManager.App.Api.Basic.Services;

using EventManager.App.Api.Basic.Interfaces;
using EventManager.App.Api.Basic.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

/// <summary>
/// The <see cref="TokenService"/> class implements the <see cref="ITokenService"/> interface for handling token operations.
/// </summary>
public class TokenService : ITokenService
{
    private readonly JwtConfig jwtConfig;

    public TokenService(IOptions<JwtConfig> jwtConfig)
    {
        this.jwtConfig = jwtConfig.Value;
    }

    /// <inheritdoc/>
    public AuthTokenInfo GenerateToken(User user)
    {
        Dictionary<string, object> authClaims = new Dictionary<string, object>
        {
            { ClaimTypes.Name, user.Email },
            { JwtRegisteredClaimNames.Sub, "bipul.in" },
            { JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() },
            { JwtRegisteredClaimNames.Name, user.Name },
            { ClaimTypes.Role, user.Roles.Split(",") },
            { ClaimTypes.Email, user.Email },
            { ClaimTypes.PrimarySid, user.Id },
            { ClaimTypes.GivenName, user.Name },
            { ClaimTypes.SerialNumber, user.SecurityKey },
        };

        AuthTokenInfo authTokenInfo = GenerateAuthInfo(user.Email, authClaims);
        return authTokenInfo;
    }

    /// <inheritdoc/>
    public bool ValidateToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtConfig.Key);
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

            var jwtToken = (JwtSecurityToken)validatedToken;
            return jwtToken is not null;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Generate auth info for the user.
    /// </summary>
    /// <param name="userIdentity"></param>
    /// <returns></returns>
    private AuthTokenInfo GenerateAuthInfo(string userIdentity, Dictionary<string, object> authClaims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtConfig.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", userIdentity) }),
            Expires = DateTime.UtcNow.AddHours(500),
            NotBefore = DateTime.UtcNow,
            Issuer = jwtConfig.Issuer,
            Audience = jwtConfig.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            IssuedAt = DateTime.UtcNow,
            Claims = authClaims,
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        AuthTokenInfo authTokenInfo = new AuthTokenInfo()
        {
            AccessToken = tokenHandler.WriteToken(token),
            TokenType = "Bearer",
            Resource = userIdentity,
            ExpiresOn = (int)tokenDescriptor.Expires.Value.Subtract(DateTime.UnixEpoch).TotalSeconds,
            NotBefore = (int)tokenDescriptor.NotBefore.Value.Subtract(DateTime.UnixEpoch).TotalSeconds,
            ExpiresIn = (int)tokenDescriptor.Expires.Value.Subtract(DateTime.UtcNow).TotalSeconds,
            ExtExpiresIn = (int)tokenDescriptor.Expires.Value.Subtract(DateTime.UtcNow).TotalSeconds,
            Name = authClaims[ClaimTypes.GivenName].ToString(),
            UserId = authClaims[ClaimTypes.Name].ToString(),
            Roles = (authClaims[ClaimTypes.Role] as string[]).ToList(),
        };

        return authTokenInfo;
    }
}
