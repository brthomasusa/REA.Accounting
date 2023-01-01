using REA.Accounting.SharedKernel;

namespace REA.Accounting.Core.HumanResources
{
    public class DepartmentHistory : Entity<int>
    {
        private DepartmentHistory
        (
            int id,
            int shiftId,
            DateOnly startDate,
            DateOnly? endDate
        )
        {
            Id = id;
            ShiftID = shiftId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public static DepartmentHistory Create
        (
            int id,
            int shiftId,
            DateOnly startDate,
            DateOnly? endDate
        )
        {
            return new DepartmentHistory(id, shiftId, startDate, endDate);
        }

        public int DepartmentID { get; private set; }
        public int ShiftID { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly? EndDate { get; private set; }
    }
}