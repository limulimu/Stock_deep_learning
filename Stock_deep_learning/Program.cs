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

            //TestTainedNet tn = new TestTainedNet();

            //double[] t = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            //tn.TestTrained(t, 10);
            //double[] tt = new double[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

            //tn.TestTrained(tt, 10);
            //Console.Read();

            LoadData ld = new LoadData();
            StockTraining st = new StockTraining();
            List<double[]> dd = ld.Load();
           // SaveFeatures sf = new SaveFeatures();
           // sf.save(dd.ToArray(), "", 100,1000);
            st.train(dd, 4000, 10, 0.1);
          
            
        }
    }
}