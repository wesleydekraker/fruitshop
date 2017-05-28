using FruitShopServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FruitShopHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(FruitShopService)))
            {
                host.Open();
                Console.WriteLine("FruitShopService is running...");
                Console.ReadKey();
            }
        }
    }
}
