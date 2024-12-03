using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
//Şimdi bu core katmanını oluşturma sebebimiz 
// aslında bizim her projede bu ekle sil getall gibi komutları kullanmamız
// madem soyutlama yapıyoruz neden her proje için tek tek soyutlama yapalım
// bunlar standart şeyler bir core katmanıyla soyut nesneleri soyut nesnelerin bağlandığı soyutları 
// buranın içinde tutmayalım kurumsal bir mimaride bu iş böyle işler

namespace Core.DataAccess
{
    // T yi sınırlandırmak istiyorum herkes istediğini  yazamasın
    // generic constraint generic kısıt
    // new() lenebilir olmalı
    // burada dedikki T class olmalı IEntity olmalı ve newlenebilir olmalı dedik IENTİTY NİN KENDİSİ OLAMAZ YANİ
    public interface IEntityRepository<T> where T : class, IEntity, new()// önemli bir önlem
    {
        //Burada generict yani T KISMINI kullanmamızın sebibi
        // category product customer vb. gibi bir çok şey için 
        // aynı ayrnı interface oluşturdukdan sonra 
        // bunların içini doldurmak yerine T yani değişken 
        // şekilde olan bir generics yapı tutmak katman oluşturmak demektir
        // getall içine yazdığımızda bir filtredir bu filtre sayesinde
        // bütün veriyi getirmek yerine filtre uygulayacağız
        // her katmanda tek tek bu filtreleri tutmamıza gerek kalmaz
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        // ekleme silme güncelleme metodlarını yükledik
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        // buna gerek kalmadı
        //List<T> GetAllByCategoryId(int categoryId);
    }
}
