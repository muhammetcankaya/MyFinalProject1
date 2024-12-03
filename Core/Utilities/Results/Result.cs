using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        // Burada da bu işlemi yapmak için gerekli kodu yazıcaz
        // şimdi result değikenini aldık 
        // bunları tanımadık burada 
        // burada bazen mesaj göndermek istemeyebiliriz ama succes bilgisi 
        // her zaman geçerli olacaağı için 
        //this(success) ifadesi, aynı sınıfın diğer bir kurucusunu
        //(yani public Result(bool success)) çağırır. Bu,
        //"kurucuların zincirleme" (constructor chaining) olarak adlandırılır.
        // yapıyı kullanarak iki işlemide yapmaasını sağladık 
        // massagenin defult olarak tanımlayarak yapabilirdik 
        // ama bu yapı frontend için olumlu bir kullanım deği
        public Result(bool success,string massage):this(success)
        {
            Message = massage;
        }
        public Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; }

        public string Message { get; }
    }
}
