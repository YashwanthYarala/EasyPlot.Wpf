using System;



namespace EasyPlot
{
    public class GenData
    {
        Random ran = new Random();
        /*  public double[] Random_CONSEC(int NoOf_DataPoints ,double spacing ,double start_point)
          {
              return DataGen.Consecutive(NoOf_DataPoints, spacing, start_point);
          } */
        public double[] Consec(int NoOf_Datapoints)
        {
            double[] values = new double[NoOf_Datapoints];
            for (int i = 0; i < NoOf_Datapoints; i++)
            {
                values[i] = i;
            }
            return values;
        }
        public bool Random_Bool()
        {
            return ran.Next(2) == 0 ? false : true;
        }
        public double[] Random_BIN(int NoOf_Datapoints)
        {
            double[] values = new double[NoOf_Datapoints];
            for (int i = 0; i < NoOf_Datapoints; i++)
            {
                values[i] = ran.Next(2);
            }
            return values;
        }
        public double[] Random_INT(int NoOf_Datapoints)
        {

            double[] values = new double[NoOf_Datapoints];
            for (int i = 0; i < NoOf_Datapoints; i++)
            {
                values[i] = ran.Next(-3000, 3000);
            }
            return values;
        }
        public double[] Random_INT_POS(int NoOf_Datapoints)
        {

            double[] values = new double[NoOf_Datapoints];
            for (int i = 0; i < NoOf_Datapoints; i++)
            {
                values[i] = ran.Next(0, 3000);
            }
            return values;
        }
        public int[] Random_INT_NEG(int NoOf_DataPoints)
        {

            int[] values = new int[NoOf_DataPoints];
            for (int i = 0; i < NoOf_DataPoints; i++)
            {
                values[i] = -ran.Next();
            }
            return values;
        }
        public double[] Sine(double[] input)
        {
            double[] OP = new double[input.Length];
            int i = 0;
            foreach (double ip in input)
            {
                double x = (ip * (Math.PI)) / 180;

                OP[i] = Math.Sin(x);
                i++;
            }

            return OP;

        }
        public double[] Cosine(double[] input)
        {
            double[] OP = new double[input.Length];
            int i = 0;
            foreach (double ip in input)
            {
                double x = (ip * (Math.PI)) / 180;

                OP[i] = Math.Cos(x);
                i++;
            }

            return OP;

        }

    }

}
