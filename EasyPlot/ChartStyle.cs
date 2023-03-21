using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;

namespace EasyPlot
{
    public class ChartStyle
    {   public bool HideX { get; set; } = false; public bool HideY { get; set; } = false;
        private Canvas chartCanvas;
        private double xmin;
        private double xmax;
        private double ymin;
        private double ymax;
        public Rectangle chartRect = new Rectangle();
        public Canvas ChartCanvas
        {
            get { return chartCanvas; }
            set { chartCanvas = value; }
        }
        public double XMin
        {
            get { return xmin; }
            set { xmin = value; }

        }
        public double XMax
        {
            get { return xmax; }
            set { xmax = value; }
        }
        public double YMin
        {
            get { return ymin; }
            set
            {
                ymin = value;
            }
        }
        public double Ymax
        {
            get { return ymax; }
            set { ymax = value; }
        }

        //
        private double xtick_min { get; set; } = 0.5;
        private double xtick_max { get; set; } = 5;

        private double ytick_min { get; set; } = 0.5;
        private double ytick_max { get; set; } = 10;

        public Canvas CrossHairCanvas { get; set; }
        public double Xtick_min
        {
            get { return xtick_min; }

            set { xtick_min = value; }
        }
        public double Xtick_max
        {
            get { return xtick_max; }
            set { xtick_max = value; }
        }
        public double Ytick_min
        {
            get
            {
                return ytick_min;
            }
            set
            {
                ytick_min = value;
            }
        }
        public double Ytick_max
        {
            get
            {
                return ytick_max;
            }
            set
            {
                ytick_max = value;
            }
        }

        //

        #region GridStyles
        public string Title { get; set; }  = "Title";
        public string XLabel { get; set; } = "X Axis";
        public string YLabel { get; set; } = "Y Axis";
        public Canvas TextCanvas { get; set; } 
        public bool IsXGrid { get; set; } = true;
        public bool IsYGrid { get; set; } = true;
        public Brush GridLineColor { get; set; } = Brushes.LightGray;
        public double XTick { get; set; } = 1;
        public double YTick { get; set; } = 0.5;
        public GridLinePatternEnum GridLinePattern { get; set; } 
        public double LeftOffset = 20;
        public double RightOffset = 10;
        public double BottomOffset = 15;
        public Line GridLine { get; set; } = new Line();

