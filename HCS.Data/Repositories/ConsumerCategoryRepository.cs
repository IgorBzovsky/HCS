using HCS.Core.Domain;
using HCS.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCS.Data.Repositories
{
    public class ConsumerCategoryRepository : Repository<ConsumerCategory>, IConsumerCategoryRepository
    {
        public ConsumerCategoryRepository(HcsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ConsumerCategory>> GetCategoriesByTypeName(string name)
        {
            return await context.ConsumerCategories
                .Include(c => c.ConsumerType)
                .Where(x => x.ConsumerType.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                .ToListAsync();
        }
    }
}
