using System.ServiceModel;
using System.Windows;
using GeoLib.Services;

namespace GeoLib.WpfHost
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            EnableStartButton(true);

            //int threadId = Thread.CurrentThread.ManagedThreadId;
            //Title = $"Thread ID = {threadId}";
        }

        private ServiceHost _geoLibServiceHost;

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _geoLibServiceHost = new ServiceHost(typeof(GeoService));
            _geoLibServiceHost.Open();

            EnableStartButton(false);
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _geoLibServiceHost.Close();

            EnableStartButton(true);
        }

        private void EnableStartButton(bool enable)
        {
            StartButton.IsEnabled = enable;
            StopButton.IsEnabled = !enable;
        }
    }
}
