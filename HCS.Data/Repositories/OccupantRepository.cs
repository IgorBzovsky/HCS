using HCS.Core.Domain;
using HCS.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
                .Include(o => o.Exemption)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Occupant>> GetOccupantsByHouseholdAsync(int householdId, bool includeRelated = true)
        {
            //if (!includeRelated)
            //    return await context.Occupants.FirstOrDefaultAsync(x => x.)

            //return await context.Consumers
            //    .Include(c => c.ConsumedUtilities)
            //        .ThenInclude(p => p.ProvidedUtility)
            //        .ThenInclude(u => u.Utility)
            //    .Include(c => c.Location)
            //    .FirstOrDefaultAsync(x => x.Id == id);
            return null;
        }
    }
}
