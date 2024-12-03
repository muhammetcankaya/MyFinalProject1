using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    //buradaki generic yapıda bana bir tane tablo ver 
    //bir tanede veritabanı ver bak ne genel ve soyut
    // bundan sonra bir tabloya ekleme silme işlemleri için kod yazamaıza gerek kalmayacak
    // Bu yapıyla yeni bir tablo geldiğinde onları oluşturup buradan referans alması gerektiğini söylememiz yetecektir

    public class EfEntityRepositoryBase<TEntity,TContext>:IEntityRepository<TEntity>
        // soyut yapımızda kullanıcıyı zorunlu kıldık 
        // Bir tablo clası kullanmalı ve bir veri  tabanı contexti kullanmalı dedik
        where TEntity : class ,IEntity, new()
        where TContext : DbContext,new()
    {
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                // Buradaki mevzuyu anlayalım
                // bir tane NorthwindContext nesnesi oluşturdum using içinde 
                // yapmamın sebebi performans amaçlı
                // bu nesneyi var değişkeninde entry yaptı yani bu verş tabanına gir demek
                // sonra bu değişken üzerinden ekleme işlemi yaptım sonrada değişiklikleri kaydettim
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                // önce db setlerinde productta bağlanıyorum
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                // filtre null mı öyleyse tüm tabloyu getirs liste yap
                //  değilse filtreyi where kriterine 
                return filter == null ?
                    context.Set<TEntity>().ToList() :
                    context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
