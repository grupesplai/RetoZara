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
        public static DateTime GetLastFriday(DateTime date, List<Data> datelist)
        {
            var dateFirts = new DateTime(date.Year, date.Month, 1).AddMonths(1);
            int vector = (((int)dateFirts.DayOfWeek + 1) % 7) + 1;

            DateTime add = dateFirts.AddDays(-vector);
            if (date != add)
            {
                if (!datelist.Exists(x => x.Date == add))
                {
                    add = add.AddDays(1);
                }
            }

            return add;
        }
        public static List<Data> GetActions(List<Data> dateList)
        {
            dateList.Reverse();
            var lastDay = (from d in dateList
                           where (d.Date != GetLastFriday(d.Date,dateList) )==false
                           select d).ToList();
            
            //var result = dateList.Union(lastDay).Intersect(dateList.Intersect(lastDay)).ToList();
            foreach (var ld in lastDay)
            {
                Console.WriteLine(ld.Date + "\t" + ld.Closed);
            }
            //DateTime dateTemp, dateFirts;
            //int vector;
            //List<Data> justFridays = new List<Data>();

            //var result = dateList.Where(x => x.Date == GetLastFriday(x.Date));
            //foreach(var re in result)
            //{
            //    Console.WriteLine(re.Date);
            //}

            //foreach (Data dl in dateList)
            //{
            //    dateFirts = new DateTime(dl.Date.Year, dl.Date.Month, 1).AddMonths(1);
            //    vector = (((int)dateFirts.DayOfWeek + 1) % 7) + 1;
            //    dateTemp = dateFirts.AddDays(-vector);

            //    //var result = dateList.Where(x => x.Date == dateTemp);

            //    //if (result.Any())
            //    //{
            //        justFridays.Add(new Data(dl.Date, dl.Closed, dl.Opened));
            //        Console.WriteLine(dateTemp);
            //    //}
            //}
            return lastDay;
        }
        public static decimal getTotal(List<Data> lastDay, decimal exactDay)
        {
            decimal total = 0;
            foreach (Data ld in lastDay)
            {
                total = Decimal.Round(total + ((50-(50*2/100)) / ld.Opened), 3);
            }
            total = exactDay * total;
            return total;
        }
    }
}
