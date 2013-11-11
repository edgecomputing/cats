
using Microsoft.AspNet.SignalR;

namespace Cats.Services.Common
{
    public class NotificationHub : Hub
    {
        public void SendNotifications(string message)
        {
            Clients.All.receiveNotification(message);
        }
    }
}
