using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;

namespace ProjektEksploracjiDanychLab1
{
    class Program
    {
        static void Main()
        {
            var examples = new List<double[]>();
            var timer = new Stopwatch();
            timer.Start();

            using ( var sr = new StreamReader("X_train.csv") )
            {
                while ( !sr.EndOfStream )
                {
                    var readLine = sr.ReadLine();
                    if (readLine == null) continue;

                    var temp = readLine.Split(',');
                    var doubleTemp = new double[temp.Length];
                    for(var i = 0; i < temp.Length; i++ )
                    {
                        doubleTemp[i] = double.Parse(temp[i], CultureInfo.InvariantCulture);
                    }
                    examples.Add(doubleTemp);
                }
            }

            Console.WriteLine($"Liczba przykładów: {examples.Count}");
            Console.WriteLine($"Liczba cech: {examples.First().Length}");

            var nonZeroFeatures = 0;

            var features = new List<Feature>();
            for ( var i = 0; i < examples.First().Length; i++ )
            {
                features.Add(new Feature());
            }

            var t0 = timer.ElapsedMilliseconds;

            foreach ( var example in examples )
            {
                for ( var i = 0; i < example.Length; i++ )
                {
                    var featureValue = example[i];
                    var feature = features[i];

                    if ( Math.Abs(featureValue) > 0.00000000001)
                    {
                        nonZeroFeatures++;
                    }
                    
                    feature.Count++;

                    var uniqueValues = feature.UniqueValues;
                    if ( uniqueValues.ContainsKey(featureValue) )
                        uniqueValues[featureValue]++;
                    else
                        uniqueValues.Add(featureValue, 1);

                    feature.Values.Add(featureValue);
                }
            }

            var sw = new StreamWriter("datax.txt");

            var dataRepresentationTime = timer.ElapsedMilliseconds - t0;

            sw.WriteLine($"Time of loading data to memory: {dataRepresentationTime}");
            
            var drawer = new Drawer();

            var globalRawMax = double.MinValue;
            var globalRawMin = double.MaxValue;
            foreach ( var feature in features )
            {
                feature.CalculateMean();
                feature.CalculateStandardDeviation();
                var max = feature.Values.Max();
                var min = feature.Values.Min();
                if ( globalRawMax < max )
                    globalRawMax = max;

                if ( globalRawMin > min )
                    globalRawMin = min;
                //drawer.DrawChart(name++.ToString(), feature.UniqueValues);
                //sw.WriteLine($"{name} \nMean: {feature.Mean}\nVariance: {feature.Variance}\nUnique values: {feature.UniqueValues.Count}");
                //sw.WriteLine();
            }

            sw.WriteLine($"Statistics calculation: {timer.ElapsedMilliseconds - dataRepresentationTime}");
            var featuresCopy = features.Select(feature => feature.Clone()).ToList();

            sw.WriteLine("Raw");
            sw.WriteLine($"Max: {features[0].Values.Max()}");
            sw.WriteLine($"Min: {features[0].Values.Min()}");
            sw.WriteLine($"Global max: {globalRawMax}");
            sw.WriteLine($"Global min: {globalRawMin}");
            sw.WriteLine($"Mean: {features[0].Mean}");
            sw.WriteLine($"Deviation: {features[0].Deviation}");
            sw.WriteLine();

            var startStandarizing = timer.ElapsedMilliseconds;

            var x = timer.ElapsedMilliseconds;
            var unused = features[200].Deviation;
            var y = timer.ElapsedMilliseconds - x;

            var globalStandarizedMax = double.MinValue;
            var globalStandarizedMin = double.MaxValue;
            foreach (var feature in features)
            {
                for ( var i = 0; i < feature.Values.Count; i++ )
                {
                    feature.Values[i] = ( feature.Values[i] - feature.Mean ) / feature.Deviation;
                }
                var max = feature.Values.Max();
                var min = feature.Values.Min();
                if (globalStandarizedMax < max)
                    globalStandarizedMax = max;

                if (globalStandarizedMin > min)
                    globalStandarizedMin = min;
            }
            
            features[0].CalculateMean();
            features[0].CalculateStandardDeviation();

            sw.WriteLine("Standarized");
            sw.WriteLine($"Max: {features[0].Values.Max()}");
            sw.WriteLine($"Min: {features[0].Values.Min()}");
            sw.WriteLine($"Global max: {globalStandarizedMax}");
            sw.WriteLine($"Global min: {globalStandarizedMin}");
            sw.WriteLine($"Mean: {features[0].Mean}");
            sw.WriteLine($"Deviation: {features[0].Deviation}");
            sw.WriteLine();


            var globalNormalizedMax = double.MinValue;
            var globalNormalizedMin = double.MaxValue;
            foreach (var feature in featuresCopy)
            {
                var max = feature.Values.Max();
                var min = feature.Values.Min();
                for ( var i = 0; i < feature.Values.Count; i++ )
                {
                    feature.Values[i] = ( feature.Values[i] - min ) / ( max - min);
                }
                max = feature.Values.Max();
                min = feature.Values.Min();

                if ( globalNormalizedMax < max )
                    globalNormalizedMax = max;

                if ( globalNormalizedMin > min )
                    globalNormalizedMin = min;
            }

            featuresCopy[0].CalculateMean();
            featuresCopy[0].CalculateStandardDeviation();
            
            sw.WriteLine("Normalized");
            sw.WriteLine($"Max: {featuresCopy[0].Values.Max()}");
            sw.WriteLine($"Min: {featuresCopy[0].Values.Min()}");
            sw.WriteLine($"Global max: {globalNormalizedMax}");
            sw.WriteLine($"Global min: {globalNormalizedMin}");
            sw.WriteLine($"Mean: {featuresCopy[0].Mean}");
            sw.WriteLine($"Deviation: {featuresCopy[0].Deviation}");
            
            sw.WriteLine($"Time of data standarization/normalization: {timer.ElapsedMilliseconds - startStandarizing}");

            var classList = new List<int>();
            using ( var sr = new StreamReader("y_train.csv") )
            {
                while ( !sr.EndOfStream )
                {
                    var readline = sr.ReadLine();
                    if ( readline == null ) continue;

                    classList.Add(int.Parse(readline));
                }
            }
            var ones = classList.FindAll(c => c == 1);
            sw.WriteLine($"Class \"1\": {ones.Count}");
            sw.WriteLine($"Class \"0\": {classList.Count - ones.Count}");
            sw.Close();

            Console.WriteLine($"Liczba niezerowych cech: {nonZeroFeatures:N0} ({(double)nonZeroFeatures / ( examples.Count * examples[0].Length ) * 100:N2}%)");

            Console.ReadLine();
        }
    }
}
