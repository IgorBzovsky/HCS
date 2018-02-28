using HCS.Core;
using HCS.Core.Repositories;
using HCS.Data.Repositories;
using System.Threading.Tasks;

namespace HCS.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HcsDbContext _context;
        public UnitOfWork(HcsDbContext context)
        {
            _context = context;
            Utilities = new UtilityRepository(_context);
            Locations = new LocationRepository(_context);
            Providers = new ProviderRepository(_context);
            Households = new HouseholdRepository(_context);

        }

        public IUtilityRepository Utilities { get; private set; }
        public ILocationRepository Locations { get; private set; }
        public IProviderRepository Providers { get; private set; }
        public IHouseholdRepository Households { get; private set; }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
