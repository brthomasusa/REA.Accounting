#pragma warning disable CS8600

using Xunit;
using REA.Accounting.Core.HumanResources.ValueObjects;

namespace REA.Accounting.UnitTests.HumanResources
{
    public class HrValueObject_Tests
    {
        [Fact]
        public void ShiftName_Create_ShouldSucceed()
        {
            string name = "Day Shift";
            var exception = Record.Exception(() => ShiftName.Create(name));
            Assert.Null(exception);
        }

        [Fact]
        public void ShiftName_Create_Null_ShouldFail()
        {
            string name = null;
            var exception = Record.Exception(() => ShiftName.Create(name!));
            Assert.NotNull(exception);
        }

        [Fact]
        public void ShiftTime_Create_ShouldSucceed()
        {
            var exception = Record.Exception(() => ShiftTime.Create(0, 59));
            Assert.Null(exception);
        }

        [Fact]
        public void ShiftTime_Create_NegativeHour_ShouldFail()
        {
            var exception = Record.Exception(() => ShiftTime.Create(-1, 59));
            Assert.NotNull(exception);
        }

        [Fact]
        public void ShiftTime_Create_NegativeMinute_ShouldFail()
        {
            var exception = Record.Exception(() => ShiftTime.Create(11, -59));
            Assert.NotNull(exception);
        }

        [Fact]
        public void ShiftTime_Create_HourGreaterThan23_ShouldFail()
        {
            var exception = Record.Exception(() => ShiftTime.Create(24, 59));
            Assert.NotNull(exception);
        }

        [Fact]
        public void ShiftTime_Create_MinuteGreaterThan59_ShouldFail()
        {
            var exception = Record.Exception(() => ShiftTime.Create(2, 60));
            Assert.NotNull(exception);
        }
    }
}