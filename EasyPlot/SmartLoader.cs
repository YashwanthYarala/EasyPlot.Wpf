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
        public Values GetValues(ChartStyle cs, double[] xval, double[] yval)
        {
            Values values = new Values();
            for(int i = 0; i < xval.Length; i++)
            {
                if (xval[i] <= cs.XMax+20 && xval[i] >= cs.XMin-20)
                {
                    values.XAxis.Add(xval[i]);
                    values.YAxis.Add(yval[i]);
                }
            }
            //foreach (var pair in DataPairs)
            //{
            //    if(pair.Key<=cs.XMax && pair.Key >= cs.XMin)
            //    {
            //        values.XAxis.Add(pair.Key);
            //        values.YAxis.Add(pair.Value);
            //    }
            //}
            
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
