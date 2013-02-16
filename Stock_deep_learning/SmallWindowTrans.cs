using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Stock_deep_learning
{
    class SmallWindowTrans
    {
        public List<double[]> getRawFeature(int full_length, int window_size, double[][] data, int framesize)
        {
            List<double[]> result = new List<double[]>();
            // ArrayList ar = new ArrayList();


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
                double min = double.MaxValue;
                for (int window = step; window < step + window_size; window++)
                {
                    if (data[window][1] > max)
                        max = data[window][1];
                    if (data[window][2] < min)
                        min = data[window][2];
                }
                //Console.WriteLine("max:" + max.ToString());
                // Console.WriteLine("min:" + min.ToString());
                // Console.WriteLine("*");
                double block = (max - min) / framesize;
                double[] frame = new double[window_size * framesize];
                for (int window = step; window < step + window_size; window++)
                {
                    //judge if one block should be 0 or 1
                    //   for (int his = 1; his <= 4; his++)
                    // {
                    //   double zhuzi = his * block;
                    //double relative_high = ((data[window][1] - min) / (max - min)) + 1;
                    //double relative_low = ((data[window][2] - min) / (max - min)) + 1;
                    //double relative_open = ((data[window][0] - min) / (max - min)) + 1;
                    //double relative_close = ((data[window][3] - min) / (max - min)) + 1;
                    //double relative_high = ((data[window][1] - min) / (max - min)) * 100;
                    //double relative_low = ((data[window][2] - min) / (max - min)) * 100;
                    //double relative_open = ((data[window][0] - min) / (max - min)) * 100;
                    //double relative_close = ((data[window][3] - min) / (max - min)) * 100;
                    //Console.WriteLine(zhuzi.ToString());
                    //  Console.WriteLine(relative_low.ToString());
                    //  Console.ReadKey();

                    //if (zhuzi < relative_high && zhuzi > relative_low)
                    //  if (zhuzi > relative_low)
                    //frame[(window - step) * 4 + 0] = relative_open;
                    //frame[(window - step) * 4 + 1] = relative_high;
                    //frame[(window - step) * 4 + 2] = relative_low;
                    //frame[(window - step) * 4 + 3] = relative_close;
                    //  }
                    int count = 0;
                    double relative_high = (data[window][1] - min);
                    double relative_low = (data[window][2] - min);
                    //  double relative_open = (data[window][0] - min) / (max - min);
                    //   double relative_close = (data[window][3] - min) / (max - min);
                    double zhuzi = 0.0;
                    for (int his = 1; his <= framesize; his++)
                    {
                        zhuzi = his * block;

                        //    double relative_v = (data[window][4] / maxv)/((relative_high-relative_low)+1);
                        //  Console.WriteLine(zhuzi.ToString());
                        //  Console.WriteLine(relative_low.ToString());
                        //  Console.ReadKey();

                        if ((zhuzi <= relative_high && zhuzi >= relative_low) || (relative_low > (zhuzi - block) && relative_low < zhuzi) || (relative_high > (zhuzi - block) && relative_high < zhuzi))
                        //  if (zhuzi > relative_low)
                        {
                            frame[framesize * (window - step) + his - 1] = 1;
                            count++;
                        }
                        // zhuzi = 0.0;
                    }
                    //  if (count == 0)
                    //      Console.WriteLine("!!!");
                }
                //   Random rrr = new Random();
                // frame[frame.Length - 1] = rrr.Next()%2;
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
