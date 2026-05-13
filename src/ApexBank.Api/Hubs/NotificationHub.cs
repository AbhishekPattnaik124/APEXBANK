using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ApexBank.Api.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }

        public async Task BroadcastSystemUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveSystemUpdate", message);
        }
    }
}
