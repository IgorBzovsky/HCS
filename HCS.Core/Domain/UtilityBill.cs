using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HCS.Core.Domain
{
    public class UtilityBill
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public bool IsMetersReading { get; set; }
        public int ConsumerId { get; set; }
        public Consumer Consumer { get; set; }
        public ICollection<UtilityBillLine> UtilityBillLines { get; set; }

        public UtilityBill()
        {
            UtilityBillLines = new Collection<UtilityBillLine>();
        }

        public void Calculate(Tariff tariff = null)
        {
            foreach (var line in UtilityBillLines)
            {
                line.Calculate(Consumer.Occupants, Month, Consumer.HasSubsidy, tariff);
            }
        }

        public decimal GetTotal()
        {
            decimal total = 0;
            foreach(var line in UtilityBillLines)
            {
                total += line.Price;
            }
            return total;
        }
    }
}
