using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using GeoLib.Contracts;
using GeoLib.Proxy;

namespace GeoLib.Client
{
    public enum WcfBindingType
    {
        NetTcpBinding,
        BasicHttpBinding,
        WsHttpBinding
    }

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            string currentProcessId = Process.GetCurrentProcess().Id.ToString();

            Title = $"GeoLib WCF Client App: Thread ID = {managedThreadId}, Process ID = {currentProcessId}";
        }

        private WcfBindingType _wcfBindingType = WcfBindingType.NetTcpBinding;

        private void GetZipCodeInfoButton_Click(object sender, RoutedEventArgs e)
        {
            // todo: fails to clear labels
            //CityOutputLabel.Content = "";
            //StateOutputLabel.Content = "";

            // ReSharper disable once InvertIf
            if (ZipCodeTextBox.Text != "")
            {
                GeoClient geoClient = GetGeoClient(_wcfBindingType);
                ZipCodeData zipCodeData = geoClient.GetZipCodeInfo(ZipCodeTextBox.Text);

                if (zipCodeData != null)
                {
                    CityOutputLabel.Content = zipCodeData.City;
                    StateOutputLabel.Content = zipCodeData.State;
                }

                geoClient.Close();
            }
        }

        private void GetZipCodesButton_Click(object sender, RoutedEventArgs e)
        {
            // todo: Fails to clear ListBox
            //ZipCodesListBox.ItemsSource = "";

            // ReSharper disable once InvertIf
            if (StateTextBox.Text != "")
            {
                // Alternate constructor
                //EndpointAddress endpointAddress = new EndpointAddress("net.tcp://localhost:8009/GeoService");
                //Binding binding = new NetTcpBinding();
                //GeoClient geoClient = new GeoClient(binding, endpointAddress);

                GeoClient geoClient = GetGeoClient(_wcfBindingType);
                IEnumerable<ZipCodeData> zipCodeDataEnumerable = geoClient.GetZipCodes(StateTextBox.Text);

                if (zipCodeDataEnumerable != null)
                {
                    ZipCodesListBox.ItemsSource = zipCodeDataEnumerable;
                }
                
                geoClient.Close();
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // todo: clean up this code
            if (e.OriginalSource is RadioButton radioButton)
            {
                switch ((string)radioButton.Content)
                {
                    case "netTcpBinding":
                        _wcfBindingType = WcfBindingType.NetTcpBinding;
                        break;

                    case "basicHttpBinding":
                        _wcfBindingType = WcfBindingType.BasicHttpBinding;
                        break;

                    case "wsHttpBinding":
                        _wcfBindingType = WcfBindingType.WsHttpBinding;
                        break;
                }
            }
        }

        private GeoClient GetGeoClient(WcfBindingType wcfBindingType)
        {
            GeoClient geoClient = null;

            switch (wcfBindingType)
            {
                case WcfBindingType.NetTcpBinding:
                    const string netTcpEndpoint = "netTcpEndpoint";
                    geoClient = new GeoClient(netTcpEndpoint);
                    Debug.WriteLine("binding = netTcpEndpoint");
                    break;

                case WcfBindingType.BasicHttpBinding:
                    const string basicHttpEndpoint = "basicHttpEndpoint";
                    geoClient = new GeoClient(basicHttpEndpoint);
                    Debug.WriteLine("binding = basicHttpEndpoint");
                    break;

                case WcfBindingType.WsHttpBinding:
                    const string wsHttpEndpoint = "wsHttpEndpoint";
                    geoClient = new GeoClient(wsHttpEndpoint);
                    Debug.WriteLine("binding = wsHttpEndpoint");
                    break;

                default:
                    Debug.WriteLine("Should not get here");
                    break;

            }

            //if (isNetTcpBinding)
            //{
            //    const string netTcpEndpoint = "netTcpEndpoint";
            //    geoClient = new GeoClient(netTcpEndpoint);
            //    //Debug.WriteLine("binding = netTcpEndpoint");
            //}
            //else
            //{
            //    const string httpEndpoint = "basicHttpEndpoint";
            //    geoClient = new GeoClient(httpEndpoint);
            //    //Debug.WriteLine("binding = httpEndpoint");
            //}

            return geoClient;
        }
    }
}
