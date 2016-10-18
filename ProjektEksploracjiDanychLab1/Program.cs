using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using Accord.Controls;
using Accord.Statistics.Visualizations;

namespace ProjektEksploracjiDanychLab1
{
    class Program
    {
        static void Main()
        {
            //List<string[]> examples = new List<string[]>();
            List<List<double>> examples = new List<List<double>>();

            using ( StreamReader sr = new StreamReader("X_train.csv") )
            {
                while ( !sr.EndOfStream )
                {
                    var readLine = sr.ReadLine();
                    if ( readLine == null ) break;

                    var listOfStr = readLine.Split(',').ToList();

                    var tempList = new List<double>();
                    listOfStr.ForEach(strVal =>
                        tempList.Add(double.Parse(strVal, CultureInfo.InvariantCulture)));
                    examples.Add(tempList);
                }
            }

            Console.WriteLine($"Liczba przykładów: {examples.Count}");
            Console.WriteLine($"Liczba cech: {examples.First().Count}");

            var nonZeroFeatures = 0;

            //var features = new List<Feature>();
            //for ( var i = 0; i < examples.First().Count; i++ )
            //{
            //    features.Add(new Feature());
            //}

            //foreach ( var example in examples )
            //{
            //    for ( var i = 0; i < example.Count; i++ )
            //    {
            //        var featureValue = Convert.ToDouble(example[i], CultureInfo.InvariantCulture);
            //        var feature = features[i];


            //        if ( featureValue > 0 )
            //        {
            //            nonZeroFeatures++;
            //        }

            //        feature.Sum += featureValue;
            //        feature.Count++;

            //        var uniqueValues = feature.UniqueValues;
            //        if ( uniqueValues.ContainsKey(featureValue) )
            //            uniqueValues[featureValue]++;
            //        else
            //            uniqueValues.Add(featureValue, 1);

            //        feature.Values.Add(featureValue);

            //    }
            //}

            //StreamWriter sw = new StreamWriter("data.txt");
            //var drawer = new Drawer();
            //var name = 0;
            //foreach ( var feature in features )
            //{
            //    feature.CalculateMean();
            //    feature.CalculateVariance();
            //    drawer.DrawChart(name++.ToString(), feature.UniqueValues);
            //    sw.WriteLine($"{name} \nMean: {feature.Mean}\nVariance: {feature.Variance}\nUnique values: {feature.UniqueValues.Count}");
            //    sw.WriteLine();
            //}
            var features = new List<List<double>>();

            foreach ( var t in examples.First() )
            {
                features.Add(new List<double>());
            }

            foreach ( var example in examples )
            {
                for ( var i = 0; i < example.Count; i++ )
                {
                    features[i].Add(example[i]);
                }
            }

            Console.WriteLine($"Liczba niezerowych cech: {nonZeroFeatures:N0}");
            //Histogram histogram = new Histogram();
            //histogram.FromData(features[1].ToArray());
            //histogram.AutoAdjustmentRule = BinAdjustmentRule.None;

            //Histogram histogram = new Histogram().(features.First().UniqueValues);
            HistogramView h = new HistogramView();

            HistogramBox histogram = HistogramBox.Show(features[1].ToArray(), "xd");
            //Bitmap b = new Bitmap(histogram.Width, histogram.Height, g);
            //h.DrawToBitmap(b, new Rectangle(0,0,1000,1000));
            //b.Save("histogram.bmp");

            Console.ReadLine();
        }
    }
}