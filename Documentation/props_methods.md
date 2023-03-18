# **Plot():**
##  **Public Properties**[ `Plot.Property_Name` ]

### _**1.` Title_`:**_		
	Title_ Sets and Gets the Title of the Plot.
 	Type:”String”.     
### _**2. `IsReadData`:**_
 	IsReadData Sets and Gets the Coordinates Calculating Method.
 	Type:”Bool”
### _**3. `X_Coordinate`**_:
	X_Coordinate gets the x-coordinate of the pointer respective to the plot up to 4 decimal places.
 	=>This is a Get Property.
 	Type:”Double”.
 		
### **_4. `Y_Coordinate`:_**
	Y_Coordinate gets the y-coordinate of the pointer respective to the plot up to 4 decimal places.
 	=>This is a Get Property.
 	Type:”Double”.


### _**5. `IsZoomLock`**_:
	IsZoomLock Gets and Set the Zooming Property of the Plot.
 	Default Value: IsZoomLock = false;
 	example:
 		IsZoomlock = true; //Makes the Zooming Function of the plot inactive.
 	Type:”Bool”.
### _**6. `IsRectangle`**_:
### _**7. `MatchAxis`**_:
 	MatchAxis Gets and Sets the Axis Matching or Synching functionality of the plot.
	Default: MatchAxis = true;							
        Note: This Property is mainly used to sync the axis ticks with other plot as a reference.
 	AxisChangedEventHandler works only if MatchAxis = true;
	Type:”Bool”
### _**8.` YLabel`**_:
	YLabel Gets and Sets the Y Axis Label of the Plot.
 	Default: YLabel = “Y Axis”.
 	Type:”String”.
### _**9. `XLabel`**_:
 	XLabel Gets and Sets the X Axis Label of the Plot.
 	Default: XLabel = “X Axis”.
 	Type:”String”.
### _**10. `Title`**_:
	Title Sets and Gets the Title of the Plot.
	Default: Title = “Title”;
	Type:”String”.

## _**Public Methods**_:
### _**1. `AddGatePulse(double[] Xvalues,double[] Yvalues)`**_:
	AddGatePulse()Method Adds Step or Gate Type of Pulse to the Plot.	
	Used as : Plot.AddGatePulse([],[]);
	Return Type : Void.
### _**2.` AddScatter(double[] Xvalues,double[] Yvalues)`**_:
	AddScatter() Method Adds Scatter Type of chart to the Plot.	
	Used as: Plot.AddScatter(double[] _,double[] _);
	Return Type : Void.
### _**3. `IsGrid(bool IsXGrid,bool IsYGrid)`:**_
	IsGrid() Method Sets the Visibility of both Horizontal and Vertical Grid Lines  the Plot.	
	Used as : Plot.IsGrid(bool _,bool _);
	Return Type : Void
### _**4. `IsXGrid(bool IsXGrid)`**_:
	IsXGrid() Method Sets the Visibility of Grid Lines of Plot in vertical direction.
	Used as : Plot.IsXGrid(bool _);
	Return Type : Void

### _**5. `IsYGrid(bool IsYGrid)`**_:
	IsYGrid() Method Sets the Visibility of Grid Lines of Plot in Horizontal direction.
	Used as : Plot.IsYGrid(bool _);
	Return Type : Void



### _**6. `GridLineThickness(double thickness)`**_:
	GridLineThickness() Method Sets the thickness of the Grid Line of the plot.		
	Used as : Plot.GridLineThickness(double _);
	Return Type : Void.


### _**7. `GridLineColor(Brush brush)`**_:
	GridLineColor() Method Sets the Color of the Grid Line of the plot.		
	Used as : Plot.GridLineColor(Brush _);
	Return Type : Void.
### _**8. `IsGridVisible(bool IsVisible)`**_:
	IsGridVisible() Method Sets the Visibility of the Grid  of the plot.		
	Used as : Plot.IsGridVisible(bool _);
	Return Type : Void.
### _**9. `HideLabels(bool isXVisible,bool isYVisible)`**_:
	HideLabels() Method Sets the Visibility of Labels of X Axis and Y Axis.
### _**10. `SetRectangle(bool isRectangle)`**_:
### _**11. `GridLinePattern(GridLinePatternEnum gridLinePattern)`**_:
	GridLinePattern() Method Sets the Pattern of the GridLine.
        Used as:Plot.GridLinePatter(GridLinePatternEnum _);
        Return Type: void
### _**12. `PlotThickness(double Thickness)`**_:
	PlotThickness() Method Sets the thickness of the Plot Line of the plot.		
	Used as : Plot.PlotThickness(double _);
	Return Type : Void.
