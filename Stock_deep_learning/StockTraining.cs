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
using Accord.Math;
using System.Threading;
namespace Stock_deep_learning
{
    class StockTraining
    {
        public void train(List<double[]> final_data,int visible,int hidden,double learnning_rate)
        {
            Accord.Math.Tools.SetupGenerator(0);
                BernoulliFunction.Random = new ThreadSafeRandom(0);
                GaussianFunction.Random.SetSeed(0);
                BernoulliFunction activation = new BernoulliFunction();
                RestrictedBoltzmannMachine network = new RestrictedBoltzmannMachine(activation, visible, hidden);
                 //   RestrictedBoltzmannMachine.CreateGaussianBernoulli(visible, hidden);
                   //RestrictedBoltzmannMachine.CreateGaussianBernoulli(40, 10);
                new GaussianWeights(network).Randomize();
                network.UpdateVisibleWeights();


                ContrastiveDivergenceLearning target = new ContrastiveDivergenceLearning(network);

                target.Momentum = 0.9;
                target.LearningRate =learnning_rate;
                target.Decay = 0.001;
                double[][] inputs = final_data.ToArray();
                int batchCount = Math.Max(1, inputs.Length / 10000);

                // Create mini-batches to speed learning
                int[] groups = Accord.Statistics.Tools
                    .RandomGroups(inputs.Length, batchCount);
                double[][][] batches = inputs.Subgroups(groups);
                //int iterations = 500;
               // double[] errors = new double[iterations];
               // for (int i = 0; i < iterations; i++)
                int index = 0;
            while(true)
                {
                    foreach (double[][] ppp in batches)
                    {
                        //double err = target.RunEpoch(inputs);
                        double err = target.RunEpoch(ppp)/ppp.Length;
                        Console.WriteLine(err.ToString());
                        if (index % 1000 == 0)
                        {
                            Random r = new Random();
                            network.Save("rrr" + index.ToString() + r.Next().ToString() + ".ann");
                        }
                        index++;
                    }
                  
                 
                }

              //  double startError = errors[0];
              //  double lastError = errors[iterations - 1];
                //  Assert.IsTrue(startError > lastError);
                //Console.WriteLine(startError);
                //Console.WriteLine(lastError);
            
         //   Console.Read();
        }
    }
}
