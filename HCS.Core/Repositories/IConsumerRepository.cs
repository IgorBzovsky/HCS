using HCS.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCS.Core.Repositories
{
    public interface IConsumerRepository : IRepository<Consumer>
    {
        Task<Consumer> GetConsumerAsync(int id, bool includeRelated = true);
        Task<Consumer> GetConsumerByLocationAsync(int id, bool includeRelated = true);
        Task<IEnumerable<Consumer>> GetAllIncludeLocation();
    }
}
