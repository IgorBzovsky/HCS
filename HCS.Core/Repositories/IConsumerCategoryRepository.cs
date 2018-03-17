using HCS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Core.Repositories
{
    public interface IConsumerCategoryRepository : IRepository<ConsumerCategory>
    {
        Task<IEnumerable<ConsumerCategory>> GetCategoriesByTypeName(string name);
    }
}
