using System.Collections.Generic;
using System.ServiceModel;
using GeoLib.Contracts;

namespace GeoLib.Proxy
{
    public class GeoClient : ClientBase<IGeoService>, IGeoService
    {
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
