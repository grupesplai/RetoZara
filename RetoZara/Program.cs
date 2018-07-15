using System;
using System.Configuration;

namespace RetoZara
{
    class Program
    {
        static void Main(string[] args)
        { 
            string path = ConfigurationManager.AppSettings["path"];
            Repositorio zara = new Repositorio();
            decimal total = zara.SetList(path);
            Console.WriteLine(total.ToString("0,000.000"));
            
        }
    }
}
