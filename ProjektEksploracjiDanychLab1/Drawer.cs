using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace ProjektEksploracjiDanychLab1
{
    public class Drawer
    {
        public void DrawChart( string filename, SortedDictionary<double, int> histogramData )
        {
            var bitmap = new Bitmap(1024, 768);

            using ( var graphics = Graphics.FromImage(bitmap) )
            {
                graphics.Clear(Color.White);
                DrawAxes(graphics);
                PrintLabels(graphics, histogramData);

                if ( System.IO.File.Exists($"{filename}.bmp") )
                    System.IO.File.Delete($"{filename}.bmp");

                bitmap.Save($"{filename}.bmp");
                bitmap.Dispose();
            }
        }

        private void DrawAxes( Graphics graphics )
        {
            graphics.DrawLine(Pens.Black, 30.0f, 20.0f, 30.0f, 740.0f);
            graphics.DrawLine(Pens.Black, 30.0f, 740.0f, 1000.0f, 740.0f);
        }

        private void PrintLabels( Graphics graphics, SortedDictionary<double, int> histogramData )
        {
            var stringFont = new Font("Tahoma", 6);
            var xCount = histogramData.Keys.Count;
            var yCount = histogramData.Values.Count;

            var xMin = histogramData.Keys.Min();
            var xMax = histogramData.Keys.Max();
            var yMin = histogramData.Values.Min();
            var yMax = histogramData.Values.Max();

            var xFactor = (double)950 / xCount;
            var scale = Scale(yMax);

            var lastXPosition = 0;

            graphics.DrawString(yMax.ToString(CultureInfo.InvariantCulture), new Font("Tahoma", 10), Brushes.Black, 10.0f, 31.0f);

            if (xCount > 50)
            {
                var counter = 0;
                foreach ( var key in histogramData.Keys )
                {
                    var x = Convert.ToInt32(counter * xFactor + 35);
                    lastXPosition = x;

                    var width = 740 / ( 2 * xCount );
                    if (width < 2)
                        width = 2;

                    var height = (int)( scale * histogramData[key] );
                    if ( height == 0 )
                        height = 1;

                    var y = 740 - height;

                    

                    graphics.DrawRectangle(Pens.Black,
                               new Rectangle(x,
                               y,
                               width,
                               height));
                    counter++;
                }

                graphics.DrawString(Math.Round(histogramData.Keys.Last(), 3).ToString(CultureInfo.InvariantCulture), stringFont, Brushes.Black, lastXPosition, 745.0f);
                return;
            }

            var count = 0;
            foreach ( var key in histogramData.Keys )
            {
                var x = Convert.ToInt32(count * xFactor + 50);
                var width = 740 / ( 2 * xCount );
                var height = (int)( scale * histogramData[key] );
                if ( height == 0 )
                    height = 1;
                var y = 740 - height;

                graphics.DrawString(Math.Round(key, 2).ToString(CultureInfo.InvariantCulture), stringFont, Brushes.Black, (float)( count * xFactor + (double)width/2 + 47 ), 745.0f);

                graphics.DrawRectangle(Pens.Black,
                           new Rectangle(x,
                           y,
                           width,
                           height));
                count++;
            }
        }

        private static double Scale( int max )
        {
            return (double)700 / max;
        }
    }
}
