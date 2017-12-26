using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace OYMLCN.Web.Mvc
{
    public static class JsonWebToken
    {
        public sealed class JwtToken
        {
            internal JwtToken(JwtSecurityToken token, int expires)
            {
                this.token = new JwtSecurityTokenHandler().WriteToken(token);
                this.expires = expires;
            }

            [JsonIgnore]
            internal string token { get; private set; }
            [JsonIgnore]
            internal int expires { get; private set; }

            public string access_token => token;
            public string refresh_token => Guid.NewGuid().ToString("N");
            public int expires_in => expires;
        }
        public sealed class JwtTokenBuilder
        {
            private SecurityKey securityKey = null;
            private string subject = "";
            private string issuer = "";
            private string audience = "";
            private Dictionary<string, string> claims = new Dictionary<string, string>();
            private int expiryInMinutes = 5;

            public JwtTokenBuilder(string secret, string subject, string issuer, string audience) :
                this(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)), subject, issuer, audience)
            { }

            public JwtTokenBuilder(SecurityKey securityKey, string subject, string issuer, string audience)
            {
                this.securityKey = securityKey;
                this.subject = subject;
                this.issuer = issuer;
                this.audience = audience;
            }

            public JwtTokenBuilder AddClaim(string type, string value)
            {
                this.claims.Add(type, value);
                return this;
            }

            public JwtTokenBuilder AddClaims(Dictionary<string, string> claims)
            {
                this.claims.Union(claims);
                return this;
            }

            public JwtTokenBuilder AddExpiry(int expiryInMinutes)
            {
                this.expiryInMinutes = expiryInMinutes;
                return this;
            }

            public JwtToken Build()
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, this.subject),
                    //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }
                .Union(this.claims.Select(item => new Claim(item.Key, item.Value)));

                var token = new JwtSecurityToken(
                                  issuer: this.issuer,
                                  audience: this.audience,
                                  claims: claims,
                                  expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                                  signingCredentials: new SigningCredentials(
                                                            this.securityKey,
                                                            SecurityAlgorithms.HmacSha256));

                return new JwtToken(token, expiryInMinutes * 60);
            }
        }

    }
}
