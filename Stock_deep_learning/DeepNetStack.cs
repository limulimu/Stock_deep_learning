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
            DeepBeliefNetwork network = new DeepBeliefNetwork(inputsCount, hiddenNeurons);
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
                int batchCount = Math.Max(1, layerInputs.Length / 1000);

                // Create mini-batches to speed learning
                int[] groups = Accord.Statistics.Tools
                    .RandomGroups(layerInputs.Length, batchCount);
                double[][][] batches = layerInputs.Subgroups(groups);
                double error = double.MaxValue;
                int index = 0;
                while (error>layerInputs[0].Length/20)
                {
                    foreach (double[][] ppp in batches)
                    {
                        bool check = false;
                        foreach (double[] p in ppp)
                        {
                            if (p == null)
                                check = true;
                        }
                        if (check)
                            continue;
                        error = target.RunEpoch(ppp) / ppp.Length;
                        Console.WriteLine(error.ToString());
                        index++;
                        if (index % 10000 == 0)
                        {
                            Random r = new Random();
                            network.Save("rrr" + index.ToString() + r.Next().ToString() + ".ann");
                        }
                    }
                }
            }
          
        }
    }
}
