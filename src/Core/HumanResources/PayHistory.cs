using REA.Accounting.SharedKernel;

namespace REA.Accounting.Core.HumanResources
{
    public class PayHistory : Entity<int>
    {
        private PayHistory
        (
            int id,
            DateTime rateChangeDate,
            decimal rate,
            PayFrequencyEnum payFrequency
        )
        {
            Id = id;
            RateChangeDate = rateChangeDate;
            Rate = rate;
            PayFrequency = payFrequency;
        }

        public static PayHistory Create
        (
            int id,
            DateTime rateChangeDate,
            decimal rate,
            PayFrequencyEnum payFrequency
        )
        {
            return new PayHistory(id, rateChangeDate, rate, payFrequency);
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