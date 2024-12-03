using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
// adı üstünde Dependency resolvers bağımlılık çözümleyici 
// yani bak Iproduct service producttır bunu demek önemli çünkü soyut bir sistem kullanıyorsun 
// Bizim web apı içinde yazdığımız oluşturduğumuz ıos i conteynırlar yerine
//Autofac kullanmak için bu kodların yazdık
// hangi interface kimdir işini burda yapmamızı sağlıyor program cs de yapmıştık
// bunu burda yapmanın faydası yarın başka bir apiye geçersek tekrar yapmamak 
// burda yaparsak backhend tarafında yapmış olacağız
// Autofaci import etmek lazım nuget dan yüklememiz lazım yani
// Aop mimarisi clasın başında veya sonunda yazdığımız kod bloğu kabaca
// bu yapı java defult olarak geliyor 
// Autofac kulanarak interfacelerin karşılığı nedir bunları tutcaz
// burada dependecyresılver içinde otofac açamamızın sebebi yarın başka bir tekloji kullanabiliriz diye

using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAcces.Concrete.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule :Module
    {
        //bu yapıyı başka formatlar ile yapıcaz
        // Autofac, Ninject, CastleWindsor,StructureMap --> Ioc Container
        // bu yapılar bize IoC altyapısı kuruyor
        // AOP chate sor 
        // benim anladığım classın başına metodlar yazıcaz ve o metodu da içinde barındırıcak
        // yani ekleme işlemi 5saniyeden uzun sürüyormu [saniye] yazınca sürüyorsa bize haber vericek 
        // biraz daha araştırıcağız

        //burada yaptığımız şey eğer bir ıproducatservice görürsen onun karşılığı produdactmanager dir diyor
        //içerisinde data tutmuyorsak singleton kullanıcaz
        //builder.Services.AddSingleton<IProductService, ProductManager>();
        //    //ben buna bu dedim ama IProductDal nedir dedi bana sistem onuda tanımlamam gerekecek
        //    builder.Services.AddSingleton<IProductDal, EfProductDal>();
            // bu yaptığımız işlem bizim için newliyor

        //yukarıda yaptığımz mevzuyu Outofac sayesinde burada yapıcaz
        // aotufac bize AOP desteği veriyor


        // bu Load sayedsinde Uygulama Çaıştığında burada çalışacak 
        protected override void Load(ContainerBuilder builder)
        {

            // İNTERFACENİN KARŞILIĞI BUDUR BÖYLE YAPILIR
            //IproductService bir ProductManagerdir diğeride aynen öyle 
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();


            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            // Bu alttakinide not al
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
