using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DueDateCalculator.test
{
    public class DueDateTester
    {
        protected readonly DueDateCalculator calculator = new DueDateCalculator();

        [Fact]
        public void testInvalidSubmitTimeBefore()
        {
            Assert.Throws<InvalidDataException>(() => calculator.CalculateDueDate(DateTime.Parse("2023-01-02 08:59"), 1));
        }

        [Fact]
        public void testInvalidSubmitTimeAfter()
        {
            Assert.Throws<InvalidDataException>(() => calculator.CalculateDueDate(DateTime.Parse("2023-01-02 17:01"), 1));
        }

        [Fact]
        public void testInvalidTurnaroundTime()
        {
            Assert.Throws<InvalidDataException>(() => calculator.CalculateDueDate(new DateTime(), -1));
        }

        [Fact]
        public void testInvalidSubmitDateWeekend()
        {
            Assert.Throws<InvalidDataException>(() => calculator.CalculateDueDate(DateTime.Parse("2023-01-01 10:00"), 2));
        }

        [Fact]
        public void testDueTimeOnSameDay()
        {
            Assert.Equal(DateTime.Parse("2023-01-02 16:00"), calculator.CalculateDueDate(DateTime.Parse("2023-01-02 10:00"), 6));
        }

        [Fact]
        public void testDueTimeOnAnotherDay()
        {
            Assert.Equal(DateTime.Parse("2023-01-03 10:00"), calculator.CalculateDueDate(DateTime.Parse("2023-01-02 15:00"), 3));
            Assert.Equal(DateTime.Parse("2023-01-03 17:00"), calculator.CalculateDueDate(DateTime.Parse("2023-01-02 15:00"), 10));
            Assert.Equal(DateTime.Parse("2023-01-04 12:13"), calculator.CalculateDueDate(DateTime.Parse("2023-01-02 15:13"), 13));
        }

        [Fact]
        public void testDueTimeOnAnotherDayWithMinutes()
        {
            Assert.Equal(DateTime.Parse("2023-01-03 10:10"), calculator.CalculateDueDate(DateTime.Parse("2023-01-02 15:10"), 3));
            Assert.Equal(DateTime.Parse("2023-01-03 09:20"), calculator.CalculateDueDate(DateTime.Parse("2023-01-02 15:20"), 2));
        }

        [Fact]
        public void testDueTimeWeekend()
        {
            Assert.Equal(DateTime.Parse("2023-01-09 10:30"), calculator.CalculateDueDate(DateTime.Parse("2023-01-06 16:30"), 2));
        }
    }
}
