using System;
using WeekNotifier.Models;
using Richter.Common.Utilities.Extensions;
using Xunit;

namespace WeekNotifier.Tests
{
    public class CalendarTests
    {
        [Fact]
        public void CanCreateDefaultCalendarTest()
        {
            var cal = Calendar.CreateInstance();

            Assert.Equal(DateTime.Today.ISO8601WeekOfYear(), cal.WeekNumber);
        }

        [Fact]
        public void WeekNumberOutOfRangeExceptionTest()
        {
            // Arrange

            // Act - local function
            void Act() => Calendar.CreateInstance(999);

            // Assert
            var caughtException = Assert.Throws<ArgumentOutOfRangeException>(Act);
            Assert.Equal("Week number cannot exceed 2 digits. (Parameter 'value')", caughtException.Message);

        }
    }
}
