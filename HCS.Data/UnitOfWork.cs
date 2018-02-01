using HCS.Core;
using HCS.Core.Repositories;
using HCS.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
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
        }

        public IUtilityRepository Utilities { get; private set; }
        public ILocationRepository Locations { get; private set; }
        public IProviderRepository Providers { get; private set; }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
