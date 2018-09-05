using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
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
        }

        private void GetZipCodeInfoButton_Click(object sender, RoutedEventArgs e)
        {
            CityOutputTextBox.Text = "";
            StateOutputTextBox.Text = "";
            string zipCode = ZipCodeTextBox.Text;

            // ReSharper disable once InvertIf
            if (zipCode != "")
            {
                WcfBindingType wcfBindingType = GetBindingTypeFromRadioButtons();
                GeoClient geoClient = GetGeoClientWithBinding(wcfBindingType);
                ZipCodeData zipCodeData = null;

                try
                {
                    zipCodeData = geoClient.GetZipCodeInfo(zipCode);

                }
                catch (FaultException exception)
                {
                    string message = "Exception: \r\n" +
                                     $"Message = {exception.Message} \r\n" +
                                     $"Proxy state = {geoClient.State.ToString()}";

                    CityOutputTextBox.Text = "";
                    StateOutputTextBox.Text = "";
                    ErrorMessage1Label.Text = message;
                }

                if (zipCodeData != null)
                {
                    CityOutputTextBox.Text = zipCodeData.City;
                    StateOutputTextBox.Text = zipCodeData.State;
                }

                geoClient.Close();
            }
        }

        private void GetZipCodesButton_Click(object sender, RoutedEventArgs e)
        {
            // todo: Fails to clear ListBox
            ZipCodesListBox.ItemsSource = null;
            ZipCodesListBox.Items.Clear();
            string state = StateTextBox.Text;

            // ReSharper disable once InvertIf
            if (state != "")
            {
                // Alternate constructor
                //EndpointAddress endpointAddress = new EndpointAddress("net.tcp://localhost:8009/GeoService");
                //Binding binding = new NetTcpBinding();
                //GeoClient geoClient = new GeoClient(binding, endpointAddress);

                IEnumerable<ZipCodeData> zipCodeDataEnumerable = null;
                WcfBindingType wcfBindingType = GetBindingTypeFromRadioButtons();
                GeoClient geoClient = GetGeoClientWithBinding(wcfBindingType);
                
                try
                {
                    zipCodeDataEnumerable = geoClient.GetZipCodes(state);
                }
                catch (FaultException exception)
                {
                    string message = "Exception: \r\n" +
                                     $"Message = {exception.Message} \r\n" +
                                     $"Proxy state = {geoClient.State.ToString()}";

                    ErrorMessage2TextBlock.Text = message;
                }

                if (zipCodeDataEnumerable != null)
                {
                    ZipCodesListBox.ItemsSource = zipCodeDataEnumerable;
                }
                
                geoClient.Close();
            }
        }

        private WcfBindingType GetBindingTypeFromRadioButtons()
        {
            WcfBindingType wcfBindingType = WcfBindingType.NetTcpBinding;

            if (NetTcpBindingRadioButton.IsChecked == true)
            {
                wcfBindingType = WcfBindingType.NetTcpBinding;
            }
            else if (BasicHttpBindingRadioButton.IsChecked == true)
            {
                wcfBindingType = WcfBindingType.BasicHttpBinding;
            }
            else if (WsHttpBindingRadioButton.IsChecked == true)
            {
                wcfBindingType = WcfBindingType.BasicHttpBinding;
            }

            return wcfBindingType;
        }

        private static GeoClient GetGeoClientWithBinding(WcfBindingType wcfBindingType)
        {
            GeoClient geoClient = null;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (wcfBindingType)
            {
                case WcfBindingType.NetTcpBinding:
                    const string netTcpEndpoint = "netTcpEndpoint";
                    geoClient = new GeoClient(netTcpEndpoint);
                    break;

                case WcfBindingType.BasicHttpBinding:
                    const string basicHttpEndpoint = "basicHttpEndpoint";
                    geoClient = new GeoClient(basicHttpEndpoint);
                    break;

                case WcfBindingType.WsHttpBinding:
                    const string wsHttpEndpoint = "wsHttpEndpoint";
                    geoClient = new GeoClient(wsHttpEndpoint);
                    break;
            }

            return geoClient;
        }
    }
}
