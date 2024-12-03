using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    // DataResult bir Result dır ve interfaceyi IDataResult dır
    // Ek olarak Bu sınıf Resultın yaptığını yapmakla beraber birde 
    // Data döndürür
    public class DataResult<T> : Result, IDataResult<T>
    {
        //Buradaki base sınıfı succes ve massage parametlerini Resulta gönderir
        // Bu işi sen yap der bende data işini yapıcam der
        // yani sonuç olarak üç değişken döndirmiş olacak
        public DataResult(T data,bool success,string massage):base(success,massage)
        {
            Data = data;

        }
        // hatırlarsan Result da sadece mesaj göndermek istiyorsak demiltik
        // buda Sadece success gönderiyor oraya ordaki yapıda direkt sadece
        // succes yapısını göndereni çalıştıracaktır
        public DataResult(T data,bool success):base(success)
        {
            Data = data;
        }
        public T Data { get;}
    }
}
