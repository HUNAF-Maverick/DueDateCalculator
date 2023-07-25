using DueDateCalculator.src.extension;
using DueDateCalculator.src.validator;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DueDateCalculator
{
    public class DueDateCalculator
    {
        protected readonly TimeSpan WORKING_DAY_START = new TimeSpan(9, 0, 0);

        protected readonly TimeSpan WORKING_DAY_END = new TimeSpan(17, 0, 0);

        public DateTime CalculateDueDate(DateTime submitDate, int turnaroundTime)
        {
            DueDateValidator validator = new DueDateValidator(WORKING_DAY_START, WORKING_DAY_END);

            validator.validateParams(submitDate, turnaroundTime);

            DateTime dueDate = addTimeShifted(submitDate, turnaroundTime, WORKING_DAY_START, WORKING_DAY_END);


            return dueDate;
        }


        protected DateTime addTimeShifted(DateTime date, int hoursToAdd, TimeSpan shiftStart, TimeSpan shiftEnd)
        {
            int workingHours = shiftEnd.Subtract(shiftStart).Hours;

            int shiftedDays = hoursToAdd / workingHours;
            int shiftedHours = hoursToAdd % workingHours;
            if (shiftedDays > 0)
            {
                date = date.AddWorkDays(shiftedDays);
            }

            if (shiftedHours > 0)
            {
                return addHoursShifted(date, shiftedHours, shiftStart, shiftEnd);
            }

            return date;
        }

        protected DateTime addHoursShifted(DateTime date, int hoursToAdd, TimeSpan shiftStart, TimeSpan shiftEnd)
        {
            DateTime dateAfterAdd = date.AddHours(hoursToAdd);
            int needDayShiftByTime = TimeSpan.Compare(dateAfterAdd.TimeOfDay, shiftEnd);

            if (needDayShiftByTime > 0)
            {
                DateTime dueDate = date.Date;
                int shiftedHours = dateAfterAdd.TimeOfDay.Hours - shiftEnd.Hours + shiftStart.Hours;
                return dueDate.AddWorkDays(1).AddHours(shiftedHours).AddMinutes(date.TimeOfDay.Minutes);
            }

            return dateAfterAdd;
        }
    }
}
