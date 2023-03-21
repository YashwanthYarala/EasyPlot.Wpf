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

        /*
        private double height { get { return this.MainWindow_Grid.ActualHeight; } set { Plot.Height_ = value; } }
        /// <summary>
        ///Height_ : Sets and Gets the Height of the window.
        /// </summary>
        public double Height_
        {
            get { return height; } set { height = value; }
        }
        private double width { get { return this.MainWindow_Grid.ActualWidth; } set { Plot.Width_ = value; } }
        /// <summary>
        ///Width_ : Sets and Gets the Width of the window.
        /// </summary>
        public double Width_
        {
            get { return height; }
            set { height = value; }
        }
        */
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

        private void MainWindow_Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Plot.Width = MainWindow_Grid.ActualWidth;
            Plot.Height = MainWindow_Grid.ActualHeight;
            MainWindow_Grid.Children.Clear();
            MainWindow_Grid.Children.Add(Plot);
        }
    }
}
