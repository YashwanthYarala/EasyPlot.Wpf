using PlottingLibrary01;
using PlottingWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasyPlot
{
    /// <summary>
    /// Interaction logic for WpfPlot.xaml
    /// </summary>
    public partial class WpfPlot : UserControl
    {
        private Point startpoint = new Point();
        private Point endPoint = new Point();
        private ChartStyle cs { get; set; }
        private DataCollection dc { get; set; }
        private DataSeries ds { get; set; }
        private double xmin0 = 0;
        private double xmax0 = 10;
        private double ymin0 = -1.5;
        private double ymax0 = 1.5;
        public List<double> TimePoints { get; set; }
        public List<double> SignalPoints { get; set; }
        public double[] x1 { get; set; }
        public double[] y1 { get; set; }
        //
        private double xIncrement { get; set; } = 5;
        private double yIncrement { get; set; } = 0;
        public string Title_ { get; set; }
        private Shape rubberBand = null;
        public double[] xVal { get; set; }
        public double[] yVal { get; set; }
        private double thickness { get; set; } = 1;
        private PlotSeriesEnum plotSeries { get; set; }
        public WpfPlot()
        {
            InitializeComponent();
            // GenData genData = new GenData();

           
            cs = new ChartStyle();
            ds = new DataSeries();
            dc = new DataCollection();
            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
            cs.XMin = xmin0;
            cs.XMax = xmax0;
            cs.YMin = ymin0;
            cs.Ymax = ymax0;
        }
        public void AddGatePulse(double[] xvalues, double[] yvalues)
        {
            //series values to point conversion
            plotSeries = PlotSeriesEnum.PulseGate;

            SignalValueToPoints signalValueToPoints = new SignalValueToPoints();
            var k = signalValueToPoints.Convert(yvalues, xvalues);
            double[] y = k[1].ToArray();
            double[] x = k[0].ToArray();
            //  ds = new DataSeries();
            xVal = xvalues;
            yVal = yvalues;
            for (int i = 0; i < x.Length; i++)
            {
                ds.LineSeries.Points.Add(new Point(x[i], y[i]));
            }
            dc.DataList.Add(ds);

        }
        public void AddScatter(double[] xvalues, double[] yvalues)
        {
            plotSeries = PlotSeriesEnum.Scatter;
            xVal = xvalues;
            yVal = yvalues;
            for (int i = 0; i < xvalues.Length; i++)
            {
                ds.LineSeries.Points.Add(new Point(xvalues[i], yvalues[i]));
            }
            dc.DataList.Add(ds);
        }
        private void AddChart(double xmin, double xmax, double ymin, double ymax)
        {
            cs.Title = Title_;
            cs.XMin = xmin;
            cs.XMax = xmax;
            cs.YMin = ymin;
            cs.Ymax = ymax;
            cs.GridLinePattern = GridLinePatternEnum.Dot;
            cs.GridLineColor = Brushes.Black;
            cs.AddChartstyle(tbTitle, tbXLabel, tbYLabel);
            
            
            ds = new DataSeries();
            ds.LineThickness = thickness;
            ds.LineColor = Brushes.Blue;
            switch (plotSeries)
            {
                case PlotSeriesEnum.Scatter:
                    AddScatter(xVal, yVal);
                    break;
                case PlotSeriesEnum.PulseGate:
                    AddGatePulse(xVal, yVal);
                    break;
            }

            
            dc.AddLines(cs);
        }
        private void chartGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            tbDate.Text = DateTime.Now.ToShortDateString();
            textCanvas.Width = chartGrid.ActualWidth;
            textCanvas.Height = chartGrid.ActualHeight;
            chartCanvas.Children.Clear();
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            AddChart(cs.XMin, cs.XMax, cs.YMin, cs.Ymax);
        }
        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            double dx = (e.Delta > 0) ? xIncrement : -xIncrement;
            double dy = (e.Delta > 0) ? yIncrement : -yIncrement;
            double x0 = cs.XMin + (cs.XMax - cs.XMin) * dx / chartCanvas.Width;
            double x1 = cs.XMax - (cs.XMax - cs.XMin) * dx / chartCanvas.Width;
            double y0 = cs.YMin + (cs.Ymax - cs.YMin) * dy / chartCanvas.Height;
            double y1 = cs.Ymax - (cs.Ymax - cs.YMin) * dy / chartCanvas.Height;
            chartCanvas.Children.Clear();
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            AddChart(x0, x1, y0, y1);
        }
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!chartCanvas.IsMouseCaptured)
            {
                startpoint = e.GetPosition(chartCanvas);
                chartCanvas.CaptureMouse();
            }
        }
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (chartCanvas.IsMouseCaptured)
            {

                TranslateTransform tt = new TranslateTransform();
                endPoint = e.GetPosition(chartCanvas);
                if (Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftCtrl))
                {
                    if (rubberBand == null)
                    {
                        rubberBand = new Rectangle();
                        rubberBand.Stroke = Brushes.Red;
                        chartCanvas.Children.Add(rubberBand);

                    }
                    rubberBand.Width = Math.Abs(startpoint.X - endPoint.X);
                    rubberBand.Height = Math.Abs(startpoint.Y - endPoint.Y);
                    double left = Math.Min(startpoint.X, endPoint.X);
                    double top = Math.Min(startpoint.Y, endPoint.Y);
                    Canvas.SetLeft(rubberBand, left);
                    Canvas.SetTop(rubberBand, top);

                }
                else
                {
                    tt.X = -(endPoint.X - startpoint.X);
                    tt.Y = (endPoint.Y - startpoint.Y);
                    for (int i = 0; i < dc.DataList.Count; i++)
                    {

                        dc.DataList[i].LineSeries.RenderTransform = tt;

                    }

                    double dx = 0;
                    double dy = 0;
                    double x0 = 0;
                    double y0 = 0;
                    double x1 = 1;
                    double y1 = 1;
                    //  endPoint = e.GetPosition(chartCanvas);
                    dx = (cs.XMax - cs.XMin) * (endPoint.X - startpoint.X) / chartCanvas.Width;
                    dy = (cs.Ymax - cs.YMin) * (endPoint.Y - startpoint.Y) / chartCanvas.Height;
                    dy = 0;
                    x0 = cs.XMin - dx / 12;
                    x1 = cs.XMax - dx / 12;
                    y0 = cs.YMin + dy / 20;
                    y1 = cs.Ymax + dy / 20;

                    chartCanvas.Children.Clear();
                    textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
                    AddChart(x0, x1, y0, y1);

                    //  chartCanvas.ReleaseMouseCapture();
                    chartCanvas.Cursor = Cursors.Arrow;
                }



            }
        }
        private void OnMouseLeave(object sender, MouseEventArgs e)
        {

        }
        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            double dx = 0;
            double dy = 0;
            double x0 = 0;
            double y0 = 0;
            double x1 = 1;
            double y1 = 1;
            endPoint = e.GetPosition(chartCanvas);
            if (Keyboard.IsKeyDown(Key.RightCtrl) || Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                //Ctrl+ mouse leftbutton down ==> rubber band zoom.
                if (endPoint.X > startpoint.X)
                {
                    x0 = cs.XMin + (cs.XMax - cs.XMin) * startpoint.X /
                    chartCanvas.Width;
                    x1 = cs.XMin + (cs.XMax - cs.XMin) * endPoint.X /
                    chartCanvas.Width;
                }
                else if (endPoint.X < startpoint.X)
                {
                    x1 = cs.XMin + (cs.XMax - cs.XMin) * startpoint.X /
                    chartCanvas.Width;
                    x0 = cs.XMin + (cs.XMax - cs.XMin) * endPoint.X /
                    chartCanvas.Width;
                }

                if (endPoint.Y < startpoint.Y)
                {
                    y0 = cs.YMin + (cs.Ymax - cs.YMin) * (chartCanvas.Height - startpoint.Y) / chartCanvas.Height;
                    y1 = cs.YMin + (cs.Ymax - cs.YMin) * (chartCanvas.Height - endPoint.Y) / chartCanvas.Height;
                }
                else if (endPoint.Y > startpoint.Y)
                {
                    y1 = cs.YMin + (cs.Ymax - cs.YMin) * (chartCanvas.Height - startpoint.Y) / chartCanvas.Height;
                    y0 = cs.YMin + (cs.Ymax - cs.YMin) * (chartCanvas.Height - endPoint.Y) / chartCanvas.Height;
                }
                chartCanvas.Children.Clear();
                textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
                AddChart(x0, x1, y0, y1);

                if (rubberBand != null)
                {
                    rubberBand = null;
                    chartCanvas.ReleaseMouseCapture();
                }

            }
            else
            {
                // Panning with mouse left button down
                dx = (cs.XMax - cs.XMin) * (endPoint.X - startpoint.X) / chartCanvas.Width;
                dy = (cs.Ymax - cs.YMin) * (endPoint.Y - startpoint.Y) / chartCanvas.Height;
                //dx and dy allows to set horizontal and vertical panning =>dy = 0 is vertical pan lock and similar to horizontal.
                dy = 0;
                x0 = cs.XMin - dx;
                x1 = cs.XMax - dx;
                y0 = cs.YMin + dy;
                y1 = cs.Ymax + dy;

                chartCanvas.Children.Clear();
                textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
                AddChart(x0, x1, y0, y1);

                chartCanvas.ReleaseMouseCapture();
                chartCanvas.Cursor = Cursors.Arrow;
            }


        }
        private void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //mouse right button down graph sets to intitial axis limits

            chartCanvas.Children.Clear();
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            AddChart(xmin0, xmax0, ymin0, ymax0);
        }
        //UI functions
        #region UI Methods
        public void IsGrid(bool IsXgrid,bool IsYgrid)
        {
            cs.IsYGrid = IsYgrid;
            cs.IsXGrid = IsXgrid;
        }
        public void PlotThickness(double Thickness)
        {
                thickness = Thickness;
        }
        public void Frameless(bool IsFrameless)
        {
            if (IsFrameless)
            {
                tbTitle.Visibility = Visibility.Collapsed;
                tbXLabel.Visibility = Visibility.Collapsed;
                tbYLabel.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbTitle.Visibility = Visibility.Visible;
                tbXLabel.Visibility = Visibility.Visible;
                tbYLabel.Visibility = Visibility.Visible;
            }

        }
        public void SetAxisLimits(double xmin,double xmax,double ymin,double ymax)
        {
            xmin0 = xmin; xmax0 = xmax;
            ymin0 = ymin; ymax0 = ymax;

            cs.XMin = xmin0;
            cs.YMin = ymin0;
            cs.XMax = xmax0;
            cs.Ymax = ymax0;

        }
        #endregion

        

    }
    public enum PlotSeriesEnum
    {
        None = 0,
        Scatter = 1,
        PulseGate  = 2,
    }
}
