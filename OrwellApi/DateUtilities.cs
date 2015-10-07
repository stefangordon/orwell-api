using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using OrwellApi.Models;

namespace OrwellApi
{
    public static class DateUtilities
    {
        public static DateTime DateOfWeek(YearWeek yearWeek)
        {
            return DateOfWeek(yearWeek.Year, yearWeek.Week);
        }

        public static DateTime DateOfWeek(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(2);
        }

        public static int NumberOfWeek(DateTime date)
        {
            DateTimeFormatInfo formatInfo = DateTimeFormatInfo.CurrentInfo;
            Calendar calendar = formatInfo.Calendar;
            return calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday);
        }

        //Hack hack not right!
        public static YearWeek YearWeekFromDate(DateTime date)
        {
            return new YearWeek { Week = NumberOfWeek(date), Year = date.Year }; 
        }

        public static DateTime EndOfWeek(DateTime date)
        {
           return date.AddDays(6-(int)date.DayOfWeek);
        }
    }
}