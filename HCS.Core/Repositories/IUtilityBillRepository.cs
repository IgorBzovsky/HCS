using HCS.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCS.Core.Repositories
{
    public interface IUtilityBillRepository : IRepository<UtilityBill>
    {
        Task<UtilityBill> GetMetersReadingAsync(int consumerId);
        Task<UtilityBill> GetLatestBillAsync(int consumerId);
        Task<UtilityBill> GetUtilityBillAsync(int id);
        Task<IEnumerable<UtilityBill>> GetConsumerUtilityBillsAsync(int consumerId);
    }
}
