using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
namespace SimpleCrawler
{
    class Program
    {
        private ConcurrentDictionary<string, bool> dic = new ConcurrentDictionary<string, bool>();
        private ConcurrentQueue<string> que = new ConcurrentQueue<string>();
        private int count = 0;
        static void Main(string[] args)
        {
            Program myCrawler = new Program();

            string startUrl = "https://www.cnblogs.com/dstang2000/";
            if (args.Length >= 1) startUrl = args[0];
            myCrawler.que.Enqueue(startUrl);
            myCrawler.myCraw();
        }
        private void myCraw()
        {
            Console.WriteLine("开始爬行了....");
            List<Task> ts = new List<Task>();
            while (count<=25)
            {
                string outstring;               
                que.TryDequeue(out outstring);
                if (outstring != null)
                    ts.Add(Task.Run(() => DP(outstring)));  
            }
            Task.WaitAll(ts.ToArray());
            Console.WriteLine("爬行结束");
        }
        public void DP(string url)
        {
            if (count >= 25||dic.ContainsKey(url)) return;
            string html;
            Console.WriteLine("爬行" + url + "页面!");
            count++;
            dic[url] = true;
            Console.WriteLine("count++    count=" + count);
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                html = webClient.DownloadString(url);

                string fileName = count.ToString();
                File.WriteAllText(fileName, html, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                html = "";
            }
            string strRef = @"(href|HREF)[]*=[]*[""'][^""'#>]+[""']";
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                strRef = match.Value.Substring(match.Value.IndexOf('=') + 1).Trim('"', '\\', '#', ' ', '>');
                if (strRef.Length == 0) continue;
                else if (!dic.ContainsKey(strRef)&&count<=25)
                    que.Enqueue(strRef);
            }
        }        
    }
}
