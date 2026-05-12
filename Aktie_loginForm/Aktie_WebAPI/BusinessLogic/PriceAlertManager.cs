using Aktie_WebAPI.DatabaseAccess;
using Aktie_WebAPI.Service;
using System.Globalization;

namespace Aktie_WebAPI.buisnesslogic
{
    public class PriceAlertManager
    {
        private readonly PriceAlertRepository _repository;
        private readonly StockService _stockService;
        private readonly NotificationService _notificationService;

        public PriceAlertManager(
            PriceAlertRepository repository,
            StockService stockService,
            NotificationService notificationService)
        {
            _repository = repository;
            _stockService = stockService;
            _notificationService = notificationService;
        }

        public async Task CheckAlertsAsync()
        {
            var alerts = await _repository.GetActiveAlertsAsync();

            foreach (var alert in alerts)
            {
                var quote =
                    await _stockService.GetQuoteAsync(alert.StockSymbol);

                if (quote == null)
                    continue;

                bool parsed = decimal.TryParse(
                    quote.Price,
                    NumberStyles.Any,
                    CultureInfo.InvariantCulture,
                    out decimal currentPrice);

                if (!parsed)
                    continue;

                if (currentPrice <= alert.TargetPrice)
                {
                    await _notificationService
                        .SendNotificationAsync(alert);

                    await _repository
                        .MarkAsTriggeredAsync(alert);
                }
            }
        }
    }
}