using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Stock_deep_learning
{
    class SmallWindowV
    {
        public List<double[]> getRawFeature(int full_length, int window_size, double[][] data, int framesize)
        {
            List<double[]> result = new List<double[]>();
            // ArrayList ar = new ArrayList();


            //double min = double.MaxValue;
            //Find the local max and min in a big_timewindow
            // for (int j = 0; j < window_size; j++)
            //{
            //    if (data[j][1] > max)
            //        max = data[j][1];
            //    if (data[j][2] < min)
            //        min = data[j][2];
            //}
            //give each block a value

            //  double block = (max - min) / 100;
            for (int step = 0; step < data.Length - window_size; step += window_size / 2)
            {
                double max = 0.0;
                for (int window = step; window < step + window_size; window++)
                {
                    if (data[window][4] > max)
                        max = data[window][4];

                }
                double block = max / framesize;
                double[] frame = new double[window_size * framesize];
                for (int window = step; window < step + window_size; window++)
                {
                    //judge if one block should be 0 or 1
                    //   for (int his = 1; his <= 4; his++)
                    // {
                    //   double zhuzi = his * block;
                    double relative_high = (data[window][4]);
                    //  double relative_high = (data[window][4] / max) *100;
                    //  Console.WriteLine(zhuzi.ToString());
                    //  Console.WriteLine(relative_low.ToString());
                    //  Console.ReadKey();

                    //if (zhuzi < relative_high && zhuzi > relative_low)
                    //  if (zhuzi > relative_low)
                    // frame[(window - step)] = relative_high;
                    //  }
                    double zhuzi = 0.0;
                    for (int his = 1; his <= framesize; his++)
                    {
                        zhuzi = his * block;

                        //    double relative_v = (data[window][4] / maxv)/((relative_high-relative_low)+1);
                        //  Console.WriteLine(zhuzi.ToString());
                        //  Console.WriteLine(relative_low.ToString());
                        //  Console.ReadKey();

                        if (relative_high > zhuzi)
                        //  if (zhuzi > relative_low)
                        {
                            frame[framesize * (window - step) + his - 1] = 1;
                            // count++;
                        }
                        // zhuzi = 0.0;
                    }

                }
                // Random rrr = new Random();
                // frame[frame.Length - 1] = rrr.Next() % 2;
                if (frame != null)
                    result.Add(frame);

                //  ar.Add(frame);

                // Console.WriteLine(((data[step][1] - min) / (max - min)).ToString());
                //     Console.ReadKey();

                // }

            }
            return (result);
        }
    }
}
