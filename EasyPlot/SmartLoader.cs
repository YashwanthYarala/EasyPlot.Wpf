using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EasyPlot
{
    internal class SmartLoader
    {
        public SmartLoader()
        {
            
        }
        public Values GetValues(ChartStyle cs,  Dictionary<double,double> DataPairs)
        {
            Values values = new Values();

            foreach (var pair in DataPairs)
            {
                if(pair.Key<=cs.XMax && pair.Key >= cs.XMin)
                {
                    values.XAxis.Add(pair.Key);
                    values.YAxis.Add(pair.Value);
                }
            }
            
            return values;
        }
        public class Values
        {
            public List<double> XAxis { get; set; }
            public List<double> YAxis { get; set; }
            public Values() 
            {
                XAxis = new List<double>();
                YAxis = new List<double>();

            }
        }
    }
}
