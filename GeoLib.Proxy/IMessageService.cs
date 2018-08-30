using System.ServiceModel;

namespace GeoLib.Proxy
{
    [ServiceContract(Namespace = "http://diracsoftware.com/geolibdemo")]
    public interface IMessageService
    {
        [OperationContract]
        void ShowMessage(string message);
    }
}
