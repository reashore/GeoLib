using System.Diagnostics;
using System.ServiceModel;
using System.Threading;
using System.Windows;
using GeoLib.Services;
using GeoLib.WpfHost.Services;

namespace GeoLib.WpfHost
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;

            MainUi = this;

            int threadId = Thread.CurrentThread.ManagedThreadId;
            Title = $"Thread ID = {threadId}";
        }

        private ServiceHost _geoManagerHost;
        private ServiceHost _messageManagerHost;

        public static MainWindow MainUi { get; set; }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _geoManagerHost = new ServiceHost(typeof(GeoManager));
            _messageManagerHost = new ServiceHost(typeof(MessageManager));

            _geoManagerHost.Open();
            _messageManagerHost.Open();

            StartButton.IsEnabled = false;
            StopButton.IsEnabled = true;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _geoManagerHost.Close();
            _messageManagerHost.Close();

            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
        }

        public void ShowMessage(string message)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            string processId = Process.GetCurrentProcess().Id.ToString();

            MessageLabel.Content = $"{message} : Thread ID = {threadId}, ProcessID = {processId}";
        }
    }
}
