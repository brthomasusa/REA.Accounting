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

        [Fact]
        public void NationalID_Create_5Digits_ShouldSucceed()
        {
            var exception = Record.Exception(() => NationalID.Create("12345"));
            Assert.Null(exception);
        }

        [Fact]
        public void NationalID_Create_8Digits_ShouldSucceed()
        {
            var exception = Record.Exception(() => NationalID.Create("12345678"));
            Assert.Null(exception);
        }

        [Fact]
        public void NationalID_Create_9Digits_ShouldSucceed()
        {
            var exception = Record.Exception(() => NationalID.Create("123456789"));
            Assert.Null(exception);
        }

        [Fact]
        public void NationalID_Create_TooFewDigits_ShouldSucceed()
        {
            var exception = Record.Exception(() => NationalID.Create("1234"));
            Assert.NotNull(exception);
        }

        [Fact]
        public void NationalID_Create_TooManyDigits_ShouldSucceed()
        {
            var exception = Record.Exception(() => NationalID.Create("1234567890"));
            Assert.NotNull(exception);
        }

        [Fact]
        public void NationalID_Create_ShouldSucceed()
        {
            var exception = Record.Exception(() => NationalID.Create("12345678P"));
            Assert.NotNull(exception);
        }

        [Fact]
        public void DateOfBirth_Create__ShouldSucceed()
        {
            var exception = Record.Exception(() => DateOfBirth.Create(new DateOnly(2004, 12, 3)));
            Assert.Null(exception);
        }

        [Fact]
        public void DateOfBirth_Create__TooYoung_ShouldFail()
        {
            var exception = Record.Exception(() => DateOfBirth.Create(new DateOnly(2004, 12, 31)));
            Assert.NotNull(exception);
        }

        [Fact]
        public void DateOfBirth_Create__TooOld_ShouldSucceed()
        {
            var exception = Record.Exception(() => DateOfBirth.Create(new DateOnly(1929, 12, 31)));
            Assert.NotNull(exception);
        }

        [Fact]
        public void DateOfHire_Create_ShouldSucceed()
        {
            var exception = Record.Exception(() => DateOfHire.Create(new DateOnly(2004, 12, 3)));
            Assert.Null(exception);
        }

        [Fact]
        public void DateOfHire_Create_Before_071996_ShouldFail()
        {
            var exception = Record.Exception(() => DateOfHire.Create(new DateOnly(1996, 6, 30)));
            Assert.NotNull(exception);
        }

        [Fact]
        public void DateOfHire_Create_TooFarIntoFuture_ShouldFail()
        {
            var exception = Record.Exception(() => DateOfHire.Create(new DateOnly(2022, 12, 31)));
            Assert.NotNull(exception);
        }

        [Fact]
        public void SickLeave_Create_ShouldSucceed()
        {
            var exception = Record.Exception(() => SickLeave.Create(0));
            Assert.Null(exception);
        }

        [Fact]
        public void SickLeave_Create_TooLow_ShouldFail()
        {
            var exception = Record.Exception(() => SickLeave.Create(-1));
            Assert.NotNull(exception);
        }

        [Fact]
        public void SickLeave_Create_TooHigh_ShouldFail()
        {
            var exception = Record.Exception(() => SickLeave.Create(121));
            Assert.NotNull(exception);
        }

        [Fact]
        public void Vacation_Create_ShouldSucceed()
        {
            var exception = Record.Exception(() => Vacation.Create(0));
            Assert.Null(exception);
        }

        [Fact]
        public void Vacation_Create_TooLow_ShouldFail()
        {
            var exception = Record.Exception(() => Vacation.Create(-41));
            Assert.NotNull(exception);
        }

        [Fact]
        public void Vacation_Create_TooHigh_ShouldFail()
        {
            var exception = Record.Exception(() => Vacation.Create(241));
            Assert.NotNull(exception);
        }
    }
}