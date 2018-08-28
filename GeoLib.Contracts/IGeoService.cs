using System.Collections.Generic;
using System.ServiceModel;

namespace GeoLib.Contracts
{
    [ServiceContract]
    public interface IGeoService
    {
        [OperationContract]
        ZipCodeData GetZipCodeInfo(string zipCode);

        [OperationContract]
        IEnumerable<string> GetStates(bool isPrimaryOnly);

        [OperationContract]
        IEnumerable<ZipCodeData> GetZipCodes(string state);

        [OperationContract(Name = "GetZipCodesForRange")]
        IEnumerable<ZipCodeData> GetZipCodes(string zipCode, int zipCodeRange);
    }
}
