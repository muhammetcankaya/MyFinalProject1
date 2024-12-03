//Bu k�sma not al�caz
//.net Asp.net in fremme worku
// asp.net .net projelerindeki web projelerine verilen isim
// API ,Razor Pages content, MVC(MODEL V�EW CONTROLLER)
// API B�Z�M bacnkhande yazd���m�z� angular react gibi farkl� arayuz uygulamalar�na
//g�ndermemizi sa�lan bu mobil uyguulamada olabilir yani yazd���m�z backhang projesi
// evrensel oluyor ��nk� biz bunu restful format�nda bir json olarak g�rmelerini sa�layabilriz
// bu sebeple ap� daha iyidir
// di�erleri backhand yokmu� gibi direkt yaz�l�r her sisteme entegre edilemez





using Autofac;
using Autofac.Extensions.DependencyInjection;// bunun i�in AutofacServiceProviderFactory()
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
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());//otofac ekleme �nstall package autofacextensionsdependencyinjection
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
            {
                // buradada bizim modelimizi s�yledik 
                builder.RegisterModule(new AutofacBusinessModule());
            });// -----------------
            // Burada bak biz senin IOC kontey�r� de�ilde (�nce busines )outofac kollanaca��z diyoruz
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // TokenOptions'� appsettings.json'dan al
            var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            
            // Authentication ve JWT yap�land�rmas�n� ekle
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
            //bu yap�y� ba�ka formatlar ile yap�caz
            // Autofac, Ninject, CastleWindsor,StructureMap --> Ioc Container
            // bu yap�lar bize IoC altyap�s� kuruyor
            // AOP chate sor 
            // benim anlad���m class�n ba��na metodlar yaz�caz ve o metodu da i�inde bar�nd�r�cak
            // yani ekleme i�lemi 5saniyeden uzun s�r�yormu [saniye] yaz�nca s�r�yorsa bize haber vericek 
            // biraz daha ara�t�r�ca��z

            //burada yapt���m�z �ey e�er bir �producatservice g�r�rsen onun kar��l��� produdactmanager dir diyor
            //i�erisinde data tutmuyorsak singleton kullan�caz
            //builder.Services.AddSingleton<IProductService, ProductManager>();
            //ben buna bu dedim ama IProductDal nedir dedi bana sistem onuda tan�mlamam gerekecek
            //builder.Services.AddSingleton<IProductDal,EfProductDal>();
            // bu yapt���m�z i�lem bizim i�in newliyor





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
