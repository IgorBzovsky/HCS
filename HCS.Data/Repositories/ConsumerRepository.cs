using HCS.Core.Domain;
using HCS.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HCS.Data.Repositories
{
    public class ConsumerRepository : Repository<Consumer>, IConsumerRepository
    {
        public ConsumerRepository(HcsDbContext context) : base(context)
        {
        }
    }
}
