using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using GeoLib.Contracts;

namespace GeoLib.Proxy
{
    public class GeoClient : ClientBase<IGeoService>, IGeoService
    {
        public GeoClient(string endpointName) : base(endpointName)
        {
        }

        public GeoClient(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
        {
        }

        public IEnumerable<string> GetStates(bool isPrimaryOnly)
        {
            return Channel.GetStates(isPrimaryOnly);
        }

        public ZipCodeData GetZipCodeInfo(string zipCode)
        {
            return Channel.GetZipCodeInfo(zipCode);
        }

        public IEnumerable<ZipCodeData> GetZipCodes(string state)
        {
            return Channel.GetZipCodes(state);
        }

        public IEnumerable<ZipCodeData> GetZipCodes(string zipCode, int zipCodeRange)
        {
            return Channel.GetZipCodes(zipCode, zipCodeRange);
        }
    }
}