        #endregion
        public void AddChartstyle(TextBlock tbTitle,TextBlock tbXLabel,TextBlock tbYLabel)
        {
            chartCanvas.Children.Clear();
           System.Windows.Point pt  = new System.Windows.Point();
            Line tick = new Line();
            double offset = 0;
            double dx,dy;
            TextBlock tb = new TextBlock();
            //for 2dinteractive plot
            double optimalXSpacing = 100;
            double optimalYSpacing = 80;
            //




            //determine the right offset
            tb.Text = Math.Round(XMax,0).ToString() ;
            tb.Measure(new System.Windows.Size(double.PositiveInfinity, double.PositiveInfinity));
            System.Windows.Size size = tb.DesiredSize;
            RightOffset = size.Width / 2 ;

            //determine the left offset

            //for 2d interactive plot
            double xScale = 0.0, yScale = 0.0;
            double xSpacing = 0.0, ySpacing = 0.0;
            double xTick = 0.0,yTick = 0.0;
            int xStart = 0, xEnd = 1;
            int yStart = 0,yEnd = 1;
            double offset0 = 30;

            while(Math.Abs(offset - offset0) > 1)
            {
                if (XMin != XMax)
                    xScale = (TextCanvas.Width - offset0 - RightOffset - 5) /(XMax - XMin);
                if(YMin != Ymax)
                {
                    yScale = TextCanvas.Height / (Ymax - YMin);
                   
                }
                
                xSpacing = optimalXSpacing / xScale;
                xTick = OptimalSpacing(xSpacing);
                ySpacing = optimalYSpacing / yScale;
                yTick = OptimalSpacing(ySpacing);
                xStart = (int)Math.Ceiling(XMin / xTick);
                xEnd = (int)Math.Floor(XMax / xTick);
                yStart = (int)Math.Ceiling(YMin / yTick);
                yEnd = (int)Math.Floor(Ymax / yTick);
                for (int i = yStart; i <= yEnd; i++)
                {
                    dy = i * yTick;
                    pt = NormalizePoint(new Point(XMin, dy));
                    tb = new TextBlock();
                    tb.Text = dy.ToString();
                    tb.TextAlignment = TextAlignment.Right;
                    tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                    size = tb.DesiredSize;
                    if (offset < size.Width)
                        offset = size.Width;
                }
                if (offset0 > offset)
                    offset0 -= 0.5;
                else if (offset0 < offset)
                    offset0 += 0.5;
            }/*

            for (dy = YMin;dy<=Ymax;dy += YTick)
            {
                pt = NormalizePoint(new System.Windows.Point(XMin, dy));
                tb = new TextBlock();
                tb.Text = dy.ToString();
                tb.Measure(new System.Windows.Size(double.PositiveInfinity,double.PositiveInfinity));
                size = tb.DesiredSize;
                if (offset < size.Width) { offset = size.Width; }

            } */
            //LeftOffset = offset + 5;
            LeftOffset  = 50 + 5;

            Canvas.SetLeft(ChartCanvas, LeftOffset);
            Canvas.SetBottom(ChartCanvas, BottomOffset);
            ChartCanvas.Width = Math.Abs(TextCanvas.Width-LeftOffset-RightOffset);
            chartCanvas.Height = Math.Abs(TextCanvas.Height-BottomOffset-size.Height/2);
            //  System.Windows.Shapes.Rectangle chartRect = new System.Windows.Shapes.Rectangle();
            chartRect = new Rectangle();
            chartRect.Stroke = Brushes.Black;
            chartRect.Width = ChartCanvas.Width;
            chartRect.Height = ChartCanvas.Height;
            ChartCanvas.Children.Add(chartRect);
            
            
            //2d interactive
            if (XMin != XMax)
                xScale = ChartCanvas.Width / (XMax - XMin);
            if (YMin != Ymax)
                yScale = ChartCanvas.Height / (Ymax - YMin);
            xSpacing = optimalXSpacing / xScale;
            
            xTick = OptimalSpacing(xSpacing);
            ySpacing = optimalYSpacing / yScale;
            yTick = OptimalSpacing(ySpacing);
            /*
            //tick validation
            if (xTick < xtick_min)
            {
                xTick = xtick_min;
            }
            else if (xTick > xtick_max)
            {
                xTick = xtick_max;
            }
            if (yTick < ytick_min)
            {
                yTick = ytick_min;
            }
            else if (yTick > ytick_max)
            {
                yTick = ytick_max;
            }
            */
            //
            xStart = (int)Math.Ceiling(XMin / xTick);
            xEnd = (int)Math.Floor(XMax / xTick);
            yStart = (int)Math.Ceiling(YMin / yTick);
            yEnd = (int)Math.Floor(Ymax / yTick);
            //
            //create vertical grid lines
            if (IsXGrid)
            {
                for (int i = xStart; i <= xEnd; i++)
                {
                    GridLine = new Line();
                    AddLinePattern();
                    dx = i * xTick;
                    GridLine.X1 = NormalizePoint(new Point(dx, YMin)).X;
                    GridLine.Y1 = NormalizePoint(new Point(dx, YMin)).Y;
                    GridLine.X2 = NormalizePoint(new Point(dx, Ymax)).X;
                    GridLine.Y2 = NormalizePoint(new Point(dx, Ymax)).Y;
                    ChartCanvas.Children.Add(GridLine);
                   
                }
                /*
                for(dx = xmin+XTick; dx < xmax; dx += XTick)
                {
                    GridLine = new Line();
                    AddLinePattern();
                    GridLine.X1 = NormalizePoint(new System.Windows.Point(dx,YMin)).X;
                    GridLine.Y1 = NormalizePoint(new System.Windows.Point(dx, YMin)).Y;
                    GridLine.X2 = NormalizePoint(new System.Windows.Point(dx, Ymax)).X;
                    GridLine.Y2 = NormalizePoint(new System.Windows.Point(dx, Ymax)).Y;
                    
                    chartCanvas.Children.Add(GridLine);

                }
                */
            }
           //create horizontal gridlines
           if(IsYGrid) 
           {
                for (int i = yStart; i <= yEnd; i++)
                {
                    GridLine = new Line();
                    AddLinePattern();
                    dy = i * yTick;
                    GridLine.X1 = NormalizePoint(new Point(XMin, dy)).X;
                    GridLine.Y1 = NormalizePoint(new Point(XMin, dy)).Y;
                    GridLine.X2 = NormalizePoint(new Point(XMax, dy)).X;
                    GridLine.Y2 = NormalizePoint(new Point(XMax, dy)).Y;

                    ChartCanvas.Children.Add(GridLine);
                }
               /*
               for(dy = YMin+YTick;dy<Ymax;dy += YTick)
                {
                    GridLine = new Line();
                    AddLinePattern();
                    GridLine.X1 = NormalizePoint(new Point(XMin, dy)).X;
                    GridLine.Y1 = NormalizePoint(new Point(XMin, dy)).Y;
                    GridLine.X2 = NormalizePoint(new Point(XMax, dy)).X;
                    GridLine.Y2 = NormalizePoint(new Point(XMax, dy)).Y;

                    ChartCanvas.Children.Add(GridLine);

                }  */
           }

            //create x-tick marks
            if (!HideX)
            {
                for (int i = xStart; i <= xEnd; i++)
                {
                    dx = i * xTick;
                    pt = NormalizePoint(new Point(dx, YMin));
                    tick = new Line();
                    tick.Stroke = Brushes.Black;
                    tick.X1 = pt.X;
                    tick.Y1 = pt.Y;
                    tick.X2 = pt.X;
                    tick.Y2 = pt.Y - 5;
                    ChartCanvas.Children.Add(tick);
                    tb = new TextBlock();
                    tb.Text = dx.ToString();
                    tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                    size = tb.DesiredSize;
                    TextCanvas.Children.Add(tb);
                    Canvas.SetLeft(tb, LeftOffset + pt.X - size.Width / 2);
                    Canvas.SetTop(tb, pt.Y + 2 + size.Height / 2);
                }
            }
            
            /*
            for(dx = XMin;dx<=XMax;dx+=XTick)
             {
                 pt = NormalizePoint(new Point(dx, YMin));
                 tick = new Line();
                 tick.Stroke = Brushes.Black;
                 tick.X1 = pt.X;
                 tick.Y1 = pt.Y;
                 tick.X2 = pt.X;
                 tick.Y2 = pt.Y-5;
                 ChartCanvas.Children.Add(tick);

                 tb = new TextBlock();
                 tb.Text = dx.ToString();
                 tb.Measure(new Size(double.PositiveInfinity,double.PositiveInfinity));
                 size = tb.DesiredSize;
                 TextCanvas.Children.Add(tb);
                 Canvas.SetLeft(tb,LeftOffset+pt.X-size.Width/2);
                 Canvas.SetTop(tb,pt.Y+size.Height/2);   
             }
            */

            //create y-tick marks
            if(!HideY)
            {
                for (int i = yStart; i <= yEnd; i++)
                {
                    dy = i * yTick;
                    pt = NormalizePoint(new Point(XMin, dy));
                    tick = new Line();
                    tick.Stroke = Brushes.Black;
                    tick.X1 = pt.X;
                    tick.Y1 = pt.Y;
                    tick.X2 = pt.X + 5;
                    tick.Y2 = pt.Y;

                    ChartCanvas.Children.Add(tick);
                    tb = new TextBlock();
                    string f = dy.ToString();

                    tb.Text = f;
                    tb.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                    size = tb.DesiredSize;
                    TextCanvas.Children.Add(tb);
                    Canvas.SetRight(tb, ChartCanvas.Width + 10);
                    Canvas.SetTop(tb, pt.Y);
                }
            }
           
            /*
            for (dy = YMin; dy <= Ymax; dy += YTick)
           {
                pt = NormalizePoint(new Point(XMin,dy));
                tick = new Line();
                tick.Stroke = Brushes.Black;
                tick.X1 = pt.X;
                tick.Y1 = pt.Y;
                tick.X2 = pt.X+5;
                tick.Y2 = pt.Y;
                ChartCanvas.Children.Add(tick);

                tb = new TextBlock();
                tb.Text = dy.ToString();
                tb.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                size = tb.DesiredSize;
                TextCanvas.Children.Add(tb);
                Canvas.SetRight(tb,ChartCanvas.Width+10);
                Canvas.SetTop(tb, pt.Y);
           }
            */
            tbTitle.Text = Title;
            tbXLabel.Text = XLabel;
            tbYLabel.Text = YLabel;
            tbXLabel.Margin = new Thickness(LeftOffset + 2, 2, 2, 2);
            tbTitle.Margin = new Thickness(LeftOffset + 2, 2, 2, 2);

        }
        #region OptimalSpacing
        public double OptimalSpacing(double original)
        {
            double[] da = { 1.0, 2.0, 5.0 };
            double multiplier = Math.Pow(10, Math.Floor(Math.Floor(Math.Log(original) / Math.Log(10))));
            double dmin = 100 * multiplier;
            double spacing = 0.0;
            double mn = 100;
            foreach(double d in da)
            {
                double delta = Math.Abs(original - d * multiplier);
                if (delta < dmin)
                {
                    dmin = delta;
                    spacing = d * multiplier;
                }
                if (d < mn)
                {
                    mn = d;
                }

            }
            if (Math.Abs(original - 10 * mn * multiplier) < Math.Abs(original - spacing))
                spacing = 10 * mn * multiplier;
            if (spacing < 0.5)
                if (spacing < 0.25)
                {
                    spacing = 0.25;
                }
                else
                {
                    spacing = 0.5;
                }
                
            return spacing;

        }
        #endregion
        public System.Windows.Point NormalizePoint(System.Windows.Point pt)
        {
           System.Windows.Point result = new System.Windows.Point();
            if (ChartCanvas.Height.ToString() == "NaN")
            {
                ChartCanvas.Height = 250;
            }
            if (ChartCanvas.Width.ToString() == "NaN")
            {
                ChartCanvas.Width = 270;
            }
            //chaging real world coordinates to viewport coordinates 
            result.X = (pt.X - XMin) * ChartCanvas.Width / (XMax - XMin);
            result.Y = ChartCanvas.Height - (pt.Y - YMin) * ChartCanvas.Height / (Ymax - YMin);
            return result;
        }
        
        public void AddLinePattern()
        {
            GridLine.Stroke = GridLineColor;
            GridLine.StrokeThickness = 1;
            switch (GridLinePattern)
            {
                case GridLinePatternEnum.Dash:
                    GridLine.StrokeDashArray =
                    new DoubleCollection(new double[2] { 4, 3 });
                    break;
                case GridLinePatternEnum.Dot:
                    GridLine.StrokeDashArray =
                    new DoubleCollection(new double[2] { 1, 2 });
                    break;
                case GridLinePatternEnum.DashDot:
                    GridLine.StrokeDashArray =
                    new DoubleCollection(new double[4] { 4, 2, 1, 2 });
                    break;
            }
        }

    }
    

    public enum GridLinePatternEnum
    {
        Solid = 1,
        Dash = 2,
        Dot = 3,
        DashDot = 4
    }
}
