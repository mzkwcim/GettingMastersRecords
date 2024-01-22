using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingMastersRecords
{
    internal class ScrapingSystem
    {
        public static double GetPlacesModulo25Celling(string s) => Math.Ceiling(Convert.ToInt32(ScrapingSystem.Loader(s).DocumentNode.SelectNodes("//td[@class='navigation']")[ScrapingSystem.Loader(s).DocumentNode.SelectNodes("//td[@class='navigation']").Count - 1].InnerText.Split(" ")[4]) / 25.0);
        public static int GetAbsolutePlaces(string s) => Convert.ToInt32(ScrapingSystem.Loader(s).DocumentNode.SelectNodes("//td[@class='navigation']")[ScrapingSystem.Loader(s).DocumentNode.SelectNodes("//td[@class='navigation']").Count - 1].InnerText.Split(" ")[4]);
        public static HtmlAgilityPack.HtmlDocument Loader(string url)
        {
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            return htmlDocument;
        }
        public static List<string> URL(string url)
        {
            var URLlink = Loader(url).DocumentNode.SelectNodes("//td[@class='swimstyle']//a[@href]");
            List<string> linki = new List<string>();
            for (int i = 0; i < URLlink.Count; i++)
            {
                linki.Add("https://www.swimrankings.net/index.php" + URLlink[i].GetAttributeValue("href", "").Replace("amp;", ""));
            }
            return linki;
        }
    }
}
