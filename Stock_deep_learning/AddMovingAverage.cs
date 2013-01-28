using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace qa
{
    class AddMovingAverage
    {
        public double[] A_MovingAverge(double[] input, int inter, int shift)
        {
            double[] t = new double[input.Length - inter];
            double[] a = new double[input.Length - inter - shift];
            for (int i = 0; i < t.Length; i++)
            {
                double p = 0.0;
                for (int j = 0; j < inter; j++)
                {
                    p += input[i + j];
                }
                t[i] = p / (double)inter;
            }
            for (int i = 0, j = shift; j < t.Length; i++, j++)
            {
                a[i] = t[j - shift];
            }
            return a;
        }
    }
}
