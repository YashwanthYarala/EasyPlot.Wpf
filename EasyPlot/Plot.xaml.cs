
using PlottingWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Policy;
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
    public partial class Plot : UserControl
    {
       
        private Point startpoint = new Point();
        private Point endPoint = new Point();
        protected ChartStyle cs { get; set; }
        private DataCollection dc { get; set; }
        private DataSeries ds { get; set; }
        protected  static double xmin0 = 0;
        protected static double xmax0 = 10;
        protected static double ymin0 = -1.5;
        protected static double ymax0 = 1.5;
        public List<double> TimePoints { get; set; }
        public List<double> SignalPoints { get; set; }
      //  public double[] x1 { get; set; }
      //  public double[] y1 { get; set; }
        //
        private double xIncrement { get; set; } = 5;
        private double yIncrement { get; set; } = 0;
        public string Title_ { get; set; }
        private Shape rubberBand = null;
        public double[] xVal { get; set; }
        public double[] yVal { get; set; }
        private double thickness { get; set; } = 1;
        private PlotSeriesEnum plotSeries { get; set; }
        private bool IsPanningLock { get; set; } = false;
        private bool IsPanningLock_X { get; set; } = false;
        private bool IsPanningLock_Y { get;set; } = false;

        private bool isZoomLock { get; set; } = false;
        public bool IsZoomLock
        {
            get { return isZoomLock; }
            set { isZoomLock = value; }
        }
        
        private bool matchAxis { get; set; } = true;
        public event EventHandler<double[]> AxisChangedEventHandler;
        private bool isRectangle { get; set; } = true;
        public bool IsReactangle
        {
            get { return isRectangle; }
            set { isRectangle = value; }
        }


        public Plot()
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
        #region Plot Series Types
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
            try
            {
                for (int i = 0; i < x.Length; i++)
                {
                    ds.LineSeries.Points.Add(new Point(x[i], y[i]));
                }
                dc = new DataCollection();
                dc.DataList.Add(ds);
            }
            catch (Exception ex)
            {
                if ((xvalues.Length != yvalues.Length))
                {
                    throw new Exception("Data Count Not Matching in X and Y " +ex.Message);
                }
                else if (xvalues == null || yvalues == null)
                {
                    throw new Exception("No Data Found To Plot "+ex.Message);
                }

            }
            

        }
        public void AddScatter(double[] xvalues, double[] yvalues)
        {
            plotSeries = PlotSeriesEnum.Scatter;
            xVal = xvalues;
            yVal = yvalues;
            try
            {
                for (int i = 0; i < xvalues.Length; i++)
                {
                    ds.LineSeries.Points.Add(new Point(xvalues[i], yvalues[i]));
                }
                dc = new DataCollection();
                dc.DataList.Add(ds);
            }catch (Exception ex)
            {
                if((xvalues.Length != yvalues.Length))
                {
                    throw new Exception("Data Count Not Matching in X and Y "+ex.Message);
                }
                else if( xvalues == null || yvalues == null)
                {
                    throw new Exception("No Data Found To Plot " + ex.Message);
                }

            }
            
        }
        #endregion
        #region Chart Related
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

            dc = new DataCollection();
            ds = new DataSeries();
            ds.LineThickness = thickness;
            ds.LineColor = Brushes.Blue;
            double[] limits_ = { xmin, xmax, ymin, ymax };
            if (matchAxis)
            {
                AxisChangedEventHandler?.Invoke(this, limits_);
            }
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
        #endregion
        #region Mouse Events
        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!isZoomLock)
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
                    if(!IsPanningLock)
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
                        if(!IsPanningLock_X)
                        {
                            dx = (cs.XMax - cs.XMin) * (endPoint.X - startpoint.X) / chartCanvas.Width;
                        }
                        if(!IsPanningLock_Y)
                        {
                            dy = (cs.Ymax - cs.YMin) * (endPoint.Y - startpoint.Y) / chartCanvas.Height;
                        }
                       
                       
                       
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
                if(!IsPanningLock)
                {
                    // Panning with mouse left button down
                    /*
                    dx = (cs.XMax - cs.XMin) * (endPoint.X - startpoint.X) / chartCanvas.Width;
                    dy = (cs.Ymax - cs.YMin) * (endPoint.Y - startpoint.Y) / chartCanvas.Height;
                    */
                    //dx and dy allows to set horizontal and vertical panning =>dy = 0 is vertical pan lock and similar to horizontal.
                    dy = 0;
                    if (!IsPanningLock_X)
                    {
                        dx = (cs.XMax - cs.XMin) * (endPoint.X - startpoint.X) / chartCanvas.Width;
                    }
                    if (!IsPanningLock_Y)
                    {
                        dy = (cs.Ymax - cs.YMin) * (endPoint.Y - startpoint.Y) / chartCanvas.Height;
                    }
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


        }
        private void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //mouse right button down graph sets to intitial axis limits

            chartCanvas.Children.Clear();
            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
            AddChart(xmin0, xmax0, ymin0, ymax0);
        }
        #endregion
        //UI functions
        #region UI Methods
        #region Grid Methods
        ///<summary>
        ///This Method Sets The Grid Lines to Plot
        ///</summary>
        
        public void IsGrid(bool IsXgrid,bool IsYgrid)
        {
            
            cs.IsYGrid = IsYgrid;
            cs.IsXGrid = IsXgrid;
        }
        ///<summary>
        ///This Method Sets The Vetical-Grid Lines to Plot
        ///</summary>
        
        public void IsXGrid(bool IsXgrid)
        {
            
            cs.IsXGrid = IsXgrid;
        }
        ///<summary>
        ///This Method Sets The Horizontal-Grid Lines to Plot
        ///</summary>
        public void IsYGrid(bool IsYgrid)
        {
           
            cs.IsYGrid = IsYgrid;
        }
        ///<summary>
        ///This Method Sets the Thickness of the Grid Lines
        ///Pass Double Value as a thickness parameter
        ///</summary>
        public void GridLineThickness(double thickness)
        {
           
            cs.GridLine.StrokeThickness = thickness;
        }
        ///<summary>
        ///This Method Sets the Color of the Grid Lines
        ///Pass Brush (Brushes.Color_Name) as a thickness parameter
        ///</summary>
        public void GridLineColor(Brush brush)
        {
           

            cs.GridLineColor = brush;
        }
        ///<summary>
        ///This Method Sets the Visibility of the Grid Lines.
        ///Pass Brush (Brushes.Color_Name) as a thickness parameter
        ///</summary>
        public void IsGridVisible(bool IsVisible)
        {
            
            cs.IsYGrid = IsVisible;
            cs.IsXGrid = IsVisible;
        }
        public void  HideLabels(bool isXVisible,bool isYVisible)
        {
            if(isXVisible)
            {
                tbXLabel.Visibility = Visibility.Visible;
            }
            else
            {
                tbXLabel.Visibility = Visibility.Collapsed;
            }
            if(isYVisible)
            {
                tbYLabel.Visibility = Visibility.Visible;
            }
            else
            {
                tbYLabel.Visibility= Visibility.Collapsed;
            }
            
        }
        public void SetRectangle(bool isRectangle)
        {

            if (isRectangle)
            {
                cs.chartRect.Visibility = Visibility.Visible;
            }
            else
            {
                cs.chartRect.Visibility = Visibility.Hidden;
            }
               
        }
        public void GridLinePattern(GridLinePatternEnum gridLinePattern)
        {
            cs.GridLinePattern = gridLinePattern;
        }
        #endregion
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
        #region Axis Methods
        public void SetAxisLimits(double xmin,double xmax,double ymin,double ymax)
        {
            xmin0 = xmin; xmax0 = xmax;
            ymin0 = ymin; ymax0 = ymax;

            cs.XMin = xmin0;
            cs.YMin = ymin0;
            cs.XMax = xmax0;
            cs.Ymax = ymax0;

        }
        public void SetXAxisLimits(double xmin, double xmax)
        {
            xmin0 = xmin; xmax0 = xmax;
            cs.XMin = xmin0; cs.XMax = xmax0;
        }
        public void SetYAxisLimits(double ymin, double ymax)
        {
            ymin0 = ymin; ymax0 = ymax;
            cs.YMin = ymin0;cs.Ymax = ymax0;
        }
        public void HideAxis(bool HideX, bool HideY)
        {
            cs.HideX = HideX; cs.HideY = HideY;
        }
        public void HideXAxis(bool HideX)
        {
            cs.HideX = HideX;
        }
        public void HideYAxis(bool HideY)
        {
            cs.HideY = HideY;
        }
        public void AutoAxis()
        {
            if (xVal != null && yVal != null)
            {
                xmin0 = xVal.Min();
                xmax0 = xVal.Max();
                ymin0 = yVal.Min();
                ymax0 = yVal.Max();
                cs.XMin = xmin0;
                cs.YMin = ymin0;
                cs.XMax = xmax0;
                cs.Ymax = ymax0;
            }
        }
        public bool MatchAxis
        {
            get { return matchAxis; }
            set { matchAxis = value; }
        }
        public double[] GetXlimits()
        {
            double[] limits = new double[2];
            limits[0] = cs.XMin;
            limits[1] = cs.XMax;
            return limits;
        }
        public double[] GetYlimits()
        {
            double[] limits = new double[2];
            limits[0] = cs.YMin;
            limits[1] = cs.Ymax;
            return limits;
        }

        public class AxisLimitsEventArgs : EventArgs
        {
            public double x1 { get; set; }
            public double y1 { get; set; }
            public double x2 { get; set; }
            public double y2 { get; set; }
        }
        public delegate void AxisLimitsChangedEventHandler(object source, AxisLimitsEventArgs e);
        public event AxisLimitsChangedEventHandler AxisLimitsChanged;
        public void OnAxisChanged()
        {
            AxisLimitsChanged?.Invoke(this, new AxisLimitsEventArgs { x1 = xmin0,x2 = xmax0,y1 = ymin0,y2 = ymax0 });
        }
        public void MatchAxisLimits(double[] xlimits, double[] ylimits)
        {
            if (matchAxis)
            {
                Exception ex  = null;
                try
                {
                    if(xlimits == null || ylimits == null)
                    {
                        ex = new Exception("Either of the limits or both the limits are null");

                    }
                    else
                    {
                        if (xlimits.Length == 2 && ylimits.Length == 2)
                        {
                            cs.XMax = xlimits[1];
                            cs.XMin = xlimits[0];
                            cs.YMin= ylimits[0];
                            cs.Ymax= ylimits[1];

                          //  xmin0 = xlimits[0];xmax0 = xlimits[1];
                          //  ymin0 = ylimits[0]; ymax0 = ylimits[1];
                            chartCanvas.Children.Clear();
                            textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
                            AddChart(cs.XMin,cs.XMax,cs.YMin,cs.Ymax);

                        }
                        else
                        {
                            ex = new Exception("Out of Index ");
                        }
                    }
                    
                }
                catch  
                {
                    MessageBox.Show(ex.Message,"MatchAxisLimits",MessageBoxButton.OK,MessageBoxImage.Error);
                }
               
            }
        }
        public void PanningLock(bool isPanning)
        {
            IsPanningLock = isPanning;
        }
        public void PanningLock_X(bool isPanningX)
        {
            IsPanningLock_X = isPanningX;
        }public void PanningLock_Y(bool isPanningY)
        {
            IsPanningLock_Y = isPanningY;
        }
        public void ZoomLock(bool isZoomLock)
        {
            IsZoomLock = isZoomLock;
        }
        public string YLabel
        {
            get { return cs.YLabel; }
            set { cs.YLabel = value; }
        }   
        public string XLabel
        {
            get { return cs.XLabel; }
            set { cs.XLabel = value; }
        }
        public string Title
        {
            get { return cs.Title; }
            set
            {
                cs.Title = value;
            }
        }
        public void SetPlotLabels(string title,string xlabel,string ylabel)
        {
            Title = title;
            XLabel = xlabel;
            YLabel = ylabel;
        }
        #endregion
        #endregion
        
        


    }

    public enum PlotSeriesEnum
    {
        None = 0,
        Scatter = 1,
        PulseGate  = 2,
    }
    
   
}
