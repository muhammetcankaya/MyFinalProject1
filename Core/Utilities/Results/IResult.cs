using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public interface IResult
    {
        // Burada bizim business clasımızın tek değer döndürmesinden ötürü 
        // oluşturulmuştur yani
        // add,delete veya update gibi işlemlerde bunlar bir void olduğu için
        // tek bir iş yapacak ama bu front end için anlamsız bir şey 
        // biz napıcaz bu void metodu burdan miras alacak ve hem işlem yapıp hemde
        // başarılı ve başarılı mesajı değişknelerini frontende göndermemizi sağlayacak
        
        bool Success { get; }
        string Message { get; }
    }
}
