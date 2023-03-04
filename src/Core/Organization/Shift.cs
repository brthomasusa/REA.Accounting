#pragma warning disable CS8618

using REA.Accounting.SharedKernel.Base;
using REA.Accounting.Core.HumanResources.ValueObjects;
using REA.Accounting.SharedKernel.Utilities;

namespace REA.Accounting.Core.Organization
{
    public sealed class Shift : Entity<int>
    {
        private Shift
        (
            int id,
            ShiftName name,
            ShiftTime startTime,
            ShiftTime endTime
        )
        {
            Id = id;
            Name = name.Value!;
            StartTime = startTime.Value;
            EndTime = endTime.Value;
        }

        internal static Result<Shift> Create
        (
            int id,
            string name,
            int startHour,
            int startMinute,
            int endHour,
            int endMinute
        )
        {
            try
            {
                Shift shift = new
                 (id,
                 ShiftName.Create(name),
                 ShiftTime.Create(startHour, startMinute),
                 ShiftTime.Create(endHour, endMinute)
                 );

                return shift;
            }
            catch (Exception ex)
            {
                return Result<Shift>.Failure<Shift>(new Error("Shift.Create", Helpers.GetExceptionMessage(ex)));
            }
        }

        internal Result<Shift> Update(string name, int startHour, int startMinute, int endHour, int endMinute)
        {
            try
            {
                Name = ShiftName.Create(name);
                StartTime = ShiftTime.Create(startHour, startMinute).Value;
                EndTime = ShiftTime.Create(endHour, endMinute).Value;
                UpdateModifiedDate();

                return this;
            }
            catch (Exception ex)
            {
                return Result<Shift>.Failure<Shift>(new Error("Shift.Update", Helpers.GetExceptionMessage(ex)));
            }
        }

        public string Name { get; private set; }

        public TimeOnly StartTime { get; private set; }

        public TimeOnly EndTime { get; private set; }
    }
}