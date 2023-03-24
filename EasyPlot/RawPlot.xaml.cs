using System.Windows;
using System.Windows.Controls;

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
            plot.HideAxis(true, true);
            plot.HideLabels(false, false);
            MainWIndow_Grid.Children.Clear();
            MainWIndow_Grid.Children.Add(plot);


        }

        private void MainWIndow_Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            IsFrameVisible = false;
            plot.SetRectangle(isRectangle);
            plot.HideAxis(true, true);
            plot.HideLabels(false, false);
            plot.Height = MainWIndow_Grid.ActualHeight;
            plot.Width = MainWIndow_Grid.ActualWidth;
            MainWIndow_Grid.Children.Clear();
            MainWIndow_Grid.Children.Add(plot);
        }
    }
}
