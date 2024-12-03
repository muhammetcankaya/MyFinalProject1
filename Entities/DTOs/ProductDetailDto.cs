using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace Entities.DTOs
{
    // bu DTO YAPISI 
    // PRODUCT DAKİ categoryID nin CATEGORY name olarak görmemizi sağlayan yapıdır
    // kısaca join yapılarını buraya yazarız yani 
    // adama category yerine category ıd mi gösterccez kankas
    public class ProductDetailDto:IDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public short UnitsInStock { get; set; } 

    }
}
