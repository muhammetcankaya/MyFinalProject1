//Bu kýsma not alýcaz
//.net Asp.net in fremme worku
// asp.net .net projelerindeki web projelerine verilen isim
// API ,Razor Pages content, MVC(MODEL VÝEW CONTROLLER)
// API BÝZÝM bacnkhande yazdýðýmýzý angular react gibi farklý arayuz uygulamalarýna
//göndermemizi saðlan bu mobil uyguulamada olabilir yani yazdýðýmýz backhang projesi
// evrensel oluyor çünkü biz bunu restful formatýnda bir json olarak görmelerini saðlayabilriz
// bu sebeple apý daha iyidir
// diðerleri backhand yokmuþ gibi direkt yazýlýr her sisteme entegre edilemez





using Autofac;
using Autofac.Extensions.DependencyInjection;// bunun için AutofacServiceProviderFactory()
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Core.Extensions;
using DataAcces.Concrete.EntityFramework;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.DependencyResolvers;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // -----------------------
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());//otofac ekleme ýnstall package autofacextensionsdependencyinjection
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                // buradada bizim modelimizi söyledik 
                builder.RegisterModule(new AutofacBusinessModule());
            });// -----------------
            // Burada bak biz senin IOC konteyýrý deðilde (önce busines )outofac kollanacaðýz diyoruz
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // TokenOptions'ý appsettings.json'dan al
            var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            
            // Authentication ve JWT yapýlandýrmasýný ekle
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });
            builder.Services.AddDependencyResolvers(new ICoreModule[] 
            { new CoreModule()
            });
            //bu yapýyý baþka formatlar ile yapýcaz
            // Autofac, Ninject, CastleWindsor,StructureMap --> Ioc Container
            // bu yapýlar bize IoC altyapýsý kuruyor
            // AOP chate sor 
            // benim anladýðým classýn baþýna metodlar yazýcaz ve o metodu da içinde barýndýrýcak
            // yani ekleme iþlemi 5saniyeden uzun sürüyormu [saniye] yazýnca sürüyorsa bize haber vericek 
            // biraz daha araþtýrýcaðýz

            //burada yaptýðýmýz þey eðer bir ýproducatservice görürsen onun karþýlýðý produdactmanager dir diyor
            //içerisinde data tutmuyorsak singleton kullanýcaz
            //builder.Services.AddSingleton<IProductService, ProductManager>();
            //ben buna bu dedim ama IProductDal nedir dedi bana sistem onuda tanýmlamam gerekecek
            //builder.Services.AddSingleton<IProductDal,EfProductDal>();
            // bu yaptýðýmýz iþlem bizim için newliyor





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}
