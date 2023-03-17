# EasyPlot.Wpf
# EasyPlot.Wpf is Based on .Net 6.0 FrameWork and Uses Wpf.
# A plotting Library for Plotting Interractive 2D Charts.
Docuentation will be released Soon.
<Html>
  <h1>Hello</h1>
  <button></button>
  <doc>
     <?xml version="1.0"?>
    <assembly>
        <name>EasyPlot</name>
    </assembly>
    <members>
        <member name="T:EasyPlot.Plot">
            <summary>
            Interaction logic for Plot User Control.
            </summary>
            <summary>
            Plot
            </summary>
        </member>
        <member name="P:EasyPlot.Plot.X_Coordinate">
            <summary>
            Get the X-Coordinate of Pointer up to 4 Decimal Places.
            Alternate:Use GetCoordinates(Int no_of_decimalPlaces).X
            </summary>
        </member>
        <member name="P:EasyPlot.Plot.Y_Coordinate">
            <summary>
            Get the Y-Coordinate of the pointer up to 4 Decimal Places.
            Alternate:Use GetCoordinates(Int no_of_decimalPlaces).X
            </summary>
        </member>
        <member name="P:EasyPlot.Plot.IsZoomLock">
            <summary>
            Sets ot Get the ZoomLock of the Plot.
            </summary>
        </member>
        <member name="E:EasyPlot.Plot.AxisChangedEventHandler">
             <summary>
            Event Hander that triggers when the axis of the plot are changed. 
             </summary>
        </member>
        <member name="M:EasyPlot.Plot.AddGatePulse(System.Double[],System.Double[])">
            <summary>
            Adds GatePulse to the plot.
            </summary>
            <param name="xvalues"></param>
            <param name="yvalues"></param>
            <exception cref="T:System.Exception"></exception>
        </member>
        <member name="M:EasyPlot.Plot.AddScatter(System.Double[],System.Double[])">
            <summary>
            Adds Scatter Plot .
            </summary>
            <param name="xvalues"></param>
            <param name="yvalues"></param>
            <exception cref="T:System.Exception"></exception>
        </member>
        <member name="M:EasyPlot.Plot.IsGrid(System.Boolean,System.Boolean)">
            <summary>
            This Method Sets The Grid Lines to Plot
            </summary>
        </member>
        <member name="M:EasyPlot.Plot.IsXGrid(System.Boolean)">
            <summary>
            This Method Sets The Vetical-Grid Lines to Plot
            </summary>
        </member>
        <member name="M:EasyPlot.Plot.IsYGrid(System.Boolean)">
            <summary>
            This Method Sets The Horizontal-Grid Lines to Plot
            </summary>
        </member>
        <member name="M:EasyPlot.Plot.GridLineThickness(System.Double)">
            <summary>
            This Method Sets the Thickness of the Grid Lines
            Pass Double Value as a thickness parameter
            </summary>
        </member>
        <member name="M:EasyPlot.Plot.GridLineColor(System.Windows.Media.Brush)">
            <summary>
            This Method Sets the Color of the Grid Lines
            Pass Brush (Brushes.Color_Name) as a thickness parameter
            </summary>
        </member>
        <member name="M:EasyPlot.Plot.IsGridVisible(System.Boolean)">
            <summary>
            This Method Sets the Visibility of the Grid Lines.
            Pass Brush (Brushes.Color_Name) as a thickness parameter
            </summary>
        </member>
        <member name="M:EasyPlot.Plot.HideLabels(System.Boolean,System.Boolean)">
            <summary>
            This Method Sets the Visibility of X label and Y label.
            </summary>
            <param name="isXVisible"></param>
            <param name="isYVisible"></param>
        </member>
        <member name="M:EasyPlot.Plot.GridLinePattern(EasyPlot.GridLinePatternEnum)">
            <summary>
            This Method Sets the Pattern of the Grid Line .
            </summary>
            <param name="gridLinePattern"></param>
        </member>
        <member name="M:EasyPlot.Plot.PlotThickness(System.Double)">
            <summary>
            This Method Sets the Thickness of the Plot .
            Pass Double value as a parameter.
            </summary>
            <param name="Thickness"></param>
        </member>
        <member name="M:EasyPlot.Plot.Frameless(System.Boolean)">
            <summary>
            This Method Sets the the Visibility of Labels and Title of the plot.
            </summary>
            <param name="IsFrameless"></param>
        </member>
        <member name="M:EasyPlot.Plot.SetAxisLimits(System.Double,System.Double,System.Double,System.Double)">
            <summary>
            This Method Set the initial  axis-limits of the Plot
            The Plot is Displayed in this Limits .
            </summary>
            <param name="xmin"></param>
            <param name="xmax"></param>
            <param name="ymin"></param>
            <param name="ymax"></param>
        </member>
        <member name="M:EasyPlot.Plot.SetXAxisLimits(System.Double,System.Double)">
            <summary>
            This Method Sets the Initial Limits of X axis that is to be displayed.
            </summary>
            <param name="xmin"></param>
            <param name="xmax"></param>
        </member>
        <member name="M:EasyPlot.Plot.SetYAxisLimits(System.Double,System.Double)">
            <summary>
            This Method Sets the Initial Limits of Y axis that is to be displayed.
            </summary>
            <param name="ymin"></param>
            <param name="ymax"></param>
        </member>
        <member name="M:EasyPlot.Plot.HideAxis(System.Boolean,System.Boolean)">
            <summary>
            This Method Sets the Visibility of the Axes.
            </summary>
            <param name="HideX"></param>
            <param name="HideY"></param>
        </member>
        <member name="M:EasyPlot.Plot.HideXAxis(System.Boolean)">
            <summary>
            This Method Sets the Visibility of X-Axis(X axis Ticks)
            </summary>
            <param name="HideX"></param>
        </member>
        <member name="M:EasyPlot.Plot.HideYAxis(System.Boolean)">
            <summary>
            This Method Sets the Visibility of Y-Axis(Y axis Ticks)
            </summary>
            <param name="HideY"></param>
        </member>
        <member name="M:EasyPlot.Plot.SetTicks(System.Double,System.Double,System.Double,System.Double)">
            <summary>
            This Method Set the Min and Max Tick values .
            Used to control the text of Ticks
            </summary>
            <param name="Xtick_min"></param>
            <param name="Xtick_max"></param>
            <param name="Ytick_min"></param>
            <param name="Ytick_max"></param>
        </member>
        <member name="M:EasyPlot.Plot.AutoAxis">
            <summary>
            This Method Sets the Plot to fit in the Window.
            Axis Limits are automatically calculated based on the Range of Input Data.
            </summary>
        </member>
        <member name="P:EasyPlot.Plot.MatchAxis">
            <summary>
            Set true or false for the plot axes to  match to other plots.
            </summary>
        </member>
        <member name="M:EasyPlot.Plot.GetXlimits">
            <summary>
            This Method is used to Get the Current limits of the X Axis.
            </summary>
            <returns>Double[]</returns>
        </member>
        <member name="M:EasyPlot.Plot.GetYlimits">
            <summary>
            This Method is used to Get the Current limits of the Y Axis.
            </summary>
            <returns>Double[]</returns>
        </member>
        <member name="M:EasyPlot.Plot.MatchAxisLimits(System.Double[],System.Double[])">
            <summary>
            This Method matches the axis limits of the plot to the limits of source plot passed as arguments.
            </summary>
            <param name="xlimits"></param>
            <param name="ylimits"></param>
        </member>
        <member name="M:EasyPlot.Plot.SetPanningLimits(System.Double,System.Double,System.Double,System.Double)">
            <summary>
            This Method Sets the Panning Limts of the Graph.
            Panning Does not go beyond this Limits.
            </summary>
            <param name="X_left"></param>
            <param name="X_right"></param>
            <param name="Y_down"></param>
            <param name="Y_up"></param>
        </member>
        <member name="M:EasyPlot.Plot.PanningLock(System.Boolean)">
            <summary>
            This Methods Set the Panning Effect to Occur or Not.
            </summary>
            <param name="isPanning"></param>
        </member>
        <member name="M:EasyPlot.Plot.PanningLock_X(System.Boolean)">
            <summary>
            This method Locks the Panning in X-Axis Direction (Horizontal)
            </summary>
            <param name="isPanningX"></param>
        </member>
        <member name="M:EasyPlot.Plot.PanningLock_Y(System.Boolean)">
            <summary>
            This method Locks the Panning in Y-Axis Direction (Verical)
            </summary>
            <param name="isPanningY"></param>
        </member>
        <member name="M:EasyPlot.Plot.ZoomLock(System.Boolean)">
            <summary>
            This Method Sets the Functionality of Zoom.
            </summary>
            <param name="isZoomLock"></param>
        </member>
        <member name="P:EasyPlot.Plot.YLabel">
            <summary>
            Sets or Gets the Y Axis Label (String)
            </summary>
        </member>
        <member name="P:EasyPlot.Plot.XLabel">
            <summary>
             Sets or Gets the Y Axis Label (String)
            </summary>
        </member>
        <member name="P:EasyPlot.Plot.Title">
            <summary>
            Sets or Gets the Title (String)
            </summary>
        </member>
        <member name="M:EasyPlot.Plot.SetPlotLabels(System.String,System.String,System.String)">
            <summary>
            This Method Sets Title ,X-label,Y-Label of the Plot
            </summary>
            <param name="title"></param>
            <param name="xlabel"></param>
            <param name="ylabel"></param>
        </member>
        <member name="M:EasyPlot.Plot.GetCoordinates(System.Int32)">
            <summary>
            This Method Return Coordinates of the pointer .
            Pass Integer value to round off the decimal positions.
            </summary>
            <param name="Round_to"></param>
            <returns></returns>
        </member>
        <member name="M:EasyPlot.Plot.InitializeComponent">
            <summary>
            InitializeComponent
            </summary>
        </member>
        <member name="T:EasyPlot.PlotSeriesEnum">
            <summary>
            This Enum is to switch between different type of plots.
            </summary>
        </member>
        <member name="T:EasyPlot.PlotViewer">
            <summary>
            Interaction logic for PlotViewer.xaml
            Pass the wpf plot as a parameter and PlotViewer.Show() populates the graph in a new window
            </summary>
            <summary>
            PlotViewer
            </summary>
        </member>
        <member name="M:EasyPlot.PlotViewer.InitializeComponent">
            <summary>
            InitializeComponent
            </summary>
        </member>
        <member name="T:EasyPlot.RawPlot">
            <summary>
            Interaction logic for RawPlot.xaml
            </summary>
            <summary>
            RawPlot
            </summary>
        </member>
        <member name="M:EasyPlot.RawPlot.InitializeComponent">
            <summary>
            InitializeComponent
            </summary>
        </member>
        <member name="T:EasyPlot.WpfPlot">
            <summary>
            Interaction logic for WpfPlot.xaml
            </summary>
            <summary>
            WpfPlot
            </summary>
        </member>
        <member name="M:EasyPlot.WpfPlot.InitializeComponent">
            <summary>
            InitializeComponent
            </summary>
        </member>
    </members>
</doc>

  

</Html>

