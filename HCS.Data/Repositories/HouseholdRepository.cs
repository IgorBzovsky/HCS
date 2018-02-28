using HCS.Core.Domain;
using HCS.Core.Repositories;

namespace HCS.Data.Repositories
{
    public class HouseholdRepository : Repository<Household>, IHouseholdRepository
    {
        public HouseholdRepository(HcsDbContext context) : base(context)
        {
        }
    }
}
