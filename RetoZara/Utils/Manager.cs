using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoZara
{
    internal class Manager
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
        public static List<Data> GetActions(List<Data> dateList)
        {
            dateList.Reverse();
            List<Data> lastDay = (from d in dateList
                                  where d.Date == GetLastFriday(d.Date)
                                  select d).ToList();
            return lastDay;
        }
        public static decimal getTotal(List<Data> lastDay)
        {
            decimal total = 0;
            foreach (Data ld in lastDay)
            {
                //Console.WriteLine(ld.Date + "\t" + ld.Closed + "\t" + ld.Opened);
                total = Decimal.Round(total + ((50-(50*2/100)) / ld.Opened), 3);
            }
            total = lastDay.Last().Closed * total;
            return total;
        }
    }
}
