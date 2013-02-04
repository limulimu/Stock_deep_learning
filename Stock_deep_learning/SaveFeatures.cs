using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Stock_deep_learning
{
    class SaveFeatures
    {
        public void save(double[][] input,string path,int linlenth)
        {
            if (input == null)
                return;
            FileStream filename = new FileStream(path+"features.txt", FileMode.OpenOrCreate); 
            StreamWriter sw = new StreamWriter(filename);
            //int index = 0;
            for(int j=0;j<input.Length;j++)
            {
                StringBuilder sb = new StringBuilder();
               
                for(int i=0;i<input[j].Length;i++)
                {
                    sb.Append(input[j][i].ToString()+",");
                   
                   
                    if ((i%linlenth==0 && i!=0) || i==input[j].Length-1)
                    {
                        sw.WriteLine(sb.ToString());
                        sb.Clear();
                     
                    }
                }
              //  sw.WriteLine((++index).ToString());
            }
            sw.Close();
        }
        public void save(double[][] input, string path, int linlenth,int writinglenth)
        {
            if (input == null)
                return;
            FileStream filename = new FileStream(path + "features.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(filename);
            int index = 0;
            for (int j = 0; j < writinglenth; j++)
            {
                StringBuilder sb = new StringBuilder();
                try
                {
                    for (int i = 0; i < input[j].Length; i++)
                    {
                        sb.Append(input[j][i].ToString() + ",");
                        index++;
                        if (index == linlenth)
                        {
                            sw.WriteLine(sb.ToString());
                            sb.Clear();
                            index = 0;

                        }

                        //  if ((i % (linlenth-1) == 0 && i != 0) || i == input[j].Length - 1)

                    }
                }
                catch
                { continue; }
                //  sw.WriteLine((++index).ToString());
            }
            sw.Close();
        }
    }
}
