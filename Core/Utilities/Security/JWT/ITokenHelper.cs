using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {

        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
// şimdi bu napıyor arayüz ve Apı olarak düşünelim arayüzde
// kullanıcı griş yaptı onun bir claimi var veri tabanında
// eğer doğruysa veri tabanına gidicek claimlerini bulucak orada bir jwt
// oluşturacak onları arayüze gönderecek bu arkadaşla bu tokenlarla neye yetkisi varsa onu yapabilecek duruma gelecek  
