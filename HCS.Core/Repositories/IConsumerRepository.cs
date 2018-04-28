using HCS.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCS.Core.Repositories
{
    public interface IConsumerRepository : IRepository<Consumer>
    {
        Task<Consumer> GetConsumerAsync(int id, bool includeRelated = true);
        Task<Consumer> GetConsumerByLocationAsync(int id, bool includeRelated = true);
        Task<IEnumerable<Consumer>> GetAllIncludeLocationAsync();
        Task<IEnumerable<Consumer>> GetAllByProviderAsync(int providerId);
        Task<IEnumerable<Consumer>> GetUserConsumersAsync(string userId);
        Task<IEnumerable<ConsumerCategory>> GetCategoriesByTypeNameAsync(string name);
        Task<IEnumerable<ConsumerCategory>> GetCategoriesByTypeIdAsync(int typeId);
        Task<IEnumerable<ConsumerType>> GetConsumerTypesAsync();
        Task<IEnumerable<Exemption>> GetExemptionsAsync();
    }
}
