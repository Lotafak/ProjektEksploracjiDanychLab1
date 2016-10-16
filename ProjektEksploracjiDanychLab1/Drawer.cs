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
            //try
            //{
            var bitmap = new Bitmap(1024, 768);

            using ( var graphics = Graphics.FromImage(bitmap) )
            {
                graphics.Clear(Color.White);
                DrawAxes(graphics);
                PrintLabels(graphics, histogramData);
                //    foreach ( var node in DAL.Instance.Nodes )
                //    {
                //        graphics.FillEllipse(new SolidBrush(Color.Red), new RectangleF(new PointF(node.X, node.Y), new SizeF(17.0f, 15.0f)));
                //    }
                //    for ( int i = 0; i < histogramData.Count - 1; i++ )
                //    {
                //        var point1 = new PointF(histogramData[i].X, histogramData[i].Y);
                //        var point2 = new PointF(histogramData[i + 1].X, histogramData[i + 1].Y);
                //        graphics.FillEllipse(new SolidBrush(Color.Black), new RectangleF(point1, new SizeF(10.0f, 10.0f)));
                //        graphics.FillEllipse(new SolidBrush(Color.Black), new RectangleF(point2, new SizeF(10.0f, 10.0f)));
                //        graphics.DrawLine(Pens.Blue, point1, point2);
                //    }

                //    graphics.DrawString("1", new Font("Tahoma", 48), Brushes.Black, histogramData[0].X + 20, histogramData[0].Y);
                //    var firstPoint = new PointF(histogramData[0].X, histogramData[0].Y);
                //    var lastPoint = new PointF(histogramData[histogramData.Count - 1].X, histogramData[histogramData.Count - 1].Y);
                //    graphics.DrawLine(Pens.Blue, firstPoint, lastPoint);

                //    foreach ( var node in histogramData )
                //    {
                //        var lastNode = new PointF(node.X, node.Y);
                //        graphics.FillEllipse(new SolidBrush(Color.Black), new RectangleF(new PointF(node.X, node.Y), new SizeF(10.0f, 10.0f)));
                //    }
                //}

                if ( System.IO.File.Exists($"{filename}.bmp") )
                    System.IO.File.Delete($"{filename}.bmp");

                bitmap.Save($"{filename}.bmp");
                bitmap.Dispose();
            }
            //}
            //catch ( Exception ex )
            //{
            //    Console.WriteLine("Conversion failed: {0}", ex.Message);
            //}
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
            var scale = this.scale(yMin, yMax);

            if ( xCount > 50 ) return;

            graphics.DrawString(yMax.ToString(CultureInfo.InvariantCulture), new Font("Tahoma", 10), Brushes.Black, 10.0f, 31.0f);

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

        private double scale( int min, int max )
        {
            if ( min != max )
                return Math.Round((double)700 / ( max - min + 1 ), 5);
            return Math.Round((double)700 / max, 5);
        }
    }
}
