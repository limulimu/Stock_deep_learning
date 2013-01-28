using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock_deep_learning
{
    class LoadData
    {
        public List<double[]> Load()
        {
           
            List<double[]> final_data = new List<double[]>();
            //string pp="e:\\data\\";
           string pp = "";
            System.Threading.Tasks.Parallel.For(0, 4000, i =>
            {
                string s = i.ToString("0000");
               
                double[][] data;
                lock (final_data)
                {
                    StockFileDAO sfd = new StockFileDAO();
                    if (sfd.check("SH60" + s, pp) != false)
                    {
                        data = sfd.getData("SH60" + s, pp);

                         Transform ts = new Transform();
                        // SimpleTrans ts = new SimpleTrans();
                        // VTrans ts = new VTrans();
                        //SmallWindowTrans ts = new SmallWindowTrans();
                      //   SmallWindowV ts = new SmallWindowV();
                         List<double[]> ddd = ts.getRawFeature(400, 40, data);
                        final_data.AddRange(ddd);
                        Console.WriteLine(i.ToString());
                    }
                }
            });
            return final_data;
        }
    }
}
