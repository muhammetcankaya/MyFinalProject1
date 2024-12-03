using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
// SecurityKeyHelper, uygulamanızda kullanılan
// güvenlik anahtarlarını oluşturmak için bir yardımcı sınıftır.
// Microsoft.IdentityModel.Tokens kütüphanesini kullanarak, 
// simetrik bir güvenlik anahtarı oluşturur.
namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        //Amaç: Güvenlik anahtarını oluşturmak için yardımcı bir sınıf sağlar.
        //JWT oluşturulurken gerekli olan SecurityKey nesnesini üretir.
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        //SymmetricSecurityKey: Simetrik şifreleme için kullanılan bir güvenlik anahtarını temsil eder.
        //Simetrik: Aynı anahtar, hem şifreleme hem de şifre çözme işlemleri için kullanılır.
        }
    }
}
