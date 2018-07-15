using System;
using System.Collections.Generic;
using System.Globalization;

namespace RetoZara.FilesExcel
{
    class FileAccessDates
    {
        public static List<Data> GetDataFromDate(List<Data> resume, string start, string end)
        {
            List<Data> newList = new List<Data>();
            DateTime inicio = DateTime.Parse(start, CultureInfo.GetCultureInfo("es"));
            DateTime final = DateTime.Parse(end, CultureInfo.GetCultureInfo("es"));
            DateTime excep = DateTime.Parse("03/01/11", CultureInfo.GetCultureInfo("es"));
            foreach (var a in resume)
            {
                if (a.Date >= inicio && a.Date < final && a.Date != excep)
                {
                    try { newList.Add(new Data(a.Date, a.Closed, a.Opened)); }
                    catch { }
                }
            }
            return newList;
        }
    }
}
