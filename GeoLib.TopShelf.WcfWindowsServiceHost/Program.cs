using Topshelf;

namespace GeoLib.TopShelf.WcfWindowsServiceHost
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {
        public static void Main()
        {
            HostFactory.Run(serviceConfiguration =>
            {
                serviceConfiguration.UseNLog();

                serviceConfiguration.Service<WcfWindowsServiceHost>(serviceInstance =>
                {
                    serviceInstance.ConstructUsing(() => new WcfWindowsServiceHost());

                    serviceInstance.WhenStarted(e => e.Start());
                    serviceInstance.WhenStopped(e => e.Stop());
                });

                serviceConfiguration.SetServiceName("GeoLibWcfWindowsServiceHost");
                serviceConfiguration.SetDisplayName("GeoLib WCF Windows Service Host");
                serviceConfiguration.SetDescription("GeoLib WCF Windows Service Host");

                serviceConfiguration.StartAutomatically();
            });
        }
    }
}
