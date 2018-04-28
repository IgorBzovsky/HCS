using HCS.Core.Domain;
using HCS.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Data.Repositories
{
    public class UtilityBillRepository : Repository<UtilityBill>, IUtilityBillRepository
    {
        public UtilityBillRepository(HcsDbContext context) : base(context)
        {
        }

        public async Task<UtilityBill> GetMetersReadingAsync(int consumerId)
        {
            return await context.UtilityBills
                .Include(u => u.UtilityBillLines)
                .ThenInclude(l => l.ConsumedUtility)
                        .ThenInclude(c => c.ProvidedUtility)
                            .ThenInclude(p => p.Utility)
                .FirstOrDefaultAsync(u => u.ConsumerId == consumerId);
        }

        public async Task<UtilityBill> GetLatestBillAsync(int consumerId)
        {
            return await context.UtilityBills
                .Include(u => u.UtilityBillLines)
                .ThenInclude(l => l.ConsumedUtility)
                        .ThenInclude(c => c.ProvidedUtility)
                            .ThenInclude(p => p.Utility)
                .Where(u => u.ConsumerId == consumerId)
                .OrderByDescending(u => u.DateCreated)
                .FirstOrDefaultAsync();
        }

        public async Task<UtilityBill> GetUtilityBillAsync(int id)
        {
            return await context.UtilityBills
                .Include(u => u.UtilityBillLines)
                    .ThenInclude(l => l.ConsumedUtility)
                        .ThenInclude(c => c.ProvidedUtility)
                            .ThenInclude(p => p.Utility)
                .Include(u => u.UtilityBillLines)
                    .ThenInclude(l => l.ConsumedUtility)
                        .ThenInclude(c => c.Tariff)
                            .ThenInclude(t => t.Blocks)
                .Include(u => u.Consumer)
                    .ThenInclude(c => c.Occupants)
                        .ThenInclude(o => o.ConsumptionNorms)
                .Include(u => u.Consumer)
                    .ThenInclude(c => c.Occupants)
                        .ThenInclude(o => o.Exemption)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<UtilityBill>> GetConsumerUtilityBillsAsync(int consumerId)
        {
            return await context.UtilityBills
                .Include(u => u.UtilityBillLines)
                .Where(u => !u.IsMetersReading && u.ConsumerId == consumerId)
                .ToListAsync();
        }
    }
}
