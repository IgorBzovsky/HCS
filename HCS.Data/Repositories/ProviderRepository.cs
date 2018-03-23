using HCS.Core.Domain;
using HCS.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HCS.Data.Repositories
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(HcsDbContext context) : base(context)
        {
        }

        public async Task<Provider> GetProviderAsync(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await GetAsync(id);

            return await context.Providers
                .Include(p => p.ProvidedUtilities)
                    .ThenInclude(u => u.Utility)
                .Include(p => p.ProvidedUtilities)
                    .ThenInclude(u => u.Tariffs)
                        .ThenInclude(t => t.ConsumerType)
                .Include(p => p.Location)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
