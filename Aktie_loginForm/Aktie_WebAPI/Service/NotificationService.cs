using Aktie_WebAPI.Hubs;
using Aktie_WebAPI.Model;
using Microsoft.AspNetCore.SignalR;

namespace Aktie_WebAPI.Service
{
    public class NotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationService(
            IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotificationAsync(PriceAlert alert)
        {
            await _hubContext.Clients.All.SendAsync(
                "ReceiveNotification",
                new
                {
                    stock = alert.StockSymbol,
                    targetPrice = alert.TargetPrice,
                    message = $"{alert.StockSymbol} reached target price!"
                });
        }
    }
}