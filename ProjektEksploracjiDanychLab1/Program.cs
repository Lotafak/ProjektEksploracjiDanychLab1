using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ProjektEksploracjiDanychLab1
{
    class Program
    {
        static void Main()
        {
            List<string[]> examples = new List<string[]>();

            using ( StreamReader sr = new StreamReader("X_train.csv") )
            {
                while ( !sr.EndOfStream )
                {
                    var readLine = sr.ReadLine();
                    if ( readLine != null )
                        examples.Add(readLine.Split(','));
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

            foreach ( var example in examples )
            {
                for ( var i = 0; i < example.Length; i++ )
                {
                    var featureValue = Convert.ToDouble(example[i], CultureInfo.InvariantCulture);
                    var feature = features[i];
                    

                    if ( featureValue > 0 )
                    {
                        nonZeroFeatures++;
                    }

                    feature.Sum += featureValue;
                    feature.Count++;

                    var uniqueValues = feature.UniqueValues;
                    if ( uniqueValues.ContainsKey(featureValue) )
                        uniqueValues[featureValue]++;
                    else
                        uniqueValues.Add(featureValue, 1);

                    feature.Values.Add(featureValue);

                }
            }

            StreamWriter sw = new StreamWriter("data.txt");
            var drawer = new Drawer();
            var name = 0;
            foreach ( var feature in features )
            {
                feature.CalculateMean();
                feature.CalculateVariance();
                drawer.DrawChart(name++.ToString(), feature.UniqueValues);
                sw.WriteLine($"{name} \nMean: {feature.Mean}\nVariance: {feature.Variance}\nUnique values: {feature.UniqueValues.Count}");
                sw.WriteLine();
            }

            Console.WriteLine($"Liczba niezerowych cech: {nonZeroFeatures:N0}");

            Console.ReadLine();
        }
    }
}
