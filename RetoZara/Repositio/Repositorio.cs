using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoZara
{
    class Repositorio : IRepositorio
    {

        public void SetList(string path)
        {
            using (var reader = new StreamReader(path))
            {
                List<Data> dateList = new List<Data>();
                DateTime tDate;
                NumberFormatInfo format = new NumberFormatInfo() { NumberDecimalSeparator = "." };
                string newDate, line;
                decimal total = 0, openValue, closeValue;

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
                dateList.Reverse();
                List<Data> lastDay = (from d in dateList
                                      where d.Date == Manager.GetLastFriday(d.Date)
                                      select d).ToList();

                Data open = new Data();
                foreach (Data a in lastDay)
                {
                    Console.WriteLine(a.Date + "\t" + a.Closed + "\t" + a.Opened);
                    total = Decimal.Round(total + (50 / a.Opened), 3);
                    Console.WriteLine(total);

                }
            }
        }
    }
}
