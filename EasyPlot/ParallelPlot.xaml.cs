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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasyPlot
{
    /// <summary>
    /// Interaction logic for ParallelPlot.xaml
    /// </summary>
    public partial class ParallelPlot : UserControl
    {
        public double LeftOffSet { get { return leftOffSet; } set { leftOffSet = value; } }

        public double RightOffSet { get { return rightOffSet; }set { rightOffSet = value; } }
        private double rightOffSet { get; set; } = 30;

        private double leftOffSet { get; set; } = 10;

        public bool isMatchAxis { get; set; } = true;

        private bool isNewColor { get; set; } = true;
        private Plot xaxisPlot { get; set; }

        private double[] XaxisValues { get; set; }

        private int plotCount { get; set; } = 0;
        public double totalPlotsHeight = 0;

     //   private List<Plot>PlotList = new List<Plot>();
        public ParallelPlot()
        {
            InitializeComponent();
            plotWrapPanel.Margin = new Thickness(0, 0, 0, 0);
            xaxisPlot = new Plot();

        }
        public void AddPlots(List<Plot> plotList)
        {
            if(plotList != null)
            {
              //  PlotList = plotList;
                try
                {
                   plotCount = plotList.Count;
                   foreach(Plot plot in plotList)
                    {   
                        
                        plot.tbTitle.Visibility = Visibility.Collapsed;
                        plot.tbXLabel.Visibility = Visibility.Collapsed;
                        plot.tbDate.Visibility = Visibility.Collapsed;
                        plot.plotGrid.Margin = new Thickness(leftOffSet,0,rightOffSet,0);
                        plot.textCanvas.Margin = new Thickness(2,0,0,0);
                        plot.HideXAxis(true);
                        plot.IsUsedAsParallel = true;
                        plot.PlotThickness(6);

                        totalPlotsHeight += plot.Height != null?plot.Height:0;

                        if (isNewColor)
                        {
                            
                            Random r = new Random();
                            Brush brush = new SolidColorBrush(Color.FromRgb((byte)r.Next(1, 255), (byte)r.Next(1, 255), (byte)r.Next(1, 233)));
                            plot.PlotColor = brush;


                        }
                        //   plot.textCanvas.Margin = new Thickness(0,0,0,0);
                        if (isMatchAxis)
                        {
                            plot.AxisChanged += delegate (object sender, RoutedEventArgs e) { Plot_AxisChanged(sender, e, plot); }; ;
                        }
                        else
                        {
                            continue;
                        }
                        if(plot == plotList.Last())
                        {
                            plotWrapPanel.Children.Add(plot);
                            
                        }
                        else
                        {
                            plotWrapPanel.Children.Add(plot);
                        }

                    }
                    AddXAxis();


                }catch(Exception ex)
                {

                }
            }
            else
            {
                MessageBox.Show("Plot list is Empty!", "ParallelPlot", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Plot_AxisChanged(object sender, RoutedEventArgs e,Plot plot)
        {
          
            foreach (Plot plt in plotWrapPanel.Children)
            {
                
                if (plot.id != plt.id)
                {
                  
                    plt.MatchAxisLimits(plot.GetXlimits(), plot.GetYlimits());
                }
            }
            xaxisPlot.MatchAxisLimits(plot.GetXlimits(),plot.GetYlimits());
        }

        private void plotScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (plotWrapPanel.IsMouseOver)
            {
                plotWrapPanel.Focus();
                plotScrollViewer.ScrollToVerticalOffset(plotScrollViewer.VerticalOffset);
            }
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
          //  plotWrapPanel.ItemWidth = mainGrid.ActualWidth;
        }
        private void AddXAxis()
        {
            xaxisPlot = new Plot();
            xaxisPlot.HideYAxis(true);
            double[] xaxis = { 0, 0 };
            double[] yaxis = {0,0 };
            xaxisPlot.tbTitle.Visibility = Visibility.Collapsed;
            xaxisPlot.tbYLabel.Visibility = Visibility.Hidden;
            xaxisPlot.tbDate.Visibility = Visibility.Collapsed;
            xaxisPlot.plotGrid.Margin = new Thickness(leftOffSet, 0, rightOffSet, 0);
            xaxisPlot.textCanvas.Margin = new Thickness(2, 0, 0, 0);
            xaxisPlot.PanningLock_Y(true);
            xaxisPlot.IsUsedAsParallel = true;
            xaxisPlot.IsUsedAsXaxis = true;
            xaxisPlot.Height = 53;
            xaxisPlot.ChartRectStroke = Brushes.Black;
            xaxisPlot.GridLinePattern(GridLinePatternEnum.Solid);
            xaxisPlot.AxisChanged += delegate (object sender, RoutedEventArgs e) { XaxisPlot_AxisChanged(sender, e, xaxisPlot); }; ;
            xaxisPlot.PlotColor = Brushes.Transparent;
            xaxisPlot.AddGatePulse(xaxis, yaxis);
            xaxisWrapPanel.Children.Clear();
            xaxisWrapPanel.Children.Add(xaxisPlot);
        }

        private void XaxisPlot_AxisChanged(object sender, RoutedEventArgs e,Plot plot)
        {
            foreach (Plot plt in plotWrapPanel.Children)
            {

                plt.MatchAxisLimits(plot.GetXlimits(), plot.GetYlimits());
            }
        }

        private void plotScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
           

        }
    }
}
