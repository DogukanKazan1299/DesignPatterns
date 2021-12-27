using System;

namespace AbstractFactory
{
    class Program
    {
        //Factory Method Design Pattern'e ek olarak toplu nesne kullanımı,ihtiyaç halinde nesne kullanımını 
        //kolaylaştırmak sağlar ayrıca standart nesnelere ihtiyaç duyuluyorsa mantık dahilinde nesne üretimimizi sağlar.
        static void Main(string[] args)
        {
            //1.fabrikayı kullan..
            ServerManager serverManager = new ServerManager(new LogCache1());
            serverManager.GetLogCache();


            //2.fabrikayı kullan.
            ServerManager serverManager1 = new ServerManager(new LogCache2());
            serverManager1.GetLogCache();

        }
    }

    //Loglama örneği; Loglama ve cache atma durumları..
    public abstract class Logging
    {
        public abstract void Log(string message);
    }

    //1-database loglama
    public class DatabaseLogger : Logging
    {
        public override void Log(string message)
        {
            Console.WriteLine("Database Logged");
        }
    }
    //2-Login loglama;
    public class LoginLogger : Logging
    {
        public override void Log(string message)
        {
            Console.WriteLine("Login Logged");
        }
    }

    //---Cachelemek ; Cache nedir ? (Önbelleğe alma) -> Daha önceden alınan verimli şekilde yeniden kullanılmasını sağlar. 
    public abstract class Caching
    {
        public abstract void Cache(string data);
    }
    //1-web cache
    public class WebCache : Caching
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Web caching");
        }
    }

    //2-Redis Cache;
    public class RedisCache : Caching
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Redis caching");
        }
    }


    //Fabrikamız olmalı.
    public abstract class CrossCuttingConcernsFactory
    {
        //Loglama için create
        public abstract Logging CreateLogger();
        //Cacheleme için create
        public abstract Caching CreateCaching();
    }


    //1.Fabrika -> database loglama + web cacheleme 
    public class LogCache1 : CrossCuttingConcernsFactory
    {
        public override Caching CreateCaching()
        {
            return new WebCache();
        }

        public override Logging CreateLogger()
        {
            return new DatabaseLogger();
        }
    }
    //2.fabrika-> Login loglama + Redis cache 
    public class LogCache2 : CrossCuttingConcernsFactory
    {
        public override Caching CreateCaching()
        {
            return new RedisCache();
        }

        public override Logging CreateLogger()
        {
            return new LoginLogger();
        }
    }

    //Server sistem yönetimimiz
    public class ServerManager
    {
        CrossCuttingConcernsFactory _crossCuttingConcernsFactory;
        Logging _logging;
        Caching _caching;

        public ServerManager(CrossCuttingConcernsFactory crossCuttingConcernsFactory)
        {
            _crossCuttingConcernsFactory = crossCuttingConcernsFactory;
            _logging = crossCuttingConcernsFactory.CreateLogger();
            _caching = crossCuttingConcernsFactory.CreateCaching();
        }

        public void GetLogCache()
        {
            _caching.Cache("Cache");
            _logging.Log("Logged");
            Console.WriteLine("Server..");
        }
    }


}
