using System.Collections.Generic;

namespace EasyPlot
{
    public class SignalValueToPoints
    {
        public List<List<double>> Convert(double[] SignalValues, double[] TimeValues)
        {
            List<double> TimePoints = new List<double>();
            List<double> SignalPoints = new List<double>();


            for (int i = 0; i < SignalValues.Length - 1; i++)
            {
                // double[] newXa;
                // double[] signalPoints = new double[100];
                if (SignalValues[i] == SignalValues[i + 1])
                {
                    if (i == 0)
                    {
                        SignalPoints.Add(SignalValues[i]);
                        TimePoints.Add(TimeValues[i]);

                        SignalPoints.Add(SignalValues[i + 1]);
                        TimePoints.Add(TimeValues[i + 1]);
                    }
                    else
                    {
                        if (i == SignalValues.Length - 2)
                        {
                            // Lasr Value Case
                            TimePoints.Add(TimeValues[i + 1]);
                            SignalPoints.Add(SignalValues[i + 1]);

                            double intervalLegth = TimePoints[1] - TimePoints[0];
                            TimePoints.Add(TimeValues[i + 1] + intervalLegth);
                            SignalPoints.Add(SignalValues[i + 1]);
                        }
                        else
                        {
                            TimePoints.Add(TimeValues[i + 1]);
                            SignalPoints.Add(SignalValues[i + 1]);
                        }

                    }
                }
                else
                {
                    if (i == 0)
                    {
                        TimePoints.Add(TimeValues[i]);
                        SignalPoints.Add(SignalValues[i]);

                        TimePoints.Add(TimeValues[i + 1]);
                        SignalPoints.Add(SignalValues[i]);

                        TimePoints.Add(TimeValues[i + 1]);
                        SignalPoints.Add(SignalValues[i + 1]);

                    }
                    else
                    {
                        if (i == SignalValues.Length - 2)
                        { // Last value case
                            TimePoints.Add(TimeValues[i + 1]);
                            SignalPoints.Add(SignalValues[i]);

                            TimePoints.Add(TimeValues[i + 1]);
                            SignalPoints.Add(SignalValues[i + 1]);

                            double intervalLegth = TimePoints[1] - TimePoints[0];
                            TimePoints.Add(TimeValues[i + 1] + intervalLegth);
                            SignalPoints.Add(SignalValues[i + 1]);
                        }
                        else
                        {
                            TimePoints.Add(TimeValues[i + 1]);
                            SignalPoints.Add(SignalValues[i]);

                            TimePoints.Add(TimeValues[i + 1]);
                            SignalPoints.Add(SignalValues[i + 1]);
                        }

                    }
                }



            }
            List<List<double>> result = new List<List<double>>();
            result.Add(TimePoints);
            result.Add(SignalPoints);
            return result;
        }
    }
}
