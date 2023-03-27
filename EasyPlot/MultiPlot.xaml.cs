using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EasyPlot
{
    /// <summary>
    /// Interaction logic for MultiPlot.xaml
    /// </summary>
    public partial class MultiPlot : UserControl 
    {
       
        public MultiPlot()
        {
            InitializeComponent();
            //  PlotList = plotList;
            
        }

        public void AddPlots(List<Plot> plotList)
        { 
            plotWrapPanel.Children.Clear();
            foreach(Plot plot in plotList)
            {
                plot.AxisChanged += delegate (object sender, RoutedEventArgs e) { Plot_AxisChanged(sender, e, plot); }; ;
                plotWrapPanel.Children.Add(plot);
            }
        }

        private void Plot_AxisChanged(object sender, RoutedEventArgs e,Plot plot)
        {
          //  MessageBox.Show("yass");
          foreach(Plot plt in plotWrapPanel.Children)
            {
                if(plot.id != plt.id)
                {
                    plt.MatchAxisLimits(plot.GetXlimits(), plot.GetYlimits());
                }
            }
        }

      
        

        private void plotScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if(plotWrapPanel.IsMouseOver)
            {
                plotWrapPanel.Focus();
                plotScrollViewer.ScrollToVerticalOffset(plotScrollViewer.VerticalOffset);
            }
        }
    }
}
           
