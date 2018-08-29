using System.Diagnostics;
using System.Threading;
using System.Windows;
using GeoLib.Contracts;
using GeoLib.Proxy;

namespace GeoLib.Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            string currentProcessId = Process.GetCurrentProcess().Id.ToString();

            this.Title = $"threadId = {managedThreadId}, processId = {currentProcessId}";
        }

        private void GetZipCodeInfoButton_Click(object sender, RoutedEventArgs e)
        {
            // ReSharper disable once InvertIf
            if (ZipCodeTextBox.Text != "")
            {
                GeoClient geoClient = new GeoClient();
                ZipCodeData zipCodeData = geoClient.GetZipCodeInfo(ZipCodeTextBox.Text);

                if (zipCodeData != null)
                {
                    CityLabel.Content = zipCodeData.City;
                    StateLabel.Content = zipCodeData.State;
                }

                geoClient.Close();
            }
        }

        private void GetZipCodesButton_Click(object sender, RoutedEventArgs e)
        {
            if (StateTextBox.Text != "")
            {
                GeoClient geoClient = new GeoClient();

                geoClient.Close();
            }
        }

        private void MakeCallButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageTextBox.Text != "")
            {
                GeoClient geoClient = new GeoClient();

                geoClient.Close();
            }
        }
    }
}
