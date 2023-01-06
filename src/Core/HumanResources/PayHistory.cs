using REA.Accounting.Core.HumanResources.ValueObjects;
using REA.Accounting.SharedKernel;

namespace REA.Accounting.Core.HumanResources
{
    public class PayHistory : Entity<int>
    {
        private PayHistory
        (
            int id,
            DateOfRateChange rateChangeDate,
            RateOfPay rate,
            PayFrequencyEnum payFrequency
        )
        {
            Id = id;
            RateChangeDate = rateChangeDate.Value;
            Rate = rate.Value;
            PayFrequency = payFrequency;
        }

        internal static PayHistory Create
        (
            int id,
            DateTime rateChangeDate,
            decimal rate,
            PayFrequencyEnum payFrequency
        )
        {
            if (!Enum.IsDefined(typeof(PayFrequencyEnum), payFrequency))
            {
                throw new ArgumentException("Invalid pay frequency.");
            }

            return new PayHistory(id, DateOfRateChange.Create(rateChangeDate), RateOfPay.Create(rate), payFrequency);
        }

        public DateTime RateChangeDate { get; private set; }
        public decimal Rate { get; private set; }
        public PayFrequencyEnum PayFrequency { get; private set; }
    }

    public enum PayFrequencyEnum : int
    {
        Monthly = 1,
        BiWeekly = 2
    }
}