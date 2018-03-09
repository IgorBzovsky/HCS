using HCS.Core.Repositories;
using System.Threading.Tasks;

namespace HCS.Core
{
    public interface IUnitOfWork
    {
        IUtilityRepository Utilities { get; }
        ILocationRepository Locations { get; }
        IProviderRepository Providers { get; }
        IConsumerRepository Consumers { get; }
        Task CompleteAsync();
    }
}
