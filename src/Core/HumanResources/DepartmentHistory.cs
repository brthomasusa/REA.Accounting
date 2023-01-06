using REA.Accounting.SharedKernel;
using REA.Accounting.Core.HumanResources.ValueObjects;

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

        internal static DepartmentHistory Create
        (
            int id,
            int shiftId,
            DateOnly startDate,
            DateTime? endDate
        )
        {
            return new DepartmentHistory(id, shiftId, DepartmentStartDate.Create(startDate), DateOnly.FromDateTime(endDate is null ? default : (DateTime)endDate));
        }

        public int DepartmentID { get; private set; }
        public int ShiftID { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly? EndDate { get; private set; }

        protected override void CheckValidity()
        {
            if (EndDate is not null && StartDate > EndDate)
                throw new ArgumentException("Start date can not be after end date.");
        }
    }
}