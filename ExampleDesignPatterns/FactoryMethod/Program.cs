using System;

namespace FactoryMethod
{
    //Factory Method Design Pattern ; Amaç yazılımda değişimi kontrol altına almaktır.
    //Backend projelerinde özellikle DataAccess katmanında bulunan Log-ORM-Cache gibi sistemlerde kullanılır.
    class Program
    {
        static void Main(string[] args)
        {
            //Database logged.
            CustomerManager customerManager = new CustomerManager(new DatabaseFactory());
            customerManager.Get();

            //Sms logged
            CustomerManager customerManager1 = new CustomerManager(new SmsFactory());
            customerManager1.Get();

            //Çıkış logged
            CustomerManager customerManager2 = new CustomerManager(new ExistFactory());
            customerManager2.Get();

            //Login logged
            CustomerManager customerManager3 = new CustomerManager(new LoginFactory());
            customerManager3.Get();
        }
    }

    public interface ILogger
    {
        public void Log();
    }
    public interface ILoggerFactory
    {
        //Yeni log oluşturulurken ILogger dönsün.
        //Loglamaya göre gelsin.
        ILogger CreateLogger();
    }

    //Fabrikalar;
    //database fabrikası.
    public class DatabaseFactory : ILoggerFactory
    {
        public ILogger CreateLogger()
        {
            return new DatabaseLogger();
        }
    }
    //sms fabrikası
    public class SmsFactory : ILoggerFactory
    {
        public ILogger CreateLogger()
        {
            return new SMSLogger();
        }
    }
    //login fabrikası
    public class LoginFactory : ILoggerFactory
    {
        public ILogger CreateLogger()
        {
            return new LoginLogger();
        }
    }
    //exit fabrikası
    public class ExistFactory : ILoggerFactory
    {
        public ILogger CreateLogger()
        {
            return new ExistLogger();
        }
    }

    //1-Database için loglama
    public class DatabaseLogger : ILogger
    {
       

        public void Log()
        {
            Console.WriteLine("Database Logged");
        }
    }
    //2-Login işlemi için Loglama
    public class LoginLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Login Logged");
        }
    }
    //3-Sms işlemi için Loglama
    public class SMSLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Sms Logged");
        }
    }
    //4-Çıkış işlemi için Loglama.
    public class ExistLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Exist Logged.");
        }
    }

    //Müşteri işlemleri için yönetim classı
    public class CustomerManager
    {
        //istenen fabrikaya göre çalış + Dependency Injection Design Pattern
        ILoggerFactory _loggerFactory;
        public CustomerManager(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public void Get()
        {
            Console.WriteLine("Logged by ....");
            //kullanıcının yaptığı işleme göre loglama atacağız.
            ILogger logger = _loggerFactory.CreateLogger();
            logger.Log();
        }
    }


}
