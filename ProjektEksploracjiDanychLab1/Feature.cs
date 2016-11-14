using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace ProjektEksploracjiDanychLab1
{
    class Feature
    {
        public Feature Clone()
        {
            return new Feature()
            {
                Count = Count,
                Sum = Sum,
                Values = new List<double>(Values)
            };
        }

        public int Count { get; set; }
        public double Sum { get; set; }
        public double Mean { get; set; }
        public double Deviation { get; set; }
        public SortedDictionary<double, int> UniqueValues { get; set; } = new SortedDictionary<double, int>();
        public List<double> Values { get; set; } = new List<double>();

        public void CalculateMean()
        {
            Sum = 0;
            foreach (var value in Values)
            {
                Sum += value;
            }
            Mean = Sum/Count;
        }

        public void CalculateStandardDeviation()
        {
            Deviation = 0;
            foreach (var value in Values)
            {
                Deviation += Math.Pow(value - Mean, 2);
            }

            Deviation = Math.Sqrt(Deviation/Count);
        }
    }
}
