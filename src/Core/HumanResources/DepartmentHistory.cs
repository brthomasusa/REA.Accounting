using REA.Accounting.Core.HumanResources.ValueObjects;
using REA.Accounting.SharedKernel.Base;
using REA.Accounting.SharedKernel.Guards;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Core.HumanResources
{
    public class DepartmentHistory : Entity<int>
    {
        private DepartmentHistory
        (
            int id,
            int shiftId,
            DepartmentStartDate startDate,
            DateOnly? endDate
        )
        {
            Id = id;
            ShiftID = shiftId;
            StartDate = startDate.Value;
            EndDate = endDate;

            CheckValidity();
        }

        internal static OperationResult<DepartmentHistory> Create
        (
            int id,
            int shiftId,
            DateOnly startDate,
            DateTime? endDate
        )
        {
            try
            {
                DepartmentHistory history = new
                    (
                        Guard.Against.LessThanZero(id, "Id", "DepartmentHistory id can not be negative."),
                        Guard.Against.LessThanZero(shiftId, "shiftId", "Shift id can not be negative."),
                        DepartmentStartDate.Create(startDate),
                        DateOnly.FromDateTime(endDate is null ? default : (DateTime)endDate)
                    );
                return OperationResult<DepartmentHistory>.CreateSuccessResult(history);

            }
            catch (Exception ex)
            {
                return OperationResult<DepartmentHistory>.CreateFailure(Helpers.GetExceptionMessage(ex));
            }


        }

        public int DepartmentID { get; private set; }
        public int ShiftID { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly? EndDate { get; private set; }

        protected override void CheckValidity()
        {
            if (EndDate != new DateOnly() && StartDate > EndDate)
                throw new ArgumentException("Start date can not be after end date.");
        }
    }
}