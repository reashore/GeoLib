using GeoLib.Services;
using System;
using System.ServiceModel;

namespace GeoLib.ConsoleHost
{
    public class Program
    {
        public static void Main()
        {
            ServiceHost geoManagerHost = new ServiceHost(typeof(GeoManager));
            geoManagerHost.Open();

            Console.WriteLine("Done");
            Console.ReadKey();

            geoManagerHost.Close();
        }
    }
}
