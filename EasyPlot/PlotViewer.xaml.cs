using System.Windows;

namespace EasyPlot
{
    /// <summary>
    /// Interaction logic for PlotViewer.xaml
    /// Pass the wpf plot as a parameter and PlotViewer.Show() populates the graph in a new window
    /// </summary>
    public partial class PlotViewer : Window
    {
        public PlotViewer(WpfPlot plot)
        {
            InitializeComponent();
            mainwindow_grid.Children.Add(plot);
        }
        public PlotViewer(UIElement uIElement)
        {
            InitializeComponent();
            mainwindow_grid.Children.Add((UIElement)uIElement);
        }
    }
}
