﻿using System;
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
using System.IO;

namespace Stock_deep_learning
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
           // StockTraining st = new StockTraining();
           // st.train(final_data, 160, 10, 0.01);
            //double[] t = new double[] {1.08988764044944,1.14044943820225,1.08988764044944,1.12359550561798,1.12359550561798,1.12921348314607,1.09550561797753,1.10112359550562,1.10112359550562,1.1123595505618,1.08988764044944,1.09550561797753,1.09550561797753,1.10112359550562,1.08988764044944,1.09550561797753,1.09550561797753,1.12359550561798,1.09550561797753,1.09550561797753};
            //double[] tt = new double[] {1.15789473684211,1.21052631578947,1.14912280701754,1.18421052631579,1.20175438596491,1.20175438596491,1.14912280701754,1.15789473684211,1.15789473684211,1.17543859649123,1.14035087719298,1.15789473684211,1.15789473684211,1.2280701754386,1.15789473684211,1.18421052631579,1.18421052631579,1.19298245614035,1.16666666666667,1.18421052631579};

        //    TestTainedNet tn = new TestTainedNet();

           // double[] d = new double[] { 0.5, 0.5, 0.5 ,0.5, 0.5, 0.5,0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5 };

            //tn.TestTrained(t, 10);
            //TestTainedNet tn = new TestTainedNet();
            //double[] tt = new double[] {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,
            //                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,
            //                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,
            //                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,
            //                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,
            //                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,
            //                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,
            //                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,
            //                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,
            //                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,
            //                            0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,
            //                            0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
            //                            0,0,0,0,0,0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
            //                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,
            //                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,
            //                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,
            //                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,
            //                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,
            //                            0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,
            //                            0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,
            //                            0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
            //                            0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
            //                            0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,
            //                            0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
            //                            0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
            //                            0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
            //                            1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
            //                            1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
            //                            0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
            //                            0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,};
           // TestTrainedDeepNet ttdn = new TestTrainedDeepNet();
            //ttdn.Test(tt, 30);
          //  tn.TestTrained(tt, 30);
           // double[] tt = { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
            //ttdn.Test(tt, 30);
            FileStream fs = new FileStream("features.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            List<double[]> dd = new List<double[]>();
            while (sr.Peek() != -1)
            {
                string[] ss = sr.ReadLine().Split(',');
                double[] ddd = new double[ss.Length-1];
                for (int i = 0; i < ddd.Length; i++)
                {
                    ddd[i] = double.Parse(ss[i]);
                }
                dd.Add(ddd);
            }
                //FirstLayerCoding ld = new FirstLayerCoding();
               // List<double[]> dd = ld.LoadL2("p151-100.ann", "p101-50.ann");
                  
                //   StockTraining st = new StockTraining();
         //   LoadData ld = new LoadData();
            DeepNetStack dns = new DeepNetStack();

         //   List<double[]> dd = ld.Load();
            dns.CreateDeepNet(dd, dd[0].Length, 0.01, 2000,1000,500,50);

          
            
             //  st.train(dd, 51, 30, 0.01);
            //foreach (double[] d in dd)
           // {
           //     tn.TestTrained(d);
           // }
          //  SaveFeatures sf = new SaveFeatures();
          //  sf.save(dd.ToArray(), "", 30,80000);
           
            Console.Read();
            
        }
    }
}
