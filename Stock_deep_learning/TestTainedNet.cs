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
    class TestTainedNet
    {
        public void TestTrained(double[] input,int linelenth)
        {
            RestrictedBoltzmannMachine network;
            network = (RestrictedBoltzmannMachine)ActivationNetwork.Load("e:\\ps.ann");
             double[] r=network.Compute(input);
           //  double[] r = network.GenerateOutput(input);
          //   double[] r = network.GenerateInput(input);
          //   double[] r= network.Reconstruct(input);

           //  foreach (double d in input)
            //     Console.Write(d.ToString() + ",");
             Console.WriteLine("*****************************************************");
             //foreach (double d in r)
             
             for (int i = 1; i <= r.Length; i++)
             {
                 Console.Write(r[i-1].ToString("N1") + ",");
               // Console.Write((r[i-1]>0.1?1:0).ToString() + ",");
                 if((i%(linelenth<r.Length?linelenth:r.Length)) ==0 && i!=0)
                     Console.WriteLine();

             }
             
        }
        public void TestTrained(double[] input)
        {
            RestrictedBoltzmannMachine network;
            network = (RestrictedBoltzmannMachine)ActivationNetwork.Load("e:\\ps.ann");
           
            double[] r = network.Compute(input);
           //   double[] r = network.GenerateOutput(input);
            //   double[] r = network.GenerateInput(input);
            //   double[] r= network.Reconstruct(input);
            double[] rr = network.Reconstruct(r);
            //  foreach (double d in input)
            //     Console.Write(d.ToString() + ",");
            Console.WriteLine("*****************************************************");
            //foreach (double d in r)
            double err = 0.0;
            for (int i = 0; i < rr.Length; i++)
            {
               // Console.Write(r[i - 1].ToString("N3") + ",");
                 Console.Write((rr[i]).ToString() + ",");
               // err += (rr[i] - input[i]) * (rr[i] - input[i]);
               // Console.Write();    

            }
            Console.WriteLine(err);

        }
    }
}
