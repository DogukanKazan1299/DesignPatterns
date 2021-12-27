using System;
using System.Collections.Generic;

namespace Observer
{
    class Program
    {
        //OBSERVER : Kendisine abone olan sistemlerin içerisinde bir işlem olduğunda devreye girmesini sağlar.
        //Örneğin bir alışveriş sitesi olduğunu hayal edelim.
        //Bu sitede belirlenen bir ürün olsun.
        //Bu ürün indirime girdiğinde bana haber gelsin istersek sistemde Observer kullanılmalıdır.(STEAM)
        static void Main(string[] args)
        {
            //1.müşterimiz için;
            var customer1 = new CustomerObserver();

            SteamManager steamManager = new SteamManager();

            //customer 1 i sisteme abone ettik.
            steamManager.Attach(customer1);
            //customer 1 i sistemden çıkardık.
            steamManager.Detach(customer1);



            steamManager.NewPrice();

        }
    }
    //Observer yapımız ve içerisinde oyunun indirime girdiğini belirttiğimiz metod olsun.
    public abstract class Observer
    {
        public abstract void NewPrice();
    }

    public class SteamManager
    {
        //1-abone olanlar listede tutulmalı
        List<Observer> _observer = new List<Observer>();

        public void NewPrice()
        {
            Console.WriteLine("Game price updated..");
            Notify();//haberi aboneye iletmeli
        }
        //2-yeni abone ekleme
        public void Attach(Observer observer)
        {
            _observer.Add(observer);
        }
        //3-abonelik iptal işlemi;
        public void Detach(Observer observer)
        {
            _observer.Remove(observer);
        }
        //4-Notify işlemi; Kişiye haber ulaşmalı
        private void Notify()
        {
            foreach (var observer in _observer)
            {
                observer.NewPrice();
            }
        }
    }

    //Müşteriye Observer ile haber dönmeliyiz.
    public class CustomerObserver : Observer
    {
        public override void NewPrice()
        {
            Console.WriteLine("Sayın müşterimize : Listenizdeki oyun indirime girmiştir");
        }
    }


}
