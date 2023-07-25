using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DueDateCalculator.src.validator
{
    public class DueDateValidator
    {
        public TimeSpan workingDayStart { get; set; }

        public TimeSpan workingDayEnd { get; set; }

        public DueDateValidator(TimeSpan workingDayStart, TimeSpan workingDayEnd) 
        {
            this.workingDayStart = workingDayStart;
            this.workingDayEnd = workingDayEnd;
        }

        public void validateParams(DateTime submitDate, int turnaroundTime)
        {
            validateSubmitDate(submitDate);
            validateTime(turnaroundTime);
        }

        protected void validateSubmitDate(DateTime submitDate)
        {
            if (submitDate.DayOfWeek.Equals(DayOfWeek.Saturday) || submitDate.DayOfWeek.Equals(DayOfWeek.Sunday))
            {
                throw new InvalidDataException("Submit date must not be on Saturday or Sunday!");
            }


            int startResult = TimeSpan.Compare(submitDate.TimeOfDay, workingDayStart);
            int endResult = TimeSpan.Compare(submitDate.TimeOfDay, workingDayEnd);

            if (startResult < 0 || endResult > 0)
            {
                throw new InvalidDataException("Submit time must be between 9AM and 5 PM!");
            }
        }

        protected void validateTime(int turnaroundTime)
        {
            if (turnaroundTime < 0)
            {
                throw new InvalidDataException("Turnaround time must not be less than 0!");
            }
        }
    }
}
