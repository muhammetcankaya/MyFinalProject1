using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class ErrorResult:Result
    {
        // Burada da add,delete gibi işlemler için error hata verimesi için
        // Error classını göndermeyi tercih edicez
        // bu kapsüllü yapıyı kullanarak if lerle uğraşmayacağız asla

        public ErrorResult(string message) : base(false, message)
        {

        }
        public ErrorResult() : base(false)
        {

        }
    }
}
