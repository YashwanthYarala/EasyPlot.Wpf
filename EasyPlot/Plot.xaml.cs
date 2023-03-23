

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
    /// Interaction logic for Plot User Control.
    /// </summary>
    public partial class Plot : UserControl
    {
        Coordinates coordinates = new Coordinates();
        private Point startpoint = new Point();
        private Point endPoint = new Point();
        private ChartStyle cs { get; set; }
        private DataCollection dc { get; set; }
        private DataSeries ds { get; set; }
        protected  static double xmin0 = 0;
        protected static double xmax0 = 10;
        protected static double ymin0 = -1.5;
        protected static double ymax0 = 1.5;
      //  public List<double> TimePoints { get; set; }
      //  public List<double> SignalPoints { get; set; }
      //  public double[] x1 { get; set; }
      //  public double[] y1 { get; set; }
        //
        private double xIncrement { get; set; } = 5;
        private double yIncrement { get; set; } = 0;
        public string Title_ { get; set; }
        private Shape rubberBand = null;
        private static double[] xVal { get; set; }
        private static double[] yVal { get; set; }
        private double thickness { get; set; } = 1;
        private PlotSeriesEnum plotSeries { get; set; }
        private bool IsPanningLock { get; set; } = false;
        private bool IsPanningLock_X { get; set; } = false;
        private bool IsPanningLock_Y { get;set; } = false;

        //panning Limits
        private double x_left { get; set; } = double.NegativeInfinity;
        private double x_right { get; set; } = double.PositiveInfinity;
        private double y_down { get; set; } = double.NegativeInfinity;
        private double y_up { get; set; } = double.PositiveInfinity;

        //

        //
        private bool isReadData { get; set; } = true;
        public bool IsReadData { get { return isReadData; }
            set { isReadData = value; }
        }

        private double x_Coordinate { get; set; }
        private double y_Coordinate { get; set; }

        /// <summary>
        /// Get the X-Coordinate of Pointer up to 4 Decimal Places.
        /// Alternate:Use GetCoordinates(Int no_of_decimalPlaces).X
        /// </summary>
        public double X_Coordinate
        {
            get {  return Math.Round(x_Coordinate,4); }
           
        }

        private  int LockCount = 0;

        public string Name { get; set; } = "";

        /// <summary>
        /// Get the Y-Coordinate of the pointer up to 4 Decimal Places.
        /// Alternate:Use GetCoordinates(Int no_of_decimalPlaces).X
        /// </summary>
        public double Y_Coordinate
        {
            get { return Math.Round(y_Coordinate,4); }
        }
        
        

        //

        /*
        private double height { get { return plotGrid.ActualHeight; } set { plotGrid.Height = value; } }
        /// <summary>
        /// Gets and Gets the Height of the plot .
        /// </summary>
        public double Height_
        {
            get { return height; }
            set { height = value; }
        }

        private double width { get { return plotGrid.ActualWidth; } set { plotGrid.Width = value; } }
        /// <summary>
        /// Gets and Gets the Height of the plot .
        /// </summary>
        public double Width_
        {
            get { return width; }
            set { width = value; }
        }
        */
        private bool isZoomLock { get; set; } = false;

        /// <summary>
        /// Sets ot Get the ZoomLock of the Plot.
        /// </summary>
        public bool IsZoomLock
        {
            get { return isZoomLock; }
            set { isZoomLock = value; }
        }
        
        private bool matchAxis { get; set; } = true;

        /// <summary>
        ///Event Hander that triggers when the axis of the plot are changed. 
        /// </summary>
        public event EventHandler<double[]> AxisChangedEventHandler;
        private bool isRectangle { get; set; } = true;
        public bool IsReactangle
        {
            get { return isRectangle; }
            set { isRectangle = value; }
        }
        //

        /// <summary>
        /// 
        /// </summary>
        
        public Symbols.SymbolTypeEnum SymbolType { get{ return symboltype; } set { symboltype = value; } }
        private Symbols.SymbolTypeEnum symboltype {  get; set; }     
        private Brush symbolColor { get; set; }
        public Brush SymbolColor { get { return symbolColor; } set { symbolColor = value; } }

        //CrossHairs
        private bool isCrossHair { get; set; } = false;
        public bool IsCrossHair { get { return isCrossHair; } set { isCrossHair = value; } }
        private CrossHair VerticalCrossHair { get; set; } = new CrossHair();
        private CrossHair HorizontalCrossHair { get; set; } = new CrossHair();

        private Dictionary<double,double> XYDict { get; set; }
        //
        private SmartLoader smartLoader { get; set; } 
         
        private double[] xvalues_smart { get; set; } = new double[0];
        private double[] yvalues_smart { get; set; } = new double[0];
        
        public Plot()
        {
            InitializeComponent();
            // GenData genData = new GenData();
           
           
            cs = new ChartStyle();
            ds = new DataSeries(); 
            dc = new DataCollection();
            cs.ChartCanvas = chartCanvas;
            cs.TextCanvas = textCanvas;
            cs.CrossHairCanvas = crosshairCanvas;
            cs.XMin = xmin0;
            cs.XMax = xmax0;
            cs.YMin = ymin0;
            cs.Ymax = ymax0;
            symboltype = Symbols.SymbolTypeEnum.None;
            smartLoader = new SmartLoader();
            XYDict  = new Dictionary<double, double>();
            
        }
        #region Plot Series Types

        /// <summary>
        /// Adds GatePulse to the plot.
        /// </summary>
        /// <param name="xvalues"></param>
        /// <param name="yvalues"></param>
        /// <exception cref="Exception"></exception>
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
                ds.Symbols.SymbolType = symboltype;
                if (symbolColor != null)
                {
                    ds.Symbols.FillColor = symbolColor;
                }
                else
                {
                    ds.Symbols.FillColor = ds.LineColor;
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

        /// <summary>
        /// Adds Scatter Plot .
        /// </summary>
        /// <param name="xvalues"></param>
        /// <param name="yvalues"></param>
        /// <exception cref="Exception"></exception>
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
                ds.Symbols.SymbolType = symboltype;
                if(symbolColor !=  null)
                {
                    ds.Symbols.FillColor = symbolColor;
                }
                else
                {
                    ds.Symbols.FillColor = ds.LineColor;
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
            //Panning Limits Validation.
           
            if (xmin < x_left)
            {
                xmin = x_left;
            }
            if(xmax>x_right)
            {
                xmax = x_right;
            }
            if(ymin < y_down)
            {
                ymin = y_down;
            }
            if (ymax > y_up)
            {
                ymax = y_up;
            }
            
            //Panning validation Ends Here.

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

            if (LockCount == 0)
            {

                //XYDict.Clear();
                //for (int i = 0; i < xVal.Length; i++)
                //{
                //    XYDict.Add(xVal[i], yVal[i]);
                //}
                xvalues_smart = xVal;
                yvalues_smart = yVal;
                LockCount++;
            }
            smartLoader = new SmartLoader();
            var val = smartLoader.GetValues(cs, xvalues_smart,yvalues_smart);
            //switch (plotSeries)
            //{
            //    case PlotSeriesEnum.Scatter:
            //        AddScatter(xVal, yVal);
            //        break;
            //    case PlotSeriesEnum.PulseGate:
            //        AddGatePulse(xVal, yVal);
            //        break;
            //}
            switch (plotSeries)
            {
                case PlotSeriesEnum.Scatter:
                    AddScatter(val.XAxis.ToArray(), val.YAxis.ToArray());
                    break;
                case PlotSeriesEnum.PulseGate:
                    AddGatePulse(val.XAxis.ToArray(), val.YAxis.ToArray());
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
              //  chartCanvas.Cursor = Cursors.Cross;
                chartCanvas.CaptureMouse();
                chartCanvas.Cursor = Cursors.Hand;

               
            }
        }
        private void OnMouseMove(object sender, MouseEventArgs e)
        {   
            chartCanvas.ToolTip = null;
            
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
                        chartCanvas.Cursor = Cursors.Hand;
                        
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
                       
                       
                       
                        x0 = cs.XMin - dx / 5;
                        x1 = cs.XMax - dx / 5;
                        y0 = cs.YMin + dy / 10;
                        y1 = cs.Ymax + dy / 10;

                        chartCanvas.Children.Clear();
                        textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
                        AddChart(x0, x1, y0, y1);

                        //  chartCanvas.ReleaseMouseCapture();
                     //   chartCanvas.Cursor = Cursors.Arrow;
                    }
                    
                }



            }
            else
            {
                chartCanvas.Cursor = Cursors.Cross;
                //  chartCanvasToolTip.Cursor = Cursors.Cross;

               


                if (isReadData)
                {
                    endPoint = e.GetPosition(chartCanvas);
                    if (Math.Abs(endPoint.X - startpoint.X) > SystemParameters.MinimumHorizontalDragDistance && Math.Abs(endPoint.Y - startpoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                    {
                        double x, y;
                        for (int i = 0; i < dc.DataList.Count; i++)
                        {
                            TranslateTransform tt = new TranslateTransform();
                            tt.X = endPoint.X - startpoint.X;
                            tt.Y = GetInterpolatedYValue(dc.DataList[i], endPoint.X) - GetInterpolatedYValue(dc.DataList[i], startpoint.X);

                            x = endPoint.X;
                            y = endPoint.Y;
                            x = cs.XMin + x * (cs.XMax - cs.XMin) / chartCanvas.Width;
                            //   y = GetInterpolatedYValue(dc.DataList[i], endPoint.X);
                            y = cs.YMin + (chartCanvas.Height - y) * (cs.Ymax - cs.YMin) / chartCanvas.Height;

                            //x_Coordinate = Math.Round(x, 4);
                            //y_Coordinate = Math.Round(y, 4);
                            x_Coordinate = x;
                            y_Coordinate = y;
                            VerticalCrossHair = new CrossHair();
                            HorizontalCrossHair = new CrossHair();
                            

                        }
                        
                        


                    }
                    if (isCrossHair)
                    {


                        //Vertical CrossHair
                        //  VerticalCrossHair.Line.X1 = x;
                        VerticalCrossHair.Line.X1 = cs.NormalizePoint(new Point(x_Coordinate, y_Coordinate)).X;
                        //  VerticalCrossHair.Line.X2 = x;
                        VerticalCrossHair.Line.X2 = cs.NormalizePoint(new Point(x_Coordinate, y_Coordinate)).X;

                        //VerticalCrossHair.Line.Y1 = cs.YMin;
                        //VerticalCrossHair.Line.Y2 = cs.Ymax;
                        VerticalCrossHair.Line.Y1 = cs.NormalizePoint(new Point(x_Coordinate, cs.YMin)).Y;
                        VerticalCrossHair.Line.Y2 = cs.NormalizePoint(new Point(x_Coordinate, cs.Ymax)).Y;

                        VerticalCrossHair.AddLinePattern();

                        //chartCanvas.Children.Add(VerticalCrossHair.Line);
                        // crosshairCanvas.Children.Clear();
                        //  crosshairCanvas.Children.Add(VerticalCrossHair.Line);
                        //Horizontal CrossHair
                        //HorizontalCrossHair.Line.X1 = cs.XMin;
                        //HorizontalCrossHair.Line.X2 = cs.XMax;
                        //HorizontalCrossHair.Line.Y1 = y;
                        //HorizontalCrossHair.Line.Y2 = y;

                        chartCanvas.Children.Clear();
                        //   crosshairCanvas.Children.Add(VerticalCrossHair.Line);
                        textCanvas.Children.RemoveRange(1, textCanvas.Children.Count - 1);
                        AddChart(cs.XMin, cs.XMax, cs.YMin, cs.Ymax);
                        chartCanvas.Children.Add(VerticalCrossHair.Line);

                    }
                    /// CrossHairs.
                }


            }
        }
        private double GetInterpolatedYValue(DataSeries data, double x)
        {
            double result = double.NaN;
            for (int i = 1; i < data.LineSeries.Points.Count; i++)
            {
                double x1 = data.LineSeries.Points[i - 1].X;
                double x2 = data.LineSeries.Points[i].X;
                if (x >= x1 && x < x2)
                {
                    double y1 = data.LineSeries.Points[i - 1].Y;
                    double y2 = data.LineSeries.Points[i].Y;
                    result = y1 + (y2 - y1) * (x - x1) / (x2 - x1);

                }
            }
            return result;
        }
        private void OnMouseLeave(object sender, MouseEventArgs e)
        {

        }
        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            chartCanvas.Cursor = Cursors.Arrow;
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

        /// <summary>
        /// This Method Sets the Visibility of X label and Y label.
        /// </summary>
        /// <param name="isXVisible"></param>
        /// <param name="isYVisible"></param>
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

        /// <summary>
        /// This Method Sets the Pattern of the Grid Line .
        /// </summary>
        /// <param name="gridLinePattern"></param>
        public void GridLinePattern(GridLinePatternEnum gridLinePattern)
        {
            cs.GridLinePattern = gridLinePattern;
        }
        #endregion

        /// <summary>
        /// This Method Sets the Thickness of the Plot .
        /// Pass Double value as a parameter.
        /// </summary>
        /// <param name="Thickness"></param>
        public void PlotThickness(double Thickness)
        {
                thickness = Thickness;
        }

        /// <summary>
        /// This Method Sets the the Visibility of Labels and Title of the plot.
        /// </summary>
        /// <param name="IsFrameless"></param>
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

        /// <summary>
        /// This Method Set the initial  axis-limits of the Plot
        /// The Plot is Displayed in this Limits .
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        /// <param name="ymin"></param>
        /// <param name="ymax"></param>
        public void SetAxisLimits(double xmin,double xmax,double ymin,double ymax)
        {
            xmin0 = xmin; xmax0 = xmax;
            ymin0 = ymin; ymax0 = ymax;

            cs.XMin = xmin0;
            cs.YMin = ymin0;
            cs.XMax = xmax0;
            cs.Ymax = ymax0;

        }
        /// <summary>
        /// This Method Sets the Initial Limits of X axis that is to be displayed.
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="xmax"></param>
        public void SetXAxisLimits(double xmin, double xmax)
        {
            xmin0 = xmin; xmax0 = xmax;
            cs.XMin = xmin0; cs.XMax = xmax0;
        }
        /// <summary>
        /// This Method Sets the Initial Limits of Y axis that is to be displayed.
        /// </summary>
        /// <param name="ymin"></param>
        /// <param name="ymax"></param>
        public void SetYAxisLimits(double ymin, double ymax)
        {
            ymin0 = ymin; ymax0 = ymax;
            cs.YMin = ymin0;cs.Ymax = ymax0;
        }

        /// <summary>
        /// This Method Sets the Visibility of the Axes.
        /// </summary>
        /// <param name="HideX"></param>
        /// <param name="HideY"></param>
        public void HideAxis(bool HideX, bool HideY)
        {
            cs.HideX = HideX; cs.HideY = HideY;
        }

        /// <summary>
        /// This Method Sets the Visibility of X-Axis(X axis Ticks)
        /// </summary>
        /// <param name="HideX"></param>
        public void HideXAxis(bool HideX)
        {
            cs.HideX = HideX;
        }
        /// <summary>
        /// This Method Sets the Visibility of Y-Axis(Y axis Ticks)
        /// </summary>
        /// <param name="HideY"></param>
        public void HideYAxis(bool HideY)
        {
            cs.HideY = HideY;
        }

        /// <summary>
        /// This Method Set the Min and Max Tick values .
        /// Used to control the text of Ticks
        /// </summary>
        /// <param name="Xtick_min"></param>
        /// <param name="Xtick_max"></param>
        /// <param name="Ytick_min"></param>
        /// <param name="Ytick_max"></param>
        public void SetTicks(double Xtick_min, double Xtick_max, double Ytick_min, double Ytick_max)
        {
            cs.Xtick_min = Xtick_min;
            cs.Xtick_max = Xtick_max;
            cs.Ytick_max = Ytick_max;
            cs.Ytick_min = Ytick_min;
        }

        /// <summary>
        /// This Method Sets the Plot to fit in the Window.
        /// Axis Limits are automatically calculated based on the Range of Input Data.
        /// </summary>
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

        /// <summary>
        /// Set true or false for the plot axes to  match to other plots.
        /// </summary>
        public bool MatchAxis
        {
            get { return matchAxis; }
            set { matchAxis = value; }
        }
        /// <summary>
        /// This Method is used to Get the Current limits of the X Axis.
        /// </summary>
        /// <returns>Double[]</returns>
        public double[] GetXlimits()
        {
            double[] limits = new double[2];
            limits[0] = cs.XMin;
            limits[1] = cs.XMax;
            return limits;
        }
        
        /// <summary>
        /// This Method is used to Get the Current limits of the Y Axis.
        /// </summary>
        /// <returns>Double[]</returns>
        public double[] GetYlimits()
        {
            double[] limits = new double[2];
            limits[0] = cs.YMin;
            limits[1] = cs.Ymax;
            return limits;
        }

       /// <summary>
       /// This Method matches the axis limits of the plot to the limits of source plot passed as arguments.
       /// </summary>
       /// <param name="xlimits"></param>
       /// <param name="ylimits"></param>
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
        /// <summary>
        /// This Method Sets the Panning Limts of the Graph.
        /// Panning Does not go beyond this Limits.
        /// </summary>
        /// <param name="X_left"></param>
        /// <param name="X_right"></param>
        /// <param name="Y_down"></param>
        /// <param name="Y_up"></param>
        public void SetPanningLimits(double X_left,double X_right,double Y_down,double Y_up)
        {
            x_left = X_left;
            x_right = X_right;
            y_down = Y_down;
            y_up = y_up;
        }

        /// <summary>
        /// This Methods Set the Panning Effect to Occur or Not.
        /// </summary>
        /// <param name="isPanning"></param>
        public void PanningLock(bool isPanning)
        {
            IsPanningLock = isPanning;
        }

        /// <summary>
        /// This method Locks the Panning in X-Axis Direction (Horizontal)
        /// </summary>
        /// <param name="isPanningX"></param>
        public void PanningLock_X(bool isPanningX)
        {
            IsPanningLock_X = isPanningX;
        }
        /// <summary>
        /// This method Locks the Panning in Y-Axis Direction (Verical)
        /// </summary>
        /// <param name="isPanningY"></param>
        public void PanningLock_Y(bool isPanningY)
        {
            IsPanningLock_Y = isPanningY;
        }

        /// <summary>
        /// This Method Sets the Functionality of Zoom.
        /// </summary>
        /// <param name="isZoomLock"></param>
        public void ZoomLock(bool isZoomLock)
        {
            IsZoomLock = isZoomLock;
        }

        /// <summary>
        /// Sets or Gets the Y Axis Label (String)
        /// </summary>
        public string YLabel
        {
            get { return cs.YLabel; }
            set { cs.YLabel = value; }
        }

        /// <summary>
        ///  Sets or Gets the Y Axis Label (String)
        /// </summary>
        public string XLabel
        {
            get { return cs.XLabel; }
            set { cs.XLabel = value; }
        }
        /// <summary>
        /// Sets or Gets the Title (String)
        /// </summary>
        public string Title
        {
            get { return cs.Title; }
            set
            {
                cs.Title = value;
            }
        }

        /// <summary>
        /// This Method Sets Title ,X-label,Y-Label of the Plot
        /// </summary>
        /// <param name="title"></param>
        /// <param name="xlabel"></param>
        /// <param name="ylabel"></param>
        public void SetPlotLabels(string title,string xlabel,string ylabel)
        {
            Title = title;
            XLabel = xlabel;
            YLabel = ylabel;
        }

        /// <summary>
        /// This Method Return Coordinates of the pointer .
        /// Pass Integer value to round off the decimal positions.
        /// </summary>
        /// <param name="Round_to"></param>
        /// <returns></returns>
        public Coordinates GetCoordinates(int Round_to = 4)
        {
            coordinates = new Coordinates();
            coordinates.X = Math.Round(x_Coordinate,Round_to);
            coordinates.Y = Math.Round(Y_Coordinate,Round_to);
            return coordinates;
        }
       
        #endregion
        #endregion
        
        public class Coordinates
        {
            public double X { get; set; }
            public double Y { get;set; }
        }

        private void chartCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
    /// <summary>
    /// This Enum is to switch between different type of plots.
    /// </summary>
    public enum PlotSeriesEnum
    {
        None = 0,
        Scatter = 1,
        PulseGate  = 2,
    }
    
   
}
