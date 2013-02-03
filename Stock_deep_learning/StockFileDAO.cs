using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Stock_deep_learning
{
    class StockFileDAO
    {
        //string m_path = "D:\\new_bhzq_v6\\T0002\\export\\";
        string m_path = "";
        string reading_name { get; set; }
        public bool check(string name,string path)
        {
            StreamReader sr;
            m_path = path;
            try
            {
                sr = new StreamReader(m_path + name + ".txt");
            }
            catch
            {
                return false;
            }
            return true;
        }
        public double[][] getData(string stockname)
        {
            List<double[]> data = new List<double[]>();
            StreamReader sr;
            try
            {
              sr = new StreamReader(m_path + stockname + ".txt");
            }
            catch
            { 
                return null;
            }
            string head = sr.ReadLine();
            string[] header = head.Split(' ');
            reading_name = header[0];
            sr.ReadLine();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] records = line.Split(',');
              

                double[] record_d = new double[6];
                for(int i=1;i<7;i++)
                {
                    record_d[i-1] = double.Parse(records[i]);
                }
                data.Add(record_d);
            }
            return (data.ToArray());

        }
        public double[][] getData(string stockname,string path)
        {
            m_path = path;
           return ( getData(stockname));
        }

        public double[][] getData(string stockname, string path, int start_date, int end_date)
        {
            List<double[]> data = new List<double[]>();
            m_path = path;
            StreamReader sr;
            try
            {
                sr = new StreamReader(m_path + stockname + ".txt");
            }
            catch
            {
                return null;
            }
            string head = sr.ReadLine();
            string[] header = head.Split(' ');
            reading_name = header[0];
            sr.ReadLine();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] records = line.Split(',');
                string[] year = records[0].Split('/');
                int y = int.Parse(year[2]);
                if (y > start_date && y < end_date)
                {
                    double[] record_d = new double[6];
                    for (int i = 1; i < 7; i++)
                    {
                        record_d[i - 1] = double.Parse(records[i]);
                    }
                    data.Add(record_d);
                }
            }
            return (data.ToArray());

        }
    }
}
