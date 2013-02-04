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
  //   private string m_ann;
     RestrictedBoltzmannMachine network;
        RestrictedBoltzmannMachine network2;
        public List<double[]> Load(string firstLayer,string secondLayer)
        {
         //   m_ann = firstLayer;
          
            network = (RestrictedBoltzmannMachine)ActivationNetwork.Load(firstLayer);
            network2 = (RestrictedBoltzmannMachine)ActivationNetwork.Load(secondLayer);
            List<double[]> final_data = new List<double[]>();
            string pp = "";
#if DEBUG
           pp="e:\\data\\";
#endif
           StockFileDAO sfd = new StockFileDAO();
           SmallWindowTrans ts = new SmallWindowTrans();
            System.Threading.Tasks.Parallel.For(0, 4000, i =>
            {
                string s = i.ToString("0000");
               
                double[][] data;
              //  lock (final_data)
                {
                 
                    if (sfd.check("SH60" + s, pp) != false)
                    {
                        data = sfd.getData("SH60" + s, pp, 1990, 2012);

                        //  Transform ts = new Transform();
                        // SimpleTrans ts = new SimpleTrans();
                        // VTrans ts = new VTrans();
                       
                        // SmallWindowV ts = new SmallWindowV();
                        if (data != null)
                        {
                            List<double[]> ddd = ts.getRawFeature(35, 30, data, 30);
                            //final_data.AddRange(SecondeLayercoding(ddd));

                            final_data.AddRange(rawcoding(ddd));
                        }
                        Console.WriteLine(i.ToString());
                    }
                }
            });
            return final_data;
        }
        public List<double[]> LoadL2(string firstLayer, string secondLayer)
        {
            //   m_ann = firstLayer;

            network = (RestrictedBoltzmannMachine)ActivationNetwork.Load(firstLayer);
            network2 = (RestrictedBoltzmannMachine)ActivationNetwork.Load(secondLayer);
            List<double[]> final_data = new List<double[]>();
            string pp = "";
#if DEBUG
            pp = "e:\\data\\";
#endif
            StockFileDAO sfd = new StockFileDAO();
            SmallWindowTrans ts = new SmallWindowTrans();
            System.Threading.Tasks.Parallel.For(0, 4000, i =>
            {
                string s = i.ToString("0000");

                double[][] data;
                //  lock (final_data)
                {

                    if (sfd.check("SH60" + s, pp) != false)
                    {
                        data = sfd.getData("SH60" + s, pp, 2000, 2012);

                        //  Transform ts = new Transform();
                        // SimpleTrans ts = new SimpleTrans();
                        // VTrans ts = new VTrans();

                        // SmallWindowV ts = new SmallWindowV();
                        if (data != null)
                        {
                            List<double[]> ddd = ts.getRawFeature(35, 30, data, 30);
                            final_data.AddRange(SecondeLayercoding(ddd));

                           // final_data.AddRange(coding(ddd));
                        }
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
        private List<double[]> rawcoding(List<double[]> input)
        {
            List<double[]> result = new List<double[]>();
            List<double[]> temp = new List<double[]>();

            double[][] inputarray = input.ToArray();
           // int index = 0;
            double[] r=new double[network.Hidden.Neurons.Count()+1];
           // double[] tt = new double[1000];
            for (int i = 0; i < inputarray.Length; i++)
            {
               double[] first_reconstrunction = network.GenerateOutput(inputarray[i]);
                if(first_reconstrunction!=null)
                {
               for (int j = 0; j < r.Length-2; j++)
                   r[j] = first_reconstrunction[j];
                  Random rrr = new Random();
                  r[r.Length - 1] = rrr.Next() % 2;
               
                    result.Add(r);
            }
                  
            }
            return result;

        }
        public List<double[]> SecondeLayercoding(List<double[]> input)
        {
            List<double[]> result = new List<double[]>();
            List<double[]> temp = new List<double[]>();

            double[][] inputarray = input.ToArray();
            int index = 0;
            double[] r;
            double[] tt = new double[1000];
            double[] rsecond=new double[101];
            double[] rr;
            int label=0;
            for (int i = 0; i < inputarray.Length-1; i++)
            {
                r = network.GenerateOutput(inputarray[i]);
               
                index++;
                temp.Add(r);
                if (index % 10 == 0)
                {
                    double[][] temparray = temp.ToArray();
                    label = checklable(inputarray[i + 1]);
                    for (int j = 0; j < temparray.Length; j++)
                    {
                        for (int m = 0; m < temparray[j].Length; m++)
                        {
                            tt[j + m] = temparray[j][m];
                        }
                    }
                    rr= network2.GenerateOutput(tt);
                    for(int m=0;m<rr.Length;m++)
                    {
                        rsecond[m]=rr[m];
                    }
                    rsecond[100]=label;
                    result.Add(rsecond);
                    index = 0;
                }
            }
            return result;

        }


        int checklable(double[] input)
        {
            int min=0;
            for(int i=450;i<480;i++)
            {
                if(input[i]==1)
                {
                    min=i-450;
                    break;
                }
            }
            int max=0;
              for(int i=870;i<900;i++)
            {
                if(input[i]==1)
                {
                    max=i-870;
                    break;
                }
            }
              if (max - min > 10)
                  return 1;
              max = 0;
              min = 0;
              for (int i = 479; i > 449; i--)
              {
                  if (input[i] == 1)
                  {
                      min = 479-i;
                      break;
                  }
              }
              for (int i = 899; i >869; i--)
              {
                  if (input[i] == 1)
                  {
                      max = 899-i;
                      break;
                  }
              }
              if (max-min > 10)
                  return -1;
              return 0;
        }
    }
}
