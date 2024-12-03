using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Entities.Absrract;

namespace Business.Constants
{
    //static verince newlenmez
    // mesajları burda tutucaz işte
    public static class Massages
    {
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem Bakımda";
        public static string ProductListed = "Ürünler Listelendi";
        internal static string AuthorizationDenied = "Yetkiniz yok.";


    }
}
