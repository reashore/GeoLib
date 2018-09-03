using System;
using System.ServiceModel;
using GeoLib.Services;
using Topshelf.Logging;

namespace GeoLib.TopShelf.WcfWindowsServiceHost
{
    public class WcfWindowsServiceHost
    {
        private ServiceHost _geoManagerHost;
        private static readonly LogWriter LogWriter = HostLogger.Get<WcfWindowsServiceHost>();

        public bool Start()
        {
            try
            {
                _geoManagerHost = new ServiceHost(typeof(GeoService));
                _geoManagerHost.Open();

            }
            catch (Exception exception)
            {
                LogWriter.InfoFormat($"{exception.Message}");
            }

            return true;
        }

        public bool Stop()
        {
            _geoManagerHost.Close();
            return true;
        }
    }
}
