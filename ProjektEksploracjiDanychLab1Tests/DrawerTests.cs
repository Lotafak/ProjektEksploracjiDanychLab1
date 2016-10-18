using System;
using System.Collections.Generic;
using System.Threading;
using Accord.Controls;
using Accord.Statistics.Visualizations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjektEksploracjiDanychLab1;

namespace ProjektEksploracjiDanychLab1Tests
{
    [TestClass()]
    public class DrawerTests
    {
        private SortedDictionary<double, int> sortedDictionary = new SortedDictionary<double, int>();
        private readonly double[] _histogramData = {1.0, 2.0, 3.0, 2.0, 4.0, 5.0, 4.0, 1.0, 10.0};

        [TestMethod()]
        public void DrawChartTest()
        {
            prepareData();
            Drawer drawer = new Drawer();
            drawer.DrawChart("jj.bmp", sortedDictionary);
            Assert.IsNotNull(drawer);
        }

        [TestMethod]
        public void AccordHistogramTest()
        {
            HistogramView histogramView = new HistogramView
            {
                NumberOfBins = 6,
                DataSource = _histogramData,
                AutoSize = true,
                BinWidth = 3
            };
            
            //HistogramBox.Show(histogramView.Histogram, "xd");
            HistogramBox.Show(_histogramData, "xd");
            //Thread.Sleep(10000); 
            Assert.IsTrue(true);
            Console.ReadLine();
        }

        //public void prepareHistogramData()
        //{
        //    double[] array = new[] {};
        //}

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