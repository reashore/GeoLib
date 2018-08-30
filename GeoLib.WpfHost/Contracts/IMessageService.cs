using System.ServiceModel;

namespace GeoLib.WpfHost.Contracts
{
    [ServiceContract(Namespace = "http://diracsoftware.com/geolibdemo")]
    public interface IMessageService
    {
        [OperationContract]
        void ShowMessage(string message);
    }
}
