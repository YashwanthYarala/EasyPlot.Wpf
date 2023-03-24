using System.Windows.Media;
using System.Windows.Shapes;

namespace EasyPlot
{
    public class DataSeries
    {
        public Polyline LineSeries = new Polyline();
        public Brush LineColor { get; set; }
        public double LineThickness { get; set; } = 1;
        public string SeriesName { get; set; } = "DefaultName";
        public LinePatternEnum LinePattern { get; set; }
        public Symbols Symbols { get; set; }

        public DataSeries()
        {
            LineColor = Brushes.Black;
            Symbols = new Symbols();

        }
        public void AddLinePattern()
        {
            LineSeries.Stroke = LineColor;
            LineSeries.StrokeThickness = LineThickness;
            switch (LinePattern)
            {
                case LinePatternEnum.Dash:
                    LineSeries.StrokeDashArray =
                    new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case LinePatternEnum.Dot:
                    LineSeries.StrokeDashArray =
                    new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case LinePatternEnum.DashDot:
                    LineSeries.StrokeDashArray =
                    new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
                case LinePatternEnum.None:
                    LineSeries.Stroke = Brushes.Transparent;
                    break;
            }
        }

        public enum LinePatternEnum
        {
            Solid = 1,
            Dash = 2,
            Dot = 3,
            DashDot = 4,
            None = 5
        }
    }


}


