using System.Collections.Generic;
using System.Linq;

namespace HCS.Core.Domain
{
    public class UtilityBillLine
    {
        private const int SEASON_BEGIN = 10;
        private const int SEASON_END = 4;
        private const int JANUARY = 1;
        private const int DECEMBER = 12;

        public int Id { get; set; }
        public double MeterReadingStart { get; set; }
        public double MeterReadingEnd { get; set; }
        public double Amount { get; set; }
        public decimal Price { get; set; }
        public int ConsumedUtilityId { get; set; }
        public ConsumedUtility ConsumedUtility { get; set; }
        public int UtilityBillId { get; set; }
        public UtilityBill UtilityBill { get; set; }



        public void Calculate(IEnumerable<Occupant> occupants, int month, bool hasSubsidy, Tariff tariff = null)
        {
            if (ConsumedUtility == null)
                return;
            if (month < JANUARY || month > DECEMBER)
                return;
            bool isSeasonal = false;
            if (ConsumedUtility.ProvidedUtility.Utility.IsSeasonal && !(month < SEASON_BEGIN && month > SEASON_END))
                isSeasonal = true;
            var actualTariff = tariff ?? ConsumedUtility.Tariff;
            if (actualTariff == null)
                return;
            SetAmount();
            CalculateBlocks(actualTariff);
            
            if(!hasSubsidy)
            {
                if (ConsumedUtility.ProvidedUtility.Utility.Strategy == Strategies.MaxExemption)
                {
                    CalculateMaxExemption(occupants, isSeasonal);
                }
                else
                {
                    CalculateSumExemption(occupants, isSeasonal);
                }
            }
            Price += actualTariff.SubscriberFee;
            if(hasSubsidy)
                Price -= ConsumedUtility.Subsidy ?? 0;
            if (Price < 0)
                Price = 0;
        }

        private void SetAmount()
        {
            if(ConsumedUtility.HasMeter)
            {
                Amount = MeterReadingEnd - MeterReadingStart;
            }
            else
            {
                Amount = ConsumedUtility.Consumption == null ? 0 : ConsumedUtility.Consumption.Value;
            }
            Amount = Amount > 0 ? Amount : 0;
        }

        private void CalculateBlocks(Tariff tariff)
        {
            var amount = Amount;
            var blocks = tariff.Blocks.OrderBy(b => b.Limit);
            foreach (var block in blocks)
            {
                if (amount >= block.Limit)
                {
                    amount -= block.Limit;
                    Price += (decimal)block.Limit * block.Price;
                }
                else
                {
                    Price += (decimal)amount * block.Price;
                    amount = 0;
                }
            }
            Price += (decimal)amount * tariff.Price;
        }

        private void CalculateMaxExemption(IEnumerable<Occupant> occupants, bool isSeasonal)
        {
            var normsSum = occupants.SelectMany(o => o.ConsumptionNorms.Where(n => n.IsSeasonal == isSeasonal && n.ConsumedUtilityId == ConsumedUtilityId)).Sum(x => x.Amount);
            var occupantsWithExemptions = occupants.Where(o => o.Exemption != null).OrderByDescending(o => o.Exemption).ToList();
            var exemption = occupantsWithExemptions.Select(x => x.Exemption).FirstOrDefault();
            if(exemption != null)
                Price -= (decimal)(normsSum / Amount) * (decimal)(exemption.Percent / 100.0) * Price;
            if (Price <= 0)
                Price = 0;
        }

        private void CalculateSumExemption(IEnumerable<Occupant> occupants, bool isSeasonal)
        {
            var orderedOccupants = occupants.OrderByDescending(x => x.Exemption.Percent).ToList();
            foreach (var occupant in orderedOccupants)
            {
                var norm = occupant.ConsumptionNorms.FirstOrDefault(x => x.ConsumedUtilityId == ConsumedUtilityId && x.IsSeasonal == isSeasonal)?.Amount ?? 0;
                var exemption = occupant.Exemption.Percent;
                Price -= (decimal)(norm / Amount) * (decimal)(exemption / 100.0) * Price;
                if(Price <= 0)
                {
                    Price = 0;
                    return;
                }
            }
        }
    }
}
