using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using FluentValidation;
//Cross Cutting Concerns= LOG,CACHE,TRANSACTİON,AUTHORİZATİON Bunları araştır bunlar her katmanda ortak veya birer olarak kullanılan şeyler
//şeyler dedim çünkü araştırmam gerekiyor ARAŞTIRRR
// validationda bunlardan biridir o yüzden buraya yazdık core içine 
namespace Core.CrossCuttingConcerns.Validation
{
    public class ValidationTool
    {
        // burdada yaptık birşeyler ama 
        public static void Validate(IValidator validator,object entity)
        {
            // IValidatör Validatorun interfacesi bunu araştırarak bulduk onu kullandık Validator yerine 
            // değişen şeyleri parametre olarak verdik
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
        
            throw new ValidationException(result.Errors);
        }
    

    }
}
