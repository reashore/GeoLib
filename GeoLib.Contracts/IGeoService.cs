using System.Collections.Generic;
using System.ServiceModel;

namespace GeoLib.Contracts
{
    [ServiceContract]
    public interface IGeoService
    {
        [OperationContract]
        List<string> GetStates(bool isPrimaryOnly);

        [OperationContract]
        ZipCodeData GetZipCodeInfo(string zipCode);

        [OperationContract]
        List<ZipCodeData> GetZipCodes(string state);

        [OperationContract(Name = "GetZipCodesForRange")]
        List<ZipCodeData> GetZipCodes(string zipCode, int zipCodeRange);
    }
}
