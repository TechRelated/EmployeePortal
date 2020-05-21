using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePortal.Helper
{
    public class VerifyToken
    {
        public async Task<JwtSecurityToken> ValidateToken_new(string authorizationHeader)
        {
            var issuer = "http://localhost:64558/";  //api
            var audience = "http://localhost:64466/"; //Web App
            var Secret = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING";
            var signingKeys = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
            
            string token = authorizationHeader.Substring("bearer".Length).Trim();

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = signingKeys,
                    ValidateLifetime = true
                };

                var principal = new JwtSecurityTokenHandler()
                 .ValidateToken(token, validationParameters, out var rawValidatedToken);

                return (JwtSecurityToken)rawValidatedToken;
            }
            catch (SecurityTokenValidationException)
            {
                return null;
            }
        }

        public bool ValidateCurrentToken(string authorizationHeader)
        {
            var mySecret = "THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING";
            var signingKeys = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var issuer = "http://localhost:64558/";  //api
            var audience = "http://localhost:64466/"; //Web App

            string token = authorizationHeader.Substring("bearer".Length).Trim();

            var tokenHandler = new JwtSecurityTokenHandler();
             try
             {
                 tokenHandler.ValidateToken(token, new TokenValidationParameters
                 {
                     RequireExpirationTime = true,
                     RequireSignedTokens = true,
                     ValidateIssuerSigningKey = true,
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidIssuer = issuer,
                     ValidAudience = audience,
                     IssuerSigningKey = signingKeys,
                     ValidateLifetime = true
                 }, out SecurityToken validatedToken);
             }
             catch
             {
                 return false;
             }
             return true;
        }
    }
}
