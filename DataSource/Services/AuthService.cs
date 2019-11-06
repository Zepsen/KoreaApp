using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using MediatR;
using DataSource.Handlers;

namespace Handlers.Services
{
    public interface IAuthService
    {
        string GenerateToken(string email);

        JwtSecurityToken Validate(string jwt);
    }

    public class AuthService : IAuthService
    {

        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;            
        }

        public string GenerateToken(string email)
        {
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var creds = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"])),
                SecurityAlgorithms.HmacSha256);

            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: _configuration["JwtIssuer"],
                audience: _configuration["JwtIssuer"],
                claims: claims,
                notBefore: now,
                expires: now.AddDays(double.Parse(_configuration["JwtExpiryInDays"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public JwtSecurityToken Validate(string jwt)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var validationParameters = new TokenValidationParameters
            {
                // Clock skew compensates for server time drift.
                // We recommend 5 minutes or less:
                ClockSkew = TimeSpan.FromMinutes(5),
                // Specify the key used to sign the token:
                IssuerSigningKey = key,
                RequireSignedTokens = true,
                // Ensure the token hasn't expired:
                RequireExpirationTime = true,
                ValidateLifetime = true,
                // Ensure the token audience matches our audience value (default true):
                ValidateAudience = true,
                ValidAudience = _configuration["JwtIssuer"],
                // Ensure the token was issued by a trusted authorization server (default true):
                ValidateIssuer = true,
                ValidIssuer = _configuration["JwtIssuer"]
            };

            try
            {
                var claimsPrincipal = new JwtSecurityTokenHandler()
                    .ValidateToken(jwt, validationParameters, out var rawValidatedToken);

                return (JwtSecurityToken)rawValidatedToken;
                // Or, you can return the ClaimsPrincipal
                // (which has the JWT properties automatically mapped to .NET claims)
            } catch (SecurityTokenValidationException stvex)
            {
                // The token failed validation!
                // TODO: Log it or display an error.
                throw new Exception($"Token failed validation: {stvex.Message}");
            } catch (ArgumentException argex)
            {
                // The token was not well-formed or was invalid for some other reason.
                // TODO: Log it or display an error.
                throw new Exception($"Token was invalid: {argex.Message}");
            }
        }


        //Cookies

        //public async Task<bool> Login(string userName, string password)
        //{
        //    if (ValidateLogin(userName, password))
        //    {
        //        var claims = new List<Claim>
        //        {
        //            new Claim("user", userName),
        //            new Claim("role", "Member")
        //        };

        //        await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "role")));
        //        return true;
        //    } else return false;            
        //}

        //private bool ValidateLogin(string userName, string password)
        //{
        //    return true;
        //}

//        public async Task Login(AppUser user)
//        {
//            var claims = new List<Claim>
//{
//                new Claim(ClaimTypes.Name, user.Email),
//                new Claim("FullName", user.Name),
//                new Claim(ClaimTypes.Role, "Administrator"),
//            };

//            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
//            var authProperties = new AuthenticationProperties
//            {
//                //AllowRefresh = <bool>,
//                // Refreshing the authentication session should be allowed.

//                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
//                // The time at which the authentication ticket expires. A 
//                // value set here overrides the ExpireTimeSpan option of 
//                // CookieAuthenticationOptions set with AddCookie.

//                //IsPersistent = true,
//                // Whether the authentication session is persisted across 
//                // multiple requests. When used with cookies, controls
//                // whether the cookie's lifetime is absolute (matching the
//                // lifetime of the authentication ticket) or session-based.

//                //IssuedUtc = <DateTimeOffset>,
//                // The time at which the authentication ticket was issued.

//                //RedirectUri = <string>
//                // The full path or absolute URI to be used as an http 
//                // redirect response value.
//            };

//            await _httpContextAccessor.HttpContext.SignInAsync(
//                CookieAuthenticationDefaults.AuthenticationScheme,
//                new ClaimsPrincipal(claimsIdentity),
//                authProperties);
//        }
    }

    public class FakeAuthenticationStateProvider : AuthenticationStateProvider
    {
        private IMediator _mediator;

        public FakeAuthenticationStateProvider(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = await _mediator.Send(new UserQuery.Request() { Id = 1 });

            var identity = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, "User")
                }, "FakeAuthType"); //set type for IsAuth = true

            var principal = new ClaimsPrincipal(identity);            
            return new AuthenticationState(principal);
        }
    }

}
