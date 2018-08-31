using System.ServiceModel;
using System.ServiceProcess;
using GeoLib.Services;

namespace GeoLib.WinServiceHost
{
    public partial class GeoLibWindowsServiceHost : ServiceBase
    {
        public GeoLibWindowsServiceHost()
        {
            InitializeComponent();
        }

        private ServiceHost _geoManagerHost;

        protected override void OnStart(string[] args)
        {
            _geoManagerHost = new ServiceHost(typeof(GeoManager));
            _geoManagerHost.Open();
        }

        protected override void OnStop()
        {
            _geoManagerHost?.Close();
        }
    }
}