### _**13. `Frameless(bool IsFrameless)`**_:
	Frameless() Method sets the visibility of Frame Components that includes X-Axis tick marks, X-Axis Labels,Y-Axis Tick Marks,Y-Axis Label.
	Used as: Plot.Frameless();
	Return Type: Void.
### _**14. `SetAxisLimits(double xmin,double xmax,double ymin,double ymax)`**_:
	SetAxisLimits() Method Sets the Limits  of the X Axis and Y Axis for the initial display  of the plo	
	Used as : Plot.SetAxisLimits(double _,double_);
	Return Type : Void.
### _**15. `SetYAxisLimits(double ymin, double ymax)`**_:
	SetYAxisLimits() Method Sets the Limits  of the Y Axis for the display  of the plot.
	Used as : Plot.SetXAxisLimits(double _,double_);
	Return Type : Void.
### _**16. `SetXAxisLimits(double xmin, double xmax)`**_:
	SetXAxisLimits() Method Sets the Limits  of the X Axis for the display  of the plot.
	Used as : Plot.SetXAxisLimits(double _,double_);
	Return Type : Void.



### _**17. `HideAxis(bool HideX, bool HideY)`**_:
	HideAxis() Method Sets the Visibility of the X and Y axis of the plot.		
	Used as : Plot.HideAxis(bool _,bool _);
	Return Type : Void.
### _**18. `HideXAxis(bool HideX)`**_:
	HideXAxis() Method Sets the Visibility of the X axis of the plot.		
	Used as : Plot.HideXAxis(bool _);
	Return Type : Void.
### _**19. `HideYAxis(bool HideY)`**_:
	HideYAxis() Method Sets the Visibility of the Y axis of the plot.		
	Used as : Plot.HideYAxis(bool _);
	Return Type : Void.
### _**20. `AutoAxis()`**_:
	AutoAxis() Method Sets the Limits of the Xaxis and Yaxis of the plot based on the Range of its input data.		
	Used as : Plot.AutoAxis( );
	Return Type : Void.
### _**21. `GetXlimits()`**_:
	GetXLimits() Method Returs the Limits of the Xaxis and Yaxis of the plot.	
	Used as : Plot.GetXLimits( );
	Return Type : double[2]





### _**22. `GetYlimits()`**_:
	GetYLimits() Method Returs the Limits of the Yaxis and Yaxis of the plot.	
	Used as : Plot.GetYLimits( );
	Return Type : double[2]
### _**23. `MatchAxisLimits(double[] xlimits, double[] ylimits)`**_:
	MatchAxisLimits() Method Set the Limits of Xaxis and Yaxis limits to reference limits passed as arguments.	
	Used as : Plot.MatchAxisLimits(_,_ );
	Return Type : Void.
	Note: This method Gets Handy during utilization of AxisChangedEventHandler in syncing the plot.
### _**24. `SetPanningLimits(double X_left,double X_right,double Y_down,double Y_up)`**_:
	SetPanningLimits() Method Set the Limits for Panning .So that user cant pan the plot beyond this limits.
	Used as : Plot.SetPanningLimits(_,_ ,_,_);
	Return Type : Void.
### _**25. `PanningLock(bool isPanning)`**_:
	PanningLock() Method Set the functionality for Panning .Set whether panning is active or not.
	Default: PanningLock(false);
	Used as : Plot.PanningLock(bool _);
	Return Type : Void.
### _**26. `PanningLock_X(bool isPanningX)`**_:
        PanningLock_X() Method Set the functionality for Panning in Horizontal Direction.Set whether panning is active or not.
	Default: PanningLock_X(false);
	Used as : Plot.PanningLock_X(bool _);
	Return Type : Void.
### _**27. `PanningLock_Y(bool isPanningY)`**_:
	PanningLock_Y() Method Set the functionality for Panning in Vertical Direction.Set whether panning is active or not.
	Default: PanningLock_Y(false);
	Used as : Plot.PanningLock_Y(bool _);
	Return Type : Void.
### _**28. `ZoomLock(bool isZoomLock)`**_:
	ZoomLock() Method Set the functionality for Zoom .Set whether Zooming is active or not.
	Default: ZoomLock(false);
	Used as : Plot.ZoomLock(bool _);
	Return Type : Void.
### _**29. `SetPlotLabels(string title,string xlabel,string ylabel)`:**_
	setPlotLabels() Method Set the Labels of the plot that includes Title,X axis Label, Y axis Label 
        Used as : Plot.SetPlotLabels( _,_,_);
	Return Type : Void.





	
### _**30. `GetCoordinates(int Round_to)`**_:
	GetCoordinates() Method returns  the X and Y coordinate of pointer on the plot.
        Used as : double x = Plot.GetCoordinates().X;
	Return Type : Coordinates Obect =>obj.x,obj.y.


