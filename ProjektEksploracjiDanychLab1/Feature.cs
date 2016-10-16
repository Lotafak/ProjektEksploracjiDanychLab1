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
        public int Count { get; set; }
        public double Sum { get; set; }
        public double Mean { get; set; }
        public double Variance { get; set; }
        public SortedDictionary<double, int> UniqueValues { get; set; } = new SortedDictionary<double, int>();
        public List<double> Values { get; set; } = new List<double>();

        public void CalculateMean()
        {
            Mean = Sum/Count;
        }

        public void CalculateVariance()
        {
            foreach (var value in Values)
            {
                Variance += Math.Pow(value - Mean, 2);
            }

            Variance = Variance/Count;
        }
    }
}
