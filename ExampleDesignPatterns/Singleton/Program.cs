using System;

namespace Singleton
{
    class Program
    {
        //Singleton : Bir nesne örneğinden sadece bir kere üretilip her zaman kullanılmasını sağlar.
        //Hedef nesnenin state ini kontrol altında tutmaktır.
        //Nesne örneği katmanlar arasında geçerken ve sadece işlem yapıyorsa fonksiyon kullanırız.
        //Singleton ile nesne ürettiğimizde bellekte her zaman sabit kalır.

        static void Main(string[] args)
        {
            //lock objesi kullanılmadı
            var productManager = ProductManager.CreateAsSingleton();
            productManager.Get();

            //çakışma durumu olmaması için gerekli kullanım.(lock objesi ile)
            var categoryManager = CategoryManager.CreateAsSingleton();
            categoryManager.Get();

        }

    }
    public class ProductManager
    {
        //Yöneteceğimiz nesne
        //1-static olmalı
        private static ProductManager _productManager;
        public ProductManager()
        {
            //2-Dış erişime kapalı olmalı
        }
        //nesne var mı ? Kontrol yapılmalıdır.
        public static ProductManager CreateAsSingleton()
        {
            //nesne yoksa newle , nesne varsa return et.
            return _productManager ?? (_productManager = new ProductManager());
        }
        public void Get()
        {
            Console.WriteLine("Get Object");
        }
    }

    //NOT:Ayrıca bir nesneyi aynı anda iki kullanıcı isterse ve o nesne henüz üretilmediyse(biri üretme aşamasına geçerse)
    //o nesneden iki tane olabilir.

    //Bunun önüne geçebilmeliyiz.(lock)


    public class CategoryManager
    {
        //static
        private static CategoryManager _categoryManager;
        //lock 
        static object _lockObject = new object();
        public CategoryManager()
        {
            //dışa erişim kapalı
        }
        public static CategoryManager CreateAsSingleton()
        {
            //lock objesi kullanımı
            lock (_lockObject)
            {
                //categoryManager nesnesi yoksa yeni oluşturacak.
                if (_categoryManager == null)
                {
                    _categoryManager = new CategoryManager();
                }
            }
            //varsa bakmadan olan nesneyi dönecek.
            return _categoryManager;
        }
        public void Get()
        {
            Console.WriteLine("Get Object");
        }
    }



















}
