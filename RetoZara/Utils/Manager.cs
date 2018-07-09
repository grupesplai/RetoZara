using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoZara
{
    public class Manager
    {
        public static int GetMonth(string month)
        {
            string[] monthsArray = {"ene","feb","mar","abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic" };
            int index = Array.IndexOf(monthsArray, month);
            return index + 1;
        }
        public static DateTime GetLastFriday(DateTime date)
        {
            var dateFirts = new DateTime(date.Year, date.Month, 1).AddMonths(1);
            int vector = (((int)dateFirts.DayOfWeek + 1) % 7) + 1;
            return dateFirts.AddDays(-vector);
        }
    }
}
