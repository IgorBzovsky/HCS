using HCS.Core.Domain;
using HCS.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HCS.Data.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(HcsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Location>> GetLocationsIncludeChildrenAsync()
        {
            return await context.Locations
                .Include(x => x.Children).ToListAsync();
        }

        public async Task<Location> GetLocationIncludeParentAsync(int id)
        {
            var locations = await context.Locations.FromSql("LocationWithParents @LocationId", new SqlParameter("LocationId", id)).ToListAsync();
            return locations.Find(x => x.Id == id);
        }

        public async Task<Location> GetLocationByAddressAsync(int parentId, string building, string appartment)
        {
            var locations = await context.Locations.FromSql("LocationByAddress @ParentId, @Building, @Appartment", new SqlParameter("ParentId", parentId), new SqlParameter("Building", building), new SqlParameter("Appartment", appartment)).ToListAsync();
            return locations.Find(x => x.ParentId == parentId && x.Building.Equals(building) && x.Appartment.Equals(appartment));
        }
    }
}
