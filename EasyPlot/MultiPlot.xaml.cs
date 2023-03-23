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
    /// Interaction logic for MultiPlot.xaml
    /// </summary>
    public partial class MultiPlot : UserControl
    {
        //List<Plot>PlotList = new List<Plot>();
        private bool matchAxis { get; set; } = true;
        public bool MatchAxis { get { return matchAxis; } set { matchAxis = value; } }
        public MultiPlot(List<Plot>plotList)
        {
            InitializeComponent();
          //  PlotList = plotList;
            AddPlots(plotList);
        }
        public MultiPlot(List<WpfPlot> plotList)
        {
            InitializeComponent();
            AddPlots(plotList);
        }
        private void AddPlots(List<Plot>PlotList)
        {
            plotStackPanel.Children.Clear();
            
            foreach(Plot plot in PlotList)
            {

                plotStackPanel.Children.Add(plot);    
            }
        }
        public void AddPlots(List<WpfPlot> plotList)
        {
            plotStackPanel.Children.Clear();
            foreach (WpfPlot plot in plotList)
            {
                if(matchAxis)
                {
                    plot.Plot.MatchAxis = matchAxis;
                    plot.Plot.AxisChangedEventHandler += delegate (object sender, double[] e) { Plot_AxisChangedEventHandler(sender, e, plot); }; ; ;
                }
               
                plotStackPanel.Children.Add(plot);
            }
        }

        private void Plot_AxisChangedEventHandler(object? sender, double[] e,WpfPlot plot)
        {
            var x = plot.Plot.GetXlimits();
            var y = plot.Plot.GetYlimits();
            
           foreach(WpfPlot plt in plotStackPanel.Children)
           {
                if(plt.Plot.Name != plot.Plot.Name)
                {
                    plt.Plot.MatchAxisLimits(plot.Plot.GetXlimits(), plot.Plot.GetYlimits());
                }
               

           }
            
            
        }

        private void MultiplotScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if(plotStackPanel.IsMouseOver)
            {
                plotStackPanel.Focus();
                MultiplotScrollViewer.ScrollToVerticalOffset(MultiplotScrollViewer.VerticalOffset);
            }
        }
    }
}
