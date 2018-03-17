using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCS.Core.Domain
{
    public class ConsumerType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ConsumerCategory> ConsumerCategories { get; set; }

        public ConsumerType()
        {
            ConsumerCategories = new Collection<ConsumerCategory>();
        }
    }
}
