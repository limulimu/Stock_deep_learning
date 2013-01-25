using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Stock_deep_learning
{
    class Transform
    {
        public List<double[]> getRawFeature(int full_length, int window_size,double[][] data)
        {
            List<double[]> result = new List<double[]>();
           // ArrayList ar = new ArrayList();
            if (data == null)
                return null;
            for (int i = 0; i < data.Length-full_length; i += full_length/2)
            {
                double max = 0.0;
                double min = double.MaxValue;
                //Find the local max and min in a big_timewindow
                for (int j = i; j < i+full_length; j++)
                {
                    if (data[j][1] > max)
                        max = data[j][1];
                    if (data[j][2] < min)
                        min = data[j][2];
                }
                //give each block a value
             
                double block = (max - min) / 100;
                for (int step = i; step < i + full_length - window_size; step += window_size / 2)
                {
                    double[] frame = new double[window_size * 100];
                    for (int window = step; window < step + window_size; window++)
                    {
                        //judge if one block should be 0 or 1
                        for (int his = 1; his <= 100; his++)
                        {
                            double zhuzi = his * block;
                             double relative_high = (data[window][1] - min) / (max - min);
                            double relative_low = (data[window][2] - min) / (max - min);
                          //  Console.WriteLine(zhuzi.ToString());
                          //  Console.WriteLine(relative_low.ToString());
                          //  Console.ReadKey();
                           
                            if (zhuzi < relative_high && zhuzi > relative_low)
                          //  if (zhuzi > relative_low)
                                frame[100 *(window-step) + his-1] = 1;
                        }

                    }
                    result.Add(frame);
                    
                  //  ar.Add(frame);

                   // Console.WriteLine(((data[step][1] - min) / (max - min)).ToString());
                   //     Console.ReadKey();
                    
                }
             
            }
            return (result);
        }
    }
}
