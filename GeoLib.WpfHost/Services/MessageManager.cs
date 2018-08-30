using GeoLib.WpfHost.Contracts;

namespace GeoLib.WpfHost.Services
{
    public class MessageManager : IMessageService
    {
        public void ShowMessage(string message)
        {
            MainWindow.MainUi.ShowMessage(message);
        }
    }
}
