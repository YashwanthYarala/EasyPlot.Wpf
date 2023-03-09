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
using System.Windows.Shapes;

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
            mainwindow_grid.Children.Add((UIElement)uIElement);
        }
    }
}
