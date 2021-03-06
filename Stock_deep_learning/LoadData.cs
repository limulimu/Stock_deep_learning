﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Stock_deep_learning
{
    class LoadData
    {
        public List<double[]> Load()
        {

            List<double[]> final_data = new List<double[]>();
            string pp = new DirectoryInfo("../data/").FullName;
#if DEBUG
           pp="e:\\data\\";
#endif

            System.Threading.Tasks.Parallel.For(0, 400, (i) =>
            {
                string s = i.ToString("0000");

                double[][] data;
                
                lock (final_data)
                {
                    StockFileDAO sfd = new StockFileDAO();
                    if (sfd.check("SH60" + s, pp) != false)
                    {
                        data = sfd.getData("SH60" + s, pp, 1990, 2012);

                        //  Transform ts = new Transform();
                         SimpleTrans ts = new SimpleTrans();
                        // VTrans ts = new VTrans();
                       // SmallWindowTrans ts = new SmallWindowTrans();
                       // SmallWindowV ts = new SmallWindowV();
                        List<double[]> ddd;
                        if (data != null)
                        {
                            ddd = ts.getRawFeature(100, 30, data);
                            
                            final_data.AddRange(ddd);
                        }
                        Console.WriteLine(i.ToString());
                    }
                }
            });
            return final_data;
        }
    }
}
