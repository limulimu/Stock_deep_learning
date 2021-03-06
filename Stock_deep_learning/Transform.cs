﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Stock_deep_learning
{
    class Transform
    {
        public List<double[]> getRawFeature(int full_length, int window_size,double[][] data,int framesize)
        {
            List<double[]> result = new List<double[]>();
           // ArrayList ar = new ArrayList();
            if (data == null)
                return null;
            for (int i = 0; i < data.Length-full_length; i += full_length/2)
            {
                double max = 0.0;
                double min = double.MaxValue;
                double maxv = 0.0;
                //Find the local max and min in a big_timewindow
                for (int j = i; j < i+full_length; j++)
                {
                    if (data[j][1] > max)
                        max = data[j][1];
                    if (data[j][2] < min)
                        min = data[j][2];
                    if (data[j][4] > maxv)
                        maxv = data[j][4];
                }
                //give each block a value
             
                double block = (max - min) / framesize;
                for (int step = i; step < i + full_length - window_size; step += window_size / 2)
                {
                    double[] frame = new double[window_size * framesize];
                    for (int window = step; window < step + window_size; window++)
                    {
                        //judge if one block should be 0 or 1
                        for (int his = 1; his <= framesize; his++)
                        {
                            double zhuzi = his * block;
                             double relative_high = (data[window][1] - min) / (max - min);
                            double relative_low = (data[window][2] - min) / (max - min);
                            double relative_open = (data[window][0] - min) / (max - min);
                            double relative_close = (data[window][3] - min) / (max - min);
                            double relative_v = (data[window][4] / maxv)/((relative_high-relative_low)+1);
                          //  Console.WriteLine(zhuzi.ToString());
                          //  Console.WriteLine(relative_low.ToString());
                          //  Console.ReadKey();
                           
                            if ((zhuzi <= relative_high && zhuzi >= relative_low) || (relative_low>(zhuzi-block)&&relative_low<zhuzi)||(relative_high<(zhuzi+block)&&relative_high>zhuzi))
                          //  if (zhuzi > relative_low)
                                frame[framesize *(window-step) + his-1] =1;
                               // frame[100 * (window - step) + his - 1] = 1;
                            double relative_max=Math.Max(relative_open,relative_close);
                            double relative_min = Math.Min(relative_open, relative_close);
                          //  if ((zhuzi <= relative_max && zhuzi >= relative_min) || (relative_min > (zhuzi - block) && relative_min < zhuzi) || (relative_max < (zhuzi + block) && relative_max > zhuzi))
                                //  if (zhuzi > relative_low)
                              //  frame[100 * (window - step) + his - 1] += 1;
                          //  if (relative_open < relative_close)
                             //   frame[100 * (window - step) + his - 1] *= -1;
                          //  frame[100 * (window - step) + his - 1] *= relative_v*10;

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
