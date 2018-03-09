using HCS.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCS.Core.Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<IEnumerable<Location>> GetLocationsIncludeChildrenAsync();
        Task<Location> GetLocationIncludeParentAsync(int id);
        Task<Location> GetLocationByAddressAsync(int parentId, string building, string appartment);
    }
}
