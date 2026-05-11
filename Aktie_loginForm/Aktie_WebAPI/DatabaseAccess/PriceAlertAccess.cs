
using Aktie_WebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace Aktie_WebAPI.DatabaseAccess
{
    public class PriceAlertRepository
    {
        private readonly NotifAccess _context;

        public PriceAlertRepository(NotifAccess context)
        {
            _context = context;
        }

        // Opret ny alarm
        public async Task AddAlertAsync(PriceAlert alert)
        {
            _context.PriceAlerts.Add(alert);
            await _context.SaveChangesAsync();
        }

        // Hent alle aktive alarmer
        public async Task<List<PriceAlert>> GetActiveAlertsAsync()
        {
            return await _context.PriceAlerts
                .Where(a => !a.IsTriggered)
                .ToListAsync();
        }

        // Markér alarm som triggered
        public async Task MarkAsTriggeredAsync(PriceAlert alert)
        {
            alert.IsTriggered = true;
            alert.TriggeredAt = DateTime.UtcNow;

            _context.PriceAlerts.Update(alert);

            await _context.SaveChangesAsync();
        }
    }
}