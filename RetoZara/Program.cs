﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace RetoZara
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\G1\source\repos\RetoZara\RetoZara\stocks-ITX.csv";
            Repositorio zara = new Repositorio();
            zara.SetList(path);
        }
    }
}
