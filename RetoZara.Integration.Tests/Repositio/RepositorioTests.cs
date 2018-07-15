using Microsoft.VisualStudio.TestTools.UnitTesting;
using RetoZara;
using System;
using System.Configuration;
using System.Globalization;

namespace RetoZara.Tests
{
    [TestClass()]
    public class RepositorioTests
    {
        string path = ConfigurationManager.AppSettings["path"];
        Repositorio repositorio = new Repositorio();

        DateTime inicio = DateTime.Parse("01/01/17", CultureInfo.GetCultureInfo("es"));
        DateTime final = DateTime.Parse("28/12/17", CultureInfo.GetCultureInfo("es"));

       [TestMethod()]
        public void SetListTest(DateTime ini, DateTime fin)
        {
            decimal total = repositorio.SetList(path);
            Assert.IsTrue();
        }
    }
}