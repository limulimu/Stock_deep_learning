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
    class FirstLayerCoding
    {
     private string m_ann;
     RestrictedBoltzmannMachine network;
        public List<double[]> Load(string ann)
        {
            m_ann = ann;
          
            network = (RestrictedBoltzmannMachine)ActivationNetwork.Load(m_ann);
            List<double[]> final_data = new List<double[]>();
            string pp = "";
#if DEBUG
           pp="e:\\data\\";
#endif
           StockFileDAO sfd = new StockFileDAO();
           SmallWindowTrans ts = new SmallWindowTrans();
            System.Threading.Tasks.Parallel.For(0, 400, i =>
            {
                string s = i.ToString("0000");
               
                double[][] data;
              //  lock (final_data)
                {
                 
                    if (sfd.check("SH60" + s, pp) != false)
                    {
                        data = sfd.getData("SH60" + s, pp);

                        //  Transform ts = new Transform();
                        // SimpleTrans ts = new SimpleTrans();
                        // VTrans ts = new VTrans();
                       
                        // SmallWindowV ts = new SmallWindowV();
                        List<double[]> ddd = ts.getRawFeature(35, 30, data, 30);
                        final_data.AddRange(coding(ddd));
                        Console.WriteLine(i.ToString());
                    }
                }
            });
            return final_data;
        }
        private List<double[]> coding(List<double[]> input)
        {
             List<double[]> result=new List<double[]>(); 
             List<double[]> temp=new List<double[]>(); 
         
            double[][] inputarray=input.ToArray();
            int index=0;
            double[] r;
            double[] tt = new double[1000];
            for (int i = 0; i < inputarray.Length; i++)
            {
                r = network.GenerateOutput(inputarray[i]);
                index++;
                temp.Add(r);
                if (index % 10 == 0)
                {
                    double[][] temparray = temp.ToArray();
                    for (int j=0; j < temparray.Length; j++)
                    {
                        for (int m = 0; m < temparray[j].Length; m++)
                        {
                            tt[j + m] = temparray[j][m];
                        }
                    }
                    result.Add(tt);
                    index = 0;
                }
            }
            return result; 

        }
    }
}
