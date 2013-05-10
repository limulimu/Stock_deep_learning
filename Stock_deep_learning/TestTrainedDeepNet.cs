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
using System.IO;

namespace Stock_deep_learning
{
    class TestTrainedDeepNet
    {
        public void Test(List<double[]> input)
        {
            DeepBeliefNetwork dn = DeepBeliefNetwork.Load("rrr512_953471786.ann");
          // double[] cc= dn.Compute(input);
          //  double[] r = dn.Reconstruct(input);
            FileStream fs = new FileStream("code.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach (double[] dd in input)
            {
                double[] r = dn.GenerateOutput(dd);
                for (int i = 0; i < r.Length; i++)
                {
                   sw.Write(r[i].ToString() + ",");
                    // Console.Write(r[i].ToString() + ",");
                    // Console.Write((r[i-1]>0.1?1:0).ToString() + ",");
                    // if((i%(linelenth<r.Length?linelenth:r.Length)) ==0 && i!=0)
                  }
                sw.WriteLine();
              //  Console.WriteLine();
            }
            sw.Close();
            fs.Close();
           
            
        }
    }
}
