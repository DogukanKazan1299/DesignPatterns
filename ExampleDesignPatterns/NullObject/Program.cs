using System;

namespace NullObject
{
    class Program
    {
        //Null Object ->Bir projemiz olsun.Test edilirken içerisinde bulunan bazı metotların çalışmasını istemeyelim.
        //Bunun için null bir nesne yollayabiliriz ancak null reference exception alabiliriz.
        //Bunun yerine iş yapmayan bir sınıf kodlarız.
        static void Main(string[] args)
        {
            //1-database log
            CustomerManager customerManager = new CustomerManager(new DatabaseLogger());
            customerManager.Get();
            //2-Login log
            CustomerManager customerManager1 = new CustomerManager(new LoginLogger());
            customerManager1.Get();


            //3-StubLogger; NULL OBJECT ! 
            CustomerManager customerManager2 = new CustomerManager(StubLogger.GetLogger());
            customerManager2.Get();

        }
    }


    public interface ILogger
    {
        public void Log();
    }
    public class DatabaseLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Database Logged.");
        }
    }
    public class LoginLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Login Logged..");
        }
    }

    public class StubLogger : ILogger
    {
        private static StubLogger _stubLogger;
        private static object _lock = new object();
        private StubLogger() { }
        public static StubLogger GetLogger()
        {
            lock (_lock)
            {
                if (_stubLogger == null)
                {
                    _stubLogger = new StubLogger();
                }
            }
            return _stubLogger;
        }
        public void Log()
        {
            //iş yapma iş yapıyor gibi görün...
        }
    }

    public class CustomerManager
    {
        //gelen logger'a göre çalış .  + Dependency Injection
        ILogger _logger;
        public CustomerManager(ILogger logger)
        {
            _logger = logger;
        }
        public void Get()
        {
            Console.WriteLine("Get");
            _logger.Log();
        }
    }
    //test class
    public class CustomerManagerTest
    {
        public void SaveTest()
        {
            //iş yapmayan yapıyı Stublogger dön...
            CustomerManager customerManager = new CustomerManager(StubLogger.GetLogger());
            customerManager.Get();
        }
    }

}
