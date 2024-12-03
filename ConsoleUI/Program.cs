using System;
using Business.Concrete;
using DataAcces.Concrete.EntityFramework;
using Entities.Absrract;
//BUNU UNUTMA BİZİM YAZDIĞIM BACK END PROJESİNİN 
// ANGULAR REACT IAS AND GİBİ ARAYÜZLERE ULAŞABİLMESİ 
// İÇİN WEB API MİZİN OLMASI GEREKİR 
//WEB API > RESTFUL(JSON FORMATI)
namespace ConsoleUI2
{
    internal class Program
    {
        // soyut classları Abstracta ıınterface base
        // somut nesneleri Concrete koyacağız
        static void Main(string[] args)
        {
            Console.WriteLine("dasdsadsasdadsa");
            //ProductTest();
            //CategoryTest();
            ProductManager productManager = new ProductManager(new EfProductDal());
            productManager.Add(new Product { CategoryId=3,Productname="Birşlessadasdasdadssdadasyler",UnitPrice=1426,UnitsInStock=5});

        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            var s =categoryManager.GetById(2000);
            Console.WriteLine(s.CategoryName);
            //foreach (var category in categoryManager.GetAll())
            //{
            //    Console.WriteLine(category.CategoryName);
            //}
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());
            var result = productManager.GetProductDetails();

            if (result.Success==true)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + " " + product.CategoryName);

                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }



        }
    }
}
