using Business.Abstract;
using Business.Concrete;
using DataAcces.Concrete.EntityFramework;
using Entities.Absrract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    // api/controllerinizin ismi yani products
    [ApiController]
    //Loosely coupled
    // attribute bir clas ile ilgili birgi verme onu imzalama yöntemizidir
    // IoC Container inversionof control değişimin kontrolu
    // bellekteki yer product manager efPD MANAGER GİBİ REFERANSLARI VERİRİM KİM İSTİYORSA ALIR 
    // JAVASCRİPT GİBİ DİLLERDE ILAŞABİLİRİZ CTOR İÇİNE
    // ıproductservice mi dur iosi containera bakim neymiş o diye bakar ve onu  kullanır
    // webapı da starupa gidiyoruz
    // bu şekilde hata alıyoruz çünkğ 
    // constraltırın içindeki gözükmüyor
    // 200 ok hata verimedi 
    // http statu codu 200 201 400 gibi http requestlerimiz olacak
    //mesala yetki yoksa 400 gözükecek status
    // getle alırken postla göndericez
    public class ProductsController : ControllerBase
    {
        IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("getall")]
        public IActionResult  Get()
        {
            // http statu cod onlara ne anlatmak istediğini açıklamak için çok önemlidir
            var result = _productService.GetAll();
            if (result.Success)
            {
                // 200 döndürcek işlem başarılı 
                // bunun içine data atabilriz
                return Ok(result);
            }
            // sistem bakımda mesajını verir
            return BadRequest(result);
        }
        //Şimdide post yapalım
        [HttpGet("getbyid")]
        public IActionResult GetById(int ıd)
        {
            var result = _productService.GetById(ıd);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Post(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
