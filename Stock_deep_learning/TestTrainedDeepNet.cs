using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Neuro.Networks;
using Accord.Neuro.Neurons;
using Accord.Neuro.ActivationFunctions;
using Accord.Neuro.Learning;
using Accord.Neuro;
using AForge;
using AForge.Neuro;
using Accord.Math;
using System.Threading;
namespace Stock_deep_learning
{
    class TestTrainedDeepNet
    {
        public void Test(double[] input,int linelenth)
        {
            DeepBeliefNetwork dn = DeepBeliefNetwork.Load("rrr600779429327.ann");
           double[] cc= dn.Compute(input);
            double[] r = dn.Reconstruct(cc);
            int index = 0;
            for (int i = 1; i <= r.Length; i++)
            {
                Console.Write(r[i - 1].ToString("N0") + ",");
                // Console.Write((r[i-1]>0.1?1:0).ToString() + ",");
                // if((i%(linelenth<r.Length?linelenth:r.Length)) ==0 && i!=0)
                index++;
                if (index == linelenth)
                {
                    Console.WriteLine();
                    index = 0;
                }
            }
        }
    }
}
