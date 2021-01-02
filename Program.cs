using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Weather
{
    class Parser
    {
       WebClient client;
        public Parser()
        {
          client = new WebClient();
        }
        public void GetAPI(string request)
        {
            int posStartW = 0;
            int posEndW = 0;
            int posDate = 0;
            float sumtemp = 0;
            int minpress=5000;
            int pospress=0;
            double[] temp = new double[39];
            string html = client.DownloadString(request);
            string[] separator = new string [] { "},{" };
            string[] html2 = html.Split(separator, StringSplitOptions.None);
            for (int i=0; i<html2.Length; i++)
            {
                posStartW = html2[i].IndexOf("\"feels_like\":") + "\"feels_like\":".Length;
                posEndW = html2[i].IndexOf(",\"temp_min\"");
                posDate = html2[i].IndexOf("dt_txt") +20;
                pospress = html2[i].IndexOf("\"pressure\":") + "\"pressure\":".Length;
                int pressure = Convert.ToInt32(html2[i].Substring(pospress, 4));
                if (Convert.ToInt32(html2[i].Substring(posDate, 2)) == 21)
                {
                   string html3 = html2[i].Substring(posStartW, posEndW - posStartW).Replace(".", ",");
                   sumtemp = sumtemp + float.Parse(html3);
                }
                if (pressure < minpress) minpress = pressure;
            }
            Console.WriteLine(sumtemp / 5);
            Console.WriteLine(minpress);
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            string request = "http://api.openweathermap.org/data/2.5/forecast?id=1485724&appid=e94c03b79a1366b1e2dd6f28a688d946";
            Parser p = new Parser();
            p.GetAPI(request);
            Console.ReadLine();
        }
    }
}
