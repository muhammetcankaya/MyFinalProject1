using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }// apideki appsetingi okumaya yarıyor
        private TokenOptions _tokenOptions;//bunu oluşturcaz tokenın değerleri
        private DateTime _accessTokenExpiration;//accestoken ne zaman bitedcel
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();// git appsetinge token Options bölümünü al TopkenOptionsa yerleştir değerleri 

        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)// Bu kullanıcı içien bir tane token üreticez
            //bana user bilgilerini ve claimi ver
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);//süreyi veriyoruz apsetingteki tokenoptionsa gönderdik ordan aldık
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);// bana bir security key lazım e ben securitykeyhelperda createsecuritykey yazmıştım onunlasecurity key oluşturabilirsin içinede appsetingdekini attık
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);// Bu securitykey imzaladık daha güvenli hale getirdik 
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims); // ilgili optionsları kullanarak bu kullanıcıya ilgili credentials lar ve claimler ile securityToken oluşturmak jwt 
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)// jwt oluşturmak için yazdığımız privite metod
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)//claim oluşturmak için yazılan metod
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }
}
