﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Windows;
using GeoLib.Contracts;
using GeoLib.Proxy;

namespace GeoLib.Client
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            int managedThreadId = Thread.CurrentThread.ManagedThreadId;
            string currentProcessId = Process.GetCurrentProcess().Id.ToString();

            Title = $"Thread ID = {managedThreadId}, Process ID = {currentProcessId}";
        }

        private void GetZipCodeInfoButton_Click(object sender, RoutedEventArgs e)
        {
            // ReSharper disable once InvertIf
            if (ZipCodeTextBox.Text != "")
            {
                const string endpoint = "netTcpEndpoint";
                GeoClient geoClient = new GeoClient(endpoint);
                ZipCodeData zipCodeData = geoClient.GetZipCodeInfo(ZipCodeTextBox.Text);

                try
                {
                    if (zipCodeData != null)
                    {
                        CityOutputLabel.Content = zipCodeData.City;
                        StateOutputLabel.Content = zipCodeData.State;
                    }

                    geoClient.Close();

                }
                catch (FaultException<ExceptionDetail> exception)
                {
                    string message = $"Type = {exception.GetType().Name} ";
                    message += $"Message = {exception.Message} ";
                    message += $"Proxy state = {geoClient.State.ToString()}";

                    Console.WriteLine(message);
                    Debug.WriteLine(message);
                }
                catch (Exception exception)
                {
                    string message = $"Type = {exception.GetType().Name} ";
                    message += $"Message = {exception.Message} ";
                    message += $"Proxy state = {geoClient.State.ToString()}";

                    Console.WriteLine(message);
                    Debug.WriteLine(message);
                }
            }
        }

        private void GetZipCodesButton_Click(object sender, RoutedEventArgs e)
        {
            // ReSharper disable once InvertIf
            if (StateTextBox.Text != "")
            {
                EndpointAddress endpointAddress = new EndpointAddress("net.tcp://localhost:8009/GeoService");
                Binding binding = new NetTcpBinding();
                GeoClient geoClient = new GeoClient(binding, endpointAddress);
                IEnumerable<ZipCodeData> zipCodeDataEnumerable = geoClient.GetZipCodes(StateTextBox.Text);

                if (zipCodeDataEnumerable != null)
                {
                    ZipCodesListBox.ItemsSource = zipCodeDataEnumerable;
                }
                
                geoClient.Close();
            }
        }
    }
}
