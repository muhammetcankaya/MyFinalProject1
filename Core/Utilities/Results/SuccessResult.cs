using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessResult:Result
    {
        // Burada da add,delete gibi işlemler için success hata verimesi için
        // Error classını göndermeyi tercih edicez
        // bu kapsüllü yapıyı kullanarak if lerle uğraşmayacağız asla
        public SuccessResult(string message):base(true,message)
        {

        }
        public SuccessResult():base(true)
        {

        }
    }
}
