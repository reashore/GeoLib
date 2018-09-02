using System.ServiceModel;
using System.Threading;
using System.Windows;
using GeoLib.Services;

namespace GeoLib.WpfHost
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;

            int threadId = Thread.CurrentThread.ManagedThreadId;
            Title = $"Thread ID = {threadId}";
        }

        private ServiceHost _geoLibServiceHost;

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _geoLibServiceHost = new ServiceHost(typeof(GeoManager));
            _geoLibServiceHost.Open();

            StartButton.IsEnabled = false;
            StopButton.IsEnabled = true;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _geoLibServiceHost.Close();

            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
        }
    }
}
