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
            RestrictedBoltzmannMachine network= (RestrictedBoltzmannMachine)ActivationNetwork.Load("P30.ann");
            RestrictedBoltzmannMachine network1 = (RestrictedBoltzmannMachine)ActivationNetwork.Load("P-100.ann");
        //    RestrictedBoltzmannMachine network2 = (RestrictedBoltzmannMachine)ActivationNetwork.Load("P-50.ann");
            // double[] rr=network.Compute(input);
             double[] r0 = network.Compute(addone(input));
            // double[] r1 = network1.GenerateOutput(addone(r0));
          //   double[] r2 = network2.GenerateOutput(addone(r1));
          //  double[] rr2 = network1.GenerateInput(r1);
          //   double[] rr1 = network1.GenerateInput(cutone(rr2));
           //  double[] r = network.GenerateInput(r0);
             double[] r= network.Reconstruct(r0);

           //  foreach (double d in input)
            //     Console.Write(d.ToString() + ",");
             Console.WriteLine("*****************************************************");
             //foreach (double d in r)
             int index = 0;
             for (int i = 1; i <= r.Length; i++)
             {
                 Console.Write(r[i-1].ToString("N0") + ",");
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
        public void TestTrained(double[] input)
        {
            RestrictedBoltzmannMachine network;
            network = (RestrictedBoltzmannMachine)ActivationNetwork.Load("p.ann");
           
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
        private double[] addone(double[] input)
        {
            double[] output = new double[input.Length + 1];
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = input[i];
            }
            Random r = new Random();
            output[input.Length ] = r.Next() % 2;
            return output;
        }
        private double[] cutone(double[] input)
        {
            double[] output = new double[input.Length - 1];
            for (int i = 0; i < input.Length-1; i++)
            {
                output[i] = input[i];
            }
   
            return output;
        }
    }
}
