using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Cross Cutting Concerns= LOG,CACHE,TRANSACTİON,AUTHORİZATİON Bunları araştır bunlar her katmanda ortak veya birer olarak kullanılan şeyler şeyler dedim çünkü araştırmam gerekiyor validationda crosscuttingcorcerndir
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Cashing;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Absrract;
using Entities.DTOs;
using FluentValidation;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        //constractır bunu öğrencem
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        //validation ilgili nesnenin yapısnı ilgilendiren şeyler validation
        // bir kişinin krediye uygun olup olmadığı kodları ise businessda yapılır
        // kredi isteyen biri için adamın adının en az 2 harften oluşmasını istiyorsan
        // bunu validationda yaparsın ama kredi verirken uygunmu diye bakacaksan onu businessta yapıcaksın
        // nesnein yapısıyla alakalı olan şeyler validation


        //[SecuredOperation("product.add,admin")]//yetki bu yapıları claim diyoruz
        [CacheRemoveAspect("IProductService.Get")]
        [ValidationAspect(typeof(ProductValidator))]//Attribute aslında bu var diye aşağıdaki method çalışmadan önce yukarı bakıyor ettribute varmı varsa önce onu çalıştır sonra gel içeriyi çalıştır diyor
        public IResult Add(Product product)
        {
            // OAP Yaptığımız için bunun yerine [ValidationAspect(typeof(ProductValidator))]
            // bunu kullanarak yaptık bunu yapmak için altyapıyıda core kkatmanına yerleştirdik
            // Bide bunu core yazmamızın sebebi bak burada producta bağlı her nesne için bu doğrulamayı yapamıza gerke varmı bir kere coreda yapalım bunu kullanalım core da
            // oluşturduğumuz validation kodları ile ilgili nesneyi kodlamanın yöntemi budur
            //var context = new ValidationContext<Product>(product);
            //ProductValidator validator = new ProductValidator();
            //var result = validator.Validate(context);
            //if (!result.IsValid) 
            //{
            //    throw new ValidationException(result.Errors);
            //}


            //bu koddan bir şey anlamadım
            // ValidationTool.Validate(new ProductValidator(),product);
            // mÜTHÜŞ OLDU YA     
            IResult result = BusinessRules.Run(ChenckIfProductNameExists(product.Productname),
                ChenckIfProductCountOfCategryCorrenct(product.CategoryId)
               );
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Massages.ProductAdded);
        }




        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Massages.MaintenanceTime);
            }
            //İş kodları varmış gibi
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Massages.ProductListed);
            

        }
        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {

            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId==id));
        }
        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult < List < Product >> (_productDal.GetAll(p=>p.UnitPrice>min && p.UnitPrice<max));
        }
        private IResult ChenckIfProductCountOfCategryCorrenct(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        private IResult ChenckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.Productname == productName).Any();// Any varmı yokmu true veya false döndürür
            if (result)
            {
                return new ErrorResult("Böyle bir ürün ismi zaten var");
            }
            return new SuccessResult();
        }

    }
}
