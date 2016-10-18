using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjektEksploracjiDanychLab1;

namespace ProjektEksploracjiDanychLab1Tests
{
    [TestClass()]
    public class DrawerTests
    {
        private SortedDictionary<double, int> sortedDictionary = new SortedDictionary<double, int>();

        [TestMethod()]
        public void DrawChartTest()
        {
            prepareData();
            Drawer drawer = new Drawer();
            drawer.DrawChart("jj.bmp", sortedDictionary);
            Assert.IsNotNull(drawer);
        }

        private void prepareData()
        {
            Random random = new Random();
            //sortedDictionary.Add(0,2);
            //sortedDictionary.Add(1,1);
            //sortedDictionary.Add(2,1);
            //sortedDictionary.Add(3,4);
            //sortedDictionary.Add(7,1);
            //sortedDictionary.Add(8,1);
            //sortedDictionary.Add(9,1);
            //sortedDictionary.Add(10,1);
            //sortedDictionary.Add(11,1);
            //sortedDictionary.Add(12,1);
            //sortedDictionary.Add(13,100);
            //sortedDictionary.Add(15,1);
            //sortedDictionary.Add(17,1);
            //sortedDictionary.Add(21,1);
            for (var i = 0; i < 80; i++)
            {
                sortedDictionary.Add(i, random.Next(0,25) );
            }
        }
    }
}