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
    {   public Plot Plot { get; set; }
        public WpfPlot()
        {
            InitializeComponent();
            try
            {
                Plot = new Plot();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "Error in Loading Plot ");
            }
           
            MainWindow_Grid.Children.Clear();   
            MainWindow_Grid.Children.Add(Plot);
        }
        public WpfPlot(Plot plot)
        {
            InitializeComponent();
            MainWindow_Grid.Children.Clear();
            MainWindow_Grid.Children.Add(Plot);
        }
        
    }
}
