using HtmlAgilityPack;
using GettingMastersRecords;
Loops.MainLoop();
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
            Console.ReadKey();
        }
    }
    public static void LinksLoop(string url)
    {
        List<string> linki = ScrapingSystem.URL(url);
        for (int i = 0 ; i < linki.Count ; i++ )
        {
            Console.WriteLine(GetMasterTime(linki[i]));
        }
    }
    public static string GetMasterTime(string singleUrl)
    {
        int escape = 0, inter = 0, helper = 0;
        string master = "";
        string distance = ScrapingSystem.Loader(singleUrl).DocumentNode.SelectSingleNode("//td[@class='titleCenter']").InnerText;
        Console.WriteLine(distance);
        if (!distance.Contains("Lap") && !(distance.Split(" ").Length == 4))
        {
            while (escape == 0)
            {
                try
                {
                    master += ScrapingSystem.Loader(singleUrl.Replace("firstPlace=1", $"firstPlace={(inter * 25) + 1}")).DocumentNode.SelectSingleNode("//a[@class='time'][sup]").InnerText;
                    if (!String.IsNullOrEmpty(master))
                    {
                        Console.WriteLine("jestem tutaj");
                        var time = ScrapingSystem.Loader(singleUrl.Replace("firstPlace=1", $"firstPlace={(inter * 25) + 1}")).DocumentNode.SelectNodes("//td[@class='time']");
                        int gaga = 0;
                        foreach (var c in time)
                        {
                            if (c.InnerText == master)
                            {
                                break;
                            }
                            gaga++;
                        }
                        string gosc = ScrapingSystem.Loader(singleUrl.Replace("firstPlace=1", $"firstPlace={(inter * 25) + 1}")).DocumentNode.SelectNodes("//td[@class='fullname']")[gaga].InnerText;
                        Console.WriteLine(gaga + " " + gosc);
                        Console.WriteLine(ListGettingSystem.BirthDate(singleUrl.Replace("firstPlace=1", $"firstPlace={(inter * 25) + 1}"), gaga));
                    }
                    escape = 1;
                }
                catch
                {
                    Console.WriteLine("Na tej stronie nie było rekordu");
                    inter++;
                }
                helper++;
            }
        }
        return master;
    }
}
class Calculator
{
    public static int EndWith(int j, double places, string s) => (j != places - 1) ? 25 : ScrapingSystem.GetAbsolutePlaces(s) - (((int)places - 1) * 25);
}
