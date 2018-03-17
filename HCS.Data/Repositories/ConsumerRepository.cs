using HCS.Core.Domain;
using HCS.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Data.Repositories
{
    public class ConsumerRepository : Repository<Consumer>, IConsumerRepository
    {
        public ConsumerRepository(HcsDbContext context) : base(context)
        {
        }

        public async Task<Consumer> GetConsumerAsync(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await GetAsync(id);

            return await context.Consumers
                .Include(c => c.ConsumedUtilities)
                    .ThenInclude(p => p.ProvidedUtility)
                    .ThenInclude(u => u.Utility)
                .Include(c => c.Location)
                .Include(c => c.ConsumerCategory)
                    .ThenInclude(x => x.ConsumerType)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Consumer> GetConsumerByLocationAsync(int locationId, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Consumers.FirstOrDefaultAsync(x => x.LocationId == locationId);
            return await context.Consumers
                .Include(c => c.ConsumedUtilities)
                    .ThenInclude(p => p.ProvidedUtility)
                    .ThenInclude(u => u.Utility)
                .Include(p => p.Location)
                .Include(c => c.ConsumerCategory)
                    .ThenInclude(x => x.ConsumerType)
                .FirstOrDefaultAsync(x => x.LocationId == locationId);
        }

        public async Task<IEnumerable<Consumer>> GetAllIncludeLocation()
        {
            return await context.Consumers
                .Include(p => p.Location)
                    .ThenInclude(l => l.Parent)
                        .ThenInclude(l => l.Parent)
                            .ThenInclude(l => l.Parent)
                                .ThenInclude(l => l.Parent)
                .Include(c => c.ConsumerCategory)
                    .ThenInclude(x => x.ConsumerType)
                .Where(x => x.IsDraft)
                .ToListAsync();
        }
    }
}
