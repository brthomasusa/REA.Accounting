#pragma warning disable CS8600, CS8625

using Xunit;
using REA.Accounting.Core.Organization;
using REA.Accounting.Core.HumanResources.ValueObjects;
using REA.Accounting.SharedKernel.CommonValueObjects;

namespace REA.Accounting.UnitTests.HumanResources
{
    public class HrEntityObject_Tests
    {
        [Fact]
        public void Create_Department_ShouldSucceed()
        {
            const string name = "Administration";
            const string groupName = "Admins";

            var exception = Record.Exception(() =>
                Department.Create
                (
                    1,
                   OrganizationName.Create(name),
                   OrganizationName.Create(groupName)
                )
            );

            Assert.Null(exception);
        }

        [Fact]
        public void Create_Department_NullName_ShouldFail()
        {
            const string name = null;
            const string groupName = "Admins";

            var exception = Record.Exception(() =>
                Department.Create
                (
                    1,
                   OrganizationName.Create(name!),
                   OrganizationName.Create(groupName)
                )
            );

            Assert.NotNull(exception);
        }

        [Fact]
        public void Create_Department_NullGroupName_ShouldFail()
        {
            const string name = "Administration";
            const string groupName = null;

            var exception = Record.Exception(() =>
                Department.Create
                (
                    1,
                   OrganizationName.Create(name),
                   OrganizationName.Create(groupName!)
                )
            );

            Assert.NotNull(exception);
        }

        [Fact]
        public void Shift_Create_ShouldSucceed()
        {
            const string name = "Day Shift";
            var exception = Record.Exception(() =>
                Shift.Create
                (
                    1,
                    ShiftName.Create(name),
                    8,
                    0,
                    17,
                    0
                )
            );

            Assert.Null(exception);
        }

        [Fact]
        public void Shift_Create_ShiftNameIsNull_ShouldFail()
        {
            const string name = null;
            var exception = Record.Exception(() =>
                Shift.Create
                (
                    1,
                    ShiftName.Create(name!),
                    8,
                    0,
                    17,
                    0
                )
            );

            Assert.NotNull(exception);
        }

        [Fact]
        public void Shift_Create_InvalidStartHour_ShouldFail()
        {
            const string name = "Day Shift";
            var exception = Record.Exception(() =>
                Shift.Create
                (
                    1,
                    ShiftName.Create(name),
                    24,
                    0,
                    17,
                    0
                )
            );

            Assert.NotNull(exception);
        }

        [Fact]
        public void Shift_Create_InvalidStartMinute_ShouldFail()
        {
            const string name = "Day Shift";
            var exception = Record.Exception(() =>
                Shift.Create
                (
                    1,
                    ShiftName.Create(name),
                    8,
                    60,
                    17,
                    0
                )
            );

            Assert.NotNull(exception);
        }

        [Fact]
        public void Shift_Create_InvalidEndHour_ShouldFail()
        {
            const string name = "Day Shift";
            var exception = Record.Exception(() =>
                Shift.Create
                (
                    1,
                    ShiftName.Create(name),
                    8,
                    0,
                    24,
                    0
                )
            );

            Assert.NotNull(exception);
        }

        [Fact]
        public void Shift_Create_InvalidEndMinute_ShouldFail()
        {
            const string name = "Day Shift";
            var exception = Record.Exception(() =>
                Shift.Create
                (
                    1,
                    ShiftName.Create(name),
                    8,
                    0,
                    17,
                    60
                )
            );

            Assert.NotNull(exception);
        }

        [Fact]
        public void Shift_Update_ShiftName_ShouldSucceed()
        {
            Shift shift = GetShiftForEditing();

            var exception = Record.Exception(() => shift.UpdateShiftName("Midnight Shift"));

            Assert.Null(exception);
        }

        [Fact]
        public void Shift_Update_ShiftName_Null_ShouldFail()
        {
            Shift shift = GetShiftForEditing();

            var exception = Record.Exception(() => shift.UpdateShiftName(null));

            Assert.NotNull(exception);
        }

        [Fact]
        public void Shift_Update_StartTime_ShouldSucceed()
        {
            Shift shift = GetShiftForEditing();

            var exception = Record.Exception(() => shift.UpdateStartTime(9, 30));

            Assert.Null(exception);
        }

        [Fact]
        public void Shift_Update_StartTime_InvalidHour_ShouldFail()
        {
            Shift shift = GetShiftForEditing();

            var exception = Record.Exception(() => shift.UpdateStartTime(24, 30));

            Assert.NotNull(exception);
        }

        [Fact]
        public void Shift_Update_EndTime_ShouldSucceed()
        {
            Shift shift = GetShiftForEditing();

            var exception = Record.Exception(() => shift.UpdateEndTime(18, 30));

            Assert.Null(exception);
        }

        [Fact]
        public void Shift_Update_EndTime__InvalidMinute_ShouldFail()
        {
            Shift shift = GetShiftForEditing();

            var exception = Record.Exception(() => shift.UpdateEndTime(18, 60));

            Assert.NotNull(exception);
        }

        private static Shift GetShiftForEditing()
        => Shift.Create
                (
                    1,
                    ShiftName.Create("Day Shift"),
                    8,
                    0,
                    17,
                    0
                );
    }
}