using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
// MECBUREN BUNU BİR KERE DAHA İZLEYİP NOT ALMALISIN HANGİ KOD NE İE YARA DİYE
namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        public static void CreatePasswordHash // OUT NEYDİ ???
            (string password,out byte[] passwordHash,out byte[] passwordSalt)
             //Amaç: Gelen bir şifreyi güvenli bir şekilde hash'ler ve bir salt (anahtar) oluşturur.
            //Parametreler:
            //string password: Kullanıcının şifresi.
            //out byte[] passwordHash: Hash'lenmiş şifre, referans olarak döndürülür.
            //out byte[] passwordSalt: Kullanılan salt (anahtar), referans olarak döndürülür.
            //out: Bir metot çağrısı sırasında birden fazla değer döndürmek için kullanılır. Değer, çağıran metoda geri aktarılır.
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) // ŞİFRELEME ALGORİTMASI ÇAĞIRDIK
            {
                passwordSalt = hmac.Key; // ANAHTAR ATADIK  hmac.Key: Rastgele bir "salt" (anahtar) oluşturur. Bu, aynı şifre için farklı hash'lerin oluşmasını sağlar.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); //(Encoding.UTF8.GetBytes(password) BYTE TİPİNE ÇEVİRDİ STRİNGİ
                //ComputeHash: Şifreyi UTF8 formatında byte dizisine çevirir ve bu dizi üzerinden hash hesaplar.
                //Encoding.UTF8.GetBytes: Şifreyi byte dizisine dönüştürür(hash'leme işlemi için gereklidir).


                    //HMACSHA512: Güvenli bir hashing algoritması kullanılır.HMAC(Hash -
                    //Based Message Authentication Code), verilerin bütünlüğünü ve güvenliğini
                    //sağlamak için kullanılır.
                    //using: HMACSHA512 nesnesi otomatik olarak bellekten temizlenir(Dispose) ve
                    //performansı artırır.
            }
        }
        //yukarıda hashı oluşturup veri tabanına kayıt ettik 
        //aşağıda kayıt ettiğimiz hashı key sayesinde kullanıcı şifre girince kontrol ettiğimiz kısım
        public static bool VerifyPasswordHash(string password, byte[] passwordHash,byte[] passwordSalt)// password hashi dooğrula 
        //Amaç: Kullanıcıdan gelen şifreyi mevcut hash ile karşılaştırır.
        //Parametreler:
        //string password: Kullanıcının girdiği şifre.
        //byte[] passwordHash: Veritabanında saklanan hash.
        //byte[] passwordSalt: Veritabanında saklanan salt(anahtar



        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) 
            {
                //Salt Kullanımı: Hash hesaplama işlemi için saklanan passwordSalt anahtarı kullanılır. Bu, aynı algoritmanın kullanılmasını sağlar.


                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//Hash Hesaplama: Kullanıcının girdiği şifre ile aynı salt kullanılarak bir hash hesaplanır.
                for (int i = 0; i < computedHash.Length; i++) 
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                //Karşılaştırma: Hesaplanan hash ile veritabanında saklanan hash bayt bayt karşılaştırılır.
                //Mantık: Tek bir eşleşmeme bile olursa false döner(doğrulama başarısız).
                }
                return true;
            }  
        }
    }
}
