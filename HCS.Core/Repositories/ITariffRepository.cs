using HCS.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCS.Core.Repositories
{
    public interface ITariffRepository : IRepository<Tariff>
    {
        Task<Tariff> GetTariffAsync(int id, bool includeRelated = true);
        Task<IEnumerable<Tariff>> GetTariffsByProviderAsync(int providerId);
        
    }
}
