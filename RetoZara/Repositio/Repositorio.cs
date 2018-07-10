using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace RetoZara
{
    internal class Repositorio : IRepositorio
    {
        private List<Data> dateList;

        public decimal SetList(string path)
        {
            using (var reader = new StreamReader(path))
            {
                dateList = new List<Data>();
                DateTime tDate;
                NumberFormatInfo format = new NumberFormatInfo() { NumberDecimalSeparator = "." };
                string newDate, line;
                decimal openValue, closeValue, total = 0;

                while (null != (line = reader.ReadLine()))
                {
                    var values = line.Split(';');
                    var month = values[0].Split('-');

                    month[1] = Manager.GetMonth(month[1]).ToString();
                    newDate = month[0] + "-" + month[1] + "-" + month[2];

                    openValue = Convert.ToDecimal(values[1], format);
                    closeValue = Convert.ToDecimal(values[2], format);

                    tDate = DateTime.Parse(newDate, CultureInfo.GetCultureInfo("es"));
                    dateList.Add(new Data(tDate, openValue, closeValue));
                }
                DateTime datl = DateTime.Parse("28/12/17", CultureInfo.GetCultureInfo("es"));
                Data dtl = dateList.Find(x => x.Date == datl);
                List<Data> justFriday = Manager.GetActions(dateList);
                total = Manager.getTotal(justFriday, exactDay: dtl.Closed);
                return total;
            }
        }
    }
}
