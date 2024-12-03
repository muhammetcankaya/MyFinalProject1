using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Utilities.Results
{
    //Zaten result yapımız sabit
    //mesaj ve success değişkenlerini içermek zorunda
    //IDataResult ek olarak Data iiçericek 
    // Neden??
    // Bu class add gibi işlemlerin aksine getall getbyıd gibi
    // işlemler içindir bunlar ekrana bir data döndürmek zorunda
    // e biz zaten datayı döndüreni yazdık ama succes işlemi 
    // ve mesaj gönderme işlemide yapmak istiyoruz o yüzden 
    // bu sınıfı tanımlıyoruz 
    // bu sınıfı o clasın bir metodu olarak yazdığımızda 
    // 3 değişken birlikte gönderebileceğiz 
    // haydı gelin DataResultda kodları yazalım
    public interface IDataResult<T>:IResult
    {

        T Data { get; }

    }
}
