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
                

                while (null != (line = reader.ReadLine()))
                {
                    var values = line.Split(';');
                    dateList.Add(new Data(tDate, values[1], values[2]));
                }
                
            }
        }
    }
}
