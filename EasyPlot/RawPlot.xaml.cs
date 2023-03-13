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
    /// Interaction logic for RawPlot.xaml
    /// </summary>
    public partial class RawPlot : UserControl
    {
        public Plot plot { get; set; }
        private bool isRectangle { get; set; } = true;
        private double leftOffset { get; set; }
        public double PlotLeftMargin
        {
            get { return leftOffset; }
            set { leftOffset = value; }
        }
        public bool IsFrameVisible
        {
            get { return isRectangle; }
            set { isRectangle = value; }
        }
        public RawPlot()
        {
            InitializeComponent();
            plot = new Plot();
            IsFrameVisible = false;
            plot.SetRectangle(isRectangle);
            plot.HideAxis(true,true);
            plot.HideLabels(false, false);
            MainWIndow_Grid.Children.Clear();
            MainWIndow_Grid.Children.Add(plot);


        }

    }
}
