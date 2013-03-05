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
    class DeepNetStack
    {
        public void CreateDeepNet(List<double[]> final_inputs, int inputsCount, double LearningRate, params int[] hiddenNeurons)
        {
            if (final_inputs.Contains(null))
            {
                int n= final_inputs.IndexOf(null);
                return;
            }
            NoisyRectiﬁedLinearFunction activation = new NoisyRectiﬁedLinearFunction();
           // GaussianFunction activation = new GaussianFunction();
           // BernoulliFunction activation = new BernoulliFunction();
            DeepBeliefNetwork network = new DeepBeliefNetwork(activation , inputsCount, hiddenNeurons);
            double[][] inputs = final_inputs.ToArray();
            DeepBeliefNetworkLearning target = new DeepBeliefNetworkLearning(network)

            {
                Algorithm = (h, v, i) => new ContrastiveDivergenceLearning(h, v)
                {
                    LearningRate = LearningRate,
                    Momentum = 0.5,
                    Decay = 0.001,
                },
            };
            new GaussianWeights(network).Randomize();
            network.UpdateVisibleWeights();
            for (int layer = 0; layer < network.Layers.Count(); layer++)
            {

                target.LayerIndex = layer;

                double[][] layerInputs = target.GetLayerInput(inputs);
                int batchCount = Math.Max(1, layerInputs.Length / 100);

                // Create mini-batches to speed learning
                int[] groups = Accord.Statistics.Tools
                    .RandomGroups(layerInputs.Length, batchCount);
                double[][][] batches = layerInputs.Subgroups(groups);
                double error = double.MaxValue;
                int index = 0;
                double lasterror=double.MaxValue;
                while (true)
                {
                   
                    for (int i = 0; i < batches.Length; i++)
                    {


                        //bool check = false;
                        //foreach (double[] p in batches[i])
                        //{
                        //    if (p == null)
                        //    {
                        //        check = true;
                        //        break;
                        //    }
                        //}
                        //if (check)
                        //    break;
                     //   if (batches[i].Contains(null))
                       //     continue;
                        error = target.RunEpoch(batches[i]) / batches[i].Length;
                        Console.WriteLine(error .ToString());
                       
                        index++;
                        if (index % 100 == 0)
                        {
                           // if (error < layerInputs[0].Length / 8 || error < 10)
                           //     break;
                           // Random r = new Random();
                          //  network.Save("rv" + index.ToString() + r.Next().ToString() + ".ann");
                            if (Math.Abs(lasterror - error)/error  <=0.01)
                                break;
                            lasterror = error;
                        }
                    }
                    //if (error < layerInputs[0].Length / 8 || error < 10)
                    if (Math.Abs(lasterror - error) / error <=0.01)
                        break;
                }
                Random rr = new Random();
                network.Save("rrr" + index.ToString() + rr.Next().ToString() + ".ann");
            }

        }
    }
}
