using GeoLib.Contracts;
using GeoLib.Services;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace GeoLib.ConsoleHost
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {
        public static void Main()
        {
            ServiceHost geoManagerHost = null;

            try
            {
                geoManagerHost = new ServiceHost(typeof(GeoManager));
                //ConfigureEndpoint(geoManagerHost);
                geoManagerHost.Open();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            Console.WriteLine("Service started. Press any key to exit.");
            Console.ReadKey();

            geoManagerHost?.Close();
        }

        //private static void ConfigureEndpoint(ServiceHost serviceHost)
        //{
        //    const string address = "net.tcp://localhost:8009/GeoService";
        //    Binding binding = new NetTcpBinding();
        //    Type contract = typeof(IGeoService);

        //    serviceHost.AddServiceEndpoint(contract, binding, address);
        //}
    }
}
