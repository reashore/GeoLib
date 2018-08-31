using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace GeoLib.Contracts
{
    [ServiceContract]
    public interface IGeoService
    {
        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<string> GetStates(bool isPrimaryOnly);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        ZipCodeData GetZipCodeInfo(string zipCode);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        IEnumerable<ZipCodeData> GetZipCodes(string state);

        [OperationContract(Name = "GetZipCodesForRange")]
        [FaultContract(typeof(Exception))]
        IEnumerable<ZipCodeData> GetZipCodes(string zipCode, int zipCodeRange);
    }
}
