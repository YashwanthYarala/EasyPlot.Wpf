using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PlottingWpf
{
    public class DataCollection
    {
        public List<DataSeries> DataList { get; set; }
        public DataCollection() 
        {
            DataList = new List<DataSeries>();
        }

        public void AddLines(ChartStyle cs)
        {

            int j = 0;
            
            foreach (DataSeries s in DataList)
            {
                if(s.SeriesName == "DefaultSeries")
                {
                    s.SeriesName = "DataSeries" + j.ToString();
                }
                s.AddLinePattern();
                for (int i = 0; i < s.LineSeries.Points.Count; i++)
                {
                    s.LineSeries.Points[i] =cs.NormalizePoint(s.LineSeries.Points[i]);

                }
                cs.ChartCanvas.Children.Add(s.LineSeries);
                j++;
            }
        }
    }
}
