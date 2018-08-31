using System.ServiceProcess;

namespace GeoLib.WinServiceHost
{
    public static class Program
    {
        public static void Main()
        {
            ServiceBase[] servicesToRun = 
            {
                new GeoLibWindowsServiceHost()
            };

            ServiceBase.Run(servicesToRun);
        }
    }
}
