using HtmlAgilityPack;
Loops.MainLoop();
class Scraper
{
    public static HtmlAgilityPack.HtmlDocument Loader(string url)
    {
        var httpClient = new HttpClient();
        var html = httpClient.GetStringAsync(url).Result;
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);
        return htmlDocument;
    }
    public static string[] URL(string url)
    {
        var URLlink = Loader(url).DocumentNode.SelectNodes("//td[@class='swimstyle']//a[@href]");
        string[] linki = new string[URLlink.Count];
        for (int i = 0; i < URLlink.Count; i++)
        {
            linki[i] = "https://www.swimrankings.net/index.php" + URLlink[i].GetAttributeValue("href", "").Replace("amp;", "");
        }
        return linki;
    }
}
class Loops
{
    public static void MainLoop()
    {
        GenderLoop("https://www.swimrankings.net/index.php?page=rankingDetail&clubId=65774&gender=1&season=-1&course=SCM&stroke=0&agegroup=0");
    }
    public static void GenderLoop(string url)
    {
        for (int i = 0 ; i < 2 ; i++)
        {
            PoolCourseLoop(url.Replace("gender=1", $"gender={i + 1}"));
        }
    }
    public static void PoolCourseLoop(string url)
    {
        for (int i = 0 ; i < 2 ; i++ )
        {
            LinksLoop((i == 0) ? url : url.Replace("course=SCM", "course=LCM"));
        }
    }
    public static void LinksLoop(string url)
    {
        string[] linki = Scraper.URL(url);
        for (int i = 0 ; i < linki.Length ; i++ )
        {
            GetMasterTime(linki[i]);
            Console.ReadKey();
        }
    }
    public static void GetMasterTime(string singleUrl)
    {
        int escape = 0, inter = 0;
        while (escape == 0)
        {
            try
            {
                string master = Scraper.Loader(singleUrl.Replace("firstPlace=1",$"firstPlace={inter*25+1}")).DocumentNode.SelectSingleNode("//a[@class='time'][sup]").InnerText;
                Console.WriteLine(master);
                escape = 1;
            }
            catch
            {
                Console.WriteLine("Na tej stronie nie było rekordu");
                inter++;
            }
        }

    }
}