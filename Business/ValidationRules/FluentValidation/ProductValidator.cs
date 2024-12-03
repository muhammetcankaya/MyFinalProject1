using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Absrract;
using FluentValidation;
// Validation Rules Doğrulama kuralları 
// FluentValidation  teklonojisini kullandığımız için bu klasörü açıp 
// buraya koyduk yarın daha iyi bir teknoloji gelirse onu KuLLANIRIZ
namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>//aBSTRACTVALİDATÖR fLUENT VALİDATİONDAN GELİYOR
    {
        // bu kurallar constractır içine yazılır
        public ProductValidator()
        {
            // Rulefor product için yani hangi nesneyi verirsen onun için
            //buraya validation kuralları yazılabilir
            RuleFor(p => p.Productname).NotEmpty();
            RuleFor(p => p.Productname).MinimumLength(2);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            //altta unitprice 10dan büyük eşit olacak when yani categoryıd 1 oldupunda
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);

            // BURAYA KENDİ METODUMUZU YAZDIK 
            // A ile başlamak zorunda dedik
            RuleFor(p => p.Productname).Must(StartwithA);

        }

        private bool StartwithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
