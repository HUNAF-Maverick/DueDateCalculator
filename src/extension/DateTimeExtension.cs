using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DueDateCalculator.src.extension
{
    public static class DateTimeExtension
    {
        public static DateTime AddWorkDays(this DateTime dateTime, int days)
        {
            for (int i = 0; i < days; i++)
            {
                if (dateTime.DayOfWeek.Equals(DayOfWeek.Friday))
                {
                    dateTime = dateTime.AddDays(3);
                } else 
                {
                    dateTime = dateTime.AddDays(1);
                }

            }

            return dateTime;
        }
    }
}
