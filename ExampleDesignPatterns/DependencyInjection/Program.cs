using Ninject;
using System;

namespace DependencyInjection
{
    class Program
    {   
        //Dependency Injection : SOLID 'in D harfinin temelidir.(Dependency Inversion)
        //Sınıflar arası bağımlılık olabildiğince az olmalıdır.Üst seviye classlar alta bağlı olmamalıdır.
        //Özellikle katmanlar arası geçişlerde kullanılır.
        //Örneğin Business katmanında DataAccess kullanırken DataAccess'e bağlılığı en aza indirmeliyiz.

        static void Main(string[] args)
        {
            //1.kullanıcı entityframework kullanmış olabilir.
            ProductManager productManager = new ProductManager(new EfProductDal());
            productManager.Get();

            //2.kullanıcı nhibernate kullanmış olabilir.
            ProductManager productManager1 = new ProductManager(new NhiberNate());
            productManager1.Get();


            //IOC Container->Autofac-Ninject;
            //Ben senden IProductDal istediğimde sen bana EfProductDal ver dedik.Ninject kullanıldı.
            IKernel kernel = new StandardKernel();
            kernel.Bind<IProductDal>().To<EfProductDal>();
            ProductManager productManager2 = new ProductManager(kernel.Get<IProductDal>());
            productManager2.Get();




        }
    }

    //Ürünlerimiz olsun ve sisteme hangi framework ile girildiğini göstersin.
    public interface IProductDal
    {
        public void Get();
    }
    //Kullanıcı EntityFramework kullanmış olabilir.
    public class EfProductDal : IProductDal
    {
        public void Get()
        {
            Console.WriteLine("Using EntityFramework..");
        }
    }
    //Kullanıcı NHiberNate kullanmış olabilir.
    public class NhiberNate : IProductDal
    {
        public void Get()
        {
            Console.WriteLine("Using NHiberNate");
        }
    }

    public class ProductManager
    {
        //Kullanıcı hangi IProductDal'ı kullanırsa ona göre işlem yapalım.
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public void Get()
        {
            _productDal.Get();
        }
    }


}
