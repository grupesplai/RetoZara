using System;
using System.Collections.Generic;
using System.Linq;

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

            DateTime add = dateFirts.AddDays(-vector);
            return add;
        }

        public static List<Data> GetActions(List<Data> dateList)
        {
            List<Data> cotizationsDayList = new List<Data>();

            foreach (var d in dateList)
            {
                DateTime lastCotizationDay = new DateTime();
                if (GetLastFriday(d.Date).Equals(d.Date)) lastCotizationDay = d.Date;

                else
                {
                    while (!GetLastFriday(d.Date).Equals(d.Date))
                    {
                        d.Date = d.Date.AddDays(1);
                    }
                    lastCotizationDay = d.Date;
                }
                if (!(cotizationsDayList.Exists(x => x.Date == lastCotizationDay)))
                {
                    cotizationsDayList.Add(new Data(d.Date, d.Closed, d.Opened));
                    
                }
            }
            //var lastDay = (from d in dateList
            //               where (d.Date != GetLastFriday(d.Date,dateList) )==false
            //               select d).ToList();
            
            return cotizationsDayList;
        }

        public static decimal GetTotal(List<Data> cotizationsDayList, decimal exactDay)
        {
            cotizationsDayList.Reverse();
            decimal total = 0;
            foreach (Data ld in cotizationsDayList)
            {
                total = Decimal.Round(total + ((50-(50*2/100)) / ld.Opened), 3);
            }
            total = exactDay * total;
            return total;
        }
    }
}
