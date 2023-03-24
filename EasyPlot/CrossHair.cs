using System.Windows.Media;
using System.Windows.Shapes;
using static EasyPlot.DataSeries;

namespace EasyPlot
{


    public class CrossHair
    {
        private double thickness { get; set; } = 0.5;
        public double Thickness { get { return thickness; } set { thickness = value; } }

        private Brush color { get; set; } = Brushes.Red;
        public Brush Color { get { return color; } set { color = value; } }

        private DataSeries.LinePatternEnum linePattern { get; set; } = DataSeries.LinePatternEnum.Solid;
        public DataSeries.LinePatternEnum LinePattern { get { return linePattern; } set { linePattern = value; } }

        public Line Line = new Line();
        public CrossHair()
        {


            Line = new Line();


        }
        public void AddLinePattern()
        {
            Line.Stroke = color;
            Line.StrokeThickness = thickness;
            switch (linePattern)
            {
                case LinePatternEnum.Dash:
                    Line.StrokeDashArray =
                    new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case LinePatternEnum.Dot:
                    Line.StrokeDashArray =
                    new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case LinePatternEnum.DashDot:
                    Line.StrokeDashArray =
                    new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
                case LinePatternEnum.None:
                    Line.Stroke = Brushes.Transparent;
                    break;
            }
        }
    }


}
