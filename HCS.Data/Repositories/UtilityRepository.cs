using HCS.Core.Domain;
using HCS.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HCS.Data.Repositories
{
    public class UtilityRepository : Repository<Utility>, IUtilityRepository
    {
        public UtilityRepository(HcsDbContext context) : base(context)
        {
        }
    }
}
