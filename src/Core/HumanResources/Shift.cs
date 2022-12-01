#pragma warning disable CS8618

using REA.Accounting.SharedKernel;
using REA.Accounting.Core.HumanResources.ValueObjects;

namespace REA.Accounting.Core.HumanResources
{
    public class Shift : Entity<int>
    {
        protected Shift() { }

        private Shift
        (
            int id,
            ShiftName name,
            ShiftTime startTime,
            ShiftTime endTime
        )
            : this()
        {
            Id = id;
            Name = name.Value!;
            StartTime = startTime.Value;
            EndTime = endTime.Value;
        }

        public static Shift Create
        (
            int id,
            ShiftName name,
            int startHour,
            int startMinute,
            int endHour,
            int endMinute
        )
        => new Shift
            (id,
            ShiftName.Create(name),
            ShiftTime.Create(startHour, startMinute),
            ShiftTime.Create(endHour, endMinute)
            );

        public string Name { get; private set; }
        public void UpdateShiftName(string value)
        {
            Name = ShiftName.Create(value).Value!;
            UpdateLastModifiedDate();
        }

        public TimeOnly StartTime { get; private set; }
        public void UpdateStartTime(int hour, int minute)
        {
            StartTime = ShiftTime.Create(hour, minute).Value;
            UpdateLastModifiedDate();
        }

        public TimeOnly EndTime { get; private set; }
        public void UpdateEndTime(int hour, int minute)
        {
            EndTime = ShiftTime.Create(hour, minute).Value;
            UpdateLastModifiedDate();
        }
    }
}