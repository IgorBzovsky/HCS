using HCS.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HCS.Core
{
    public interface IUnitOfWork
    {
        IUtilityRepository Utilities { get; }
        ILocationRepository Locations { get; }
        IProviderRepository Providers { get; }
        Task CompleteAsync();
    }
}
