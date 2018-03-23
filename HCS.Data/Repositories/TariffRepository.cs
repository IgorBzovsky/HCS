using HCS.Core.Domain;
using HCS.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Data.Repositories
{
    public class TariffRepository : Repository<Tariff>, ITariffRepository
    {
        public TariffRepository(HcsDbContext context) : base(context)
        {
        }

        public async Task<Tariff> GetTariffAsync(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await GetAsync(id);

            return await context.Tariffs
                .Include(t => t.Blocks)
                .Include(t => t.ConsumerType)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Tariff>> GetTariffsByProviderAsync(int providerId)
        {
            return await context.Tariffs
                .Include(t => t.ProvidedUtility)
                    .ThenInclude(p => p.Utility)
                .Include(t => t.ConsumedUtilities)
                .Include(t => t.ConsumerType)
                .Where(t => t.ProvidedUtility.ProviderId == providerId)
                .ToListAsync();
        }
    }
}
