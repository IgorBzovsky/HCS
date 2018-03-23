using HCS.Core.Domain;
using HCS.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Data.Repositories
{
    public class OccupantRepository : Repository<Occupant>, IOccupantRepository
    {
        public OccupantRepository(HcsDbContext context) : base(context)
        {
        }

        public async Task<Occupant> GetOccupantAsync(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await GetAsync(id);
            return await context.Occupants
                .Include(o => o.ConsumptionNorms)
                    .ThenInclude(c => c.ConsumedUtility)
                        .ThenInclude(cu => cu.ProvidedUtility)
                            .ThenInclude(pu => pu.Utility)
                .Include(o => o.Exemption)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Occupant>> GetOccupantsByHouseholdAsync(int householdId, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Occupants.Where(o => o.ConsumerId == householdId).ToListAsync();

            return await context.Occupants
                .Include(o => o.ConsumptionNorms)
                    .ThenInclude(c => c.ConsumedUtility)
                        .ThenInclude(cu => cu.ProvidedUtility)
                            .ThenInclude(pu => pu.Utility)
                .Include(o => o.Exemption)
                .Where(o => o.ConsumerId == householdId)
                .ToListAsync();
        }
    }
}
