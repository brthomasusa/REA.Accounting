using REA.Accounting.Core.HumanResources.ValueObjects;
using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Guards;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Core.HumanResources
{
    public sealed class PayHistory : Entity<int>
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

        internal static Result<PayHistory> Create
        (
            int id,
            DateTime rateChangeDate,
            decimal rate,
            PayFrequencyEnum payFrequency
        )
        {
            try
            {
                PayHistory history = new
                    (
                        Guard.Against.LessThanZero(id, "Id", "PayHistory id can not be negative."),
                        DateOfRateChange.Create(rateChangeDate),
                        RateOfPay.Create(rate),
                        Enum.IsDefined(typeof(PayFrequencyEnum), payFrequency) ? payFrequency : throw new ArgumentException("Invalid pay frequency.")
                    );
                return history;
            }
            catch (Exception ex)
            {
                return Result<PayHistory>.Failure<PayHistory>(new Error("PayHistory.Create", Helpers.GetExceptionMessage(ex)));
            }
        }

        public DateTime RateChangeDate { get; }
        public decimal Rate { get; }
        public PayFrequencyEnum PayFrequency { get; }
    }

    public enum PayFrequencyEnum : int
    {
        Monthly = 1,
        BiWeekly = 2
    }
}