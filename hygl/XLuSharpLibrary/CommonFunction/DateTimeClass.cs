using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XLuSharpLibrary.CommonFunction
{
   public class DateTimeClass
    {
        public static DateTime GetMonthFirstDay(DateTime today)
        {
            return today.AddDays((double)(-(double)today.Day + 1));
        }

        public static int GetMonthMaxDay(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }

        public static string GetWeekText(DateTime date)
        {
            string[] array = new string[]
            {
                "星期日",
                "星期一",
                "星期二",
                "星期三",
                "星期四",
                "星期五",
                "星期六"
            };
            return array[(int)Convert.ToInt16(date.DayOfWeek)];
        }

        public static bool DateCompare(DateTime dtstart, DateTime dtover)
        {
            return !(dtstart > dtover);
        }
    }
}
