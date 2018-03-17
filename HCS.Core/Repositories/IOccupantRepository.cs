using HCS.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCS.Core.Repositories
{
    public interface IOccupantRepository: IRepository<Occupant>
    {
        Task<IEnumerable<Occupant>> GetOccupantsByHouseholdAsync(int householdId, bool includeRelated = true);
        Task<Occupant> GetOccupantAsync(int id, bool includeRelated = true);
    }
}
