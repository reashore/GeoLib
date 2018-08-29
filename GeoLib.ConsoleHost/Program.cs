using GeoLib.Services;
using System;
using System.ServiceModel;

namespace GeoLib.ConsoleHost
{
    public class Program
    {
        public static void Main()
        {
            ServiceHost geoManagerHost = null;

            try
            {
                geoManagerHost = new ServiceHost(typeof(GeoManager));
                geoManagerHost.Open();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            Console.WriteLine("Done");
            Console.ReadKey();

            geoManagerHost?.Close();
        }
    }
}