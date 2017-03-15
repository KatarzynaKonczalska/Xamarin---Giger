using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using Giger.Models;
using System.Threading.Tasks;

namespace Giger.WebParser
{
    public class WebParser
    {
        //public string HtmlCode { get; private set; }
        public string url = "http://www.projectfreetv.us/seriewatchstream";
        //HtmlNode w;
        //WebClient client = new WebClient();

        public async Task whereToWatch()
        {

            try
            {
                Activities.aContentUser.database.deleteAllSeasonsFromDatabase();
            }
            catch(Exception exc)
            {

            }
            foreach (var serieItem in Activities.aContentUser.database.ReadSeriesFromDatabase())
            {
                try
                {

                    var a = FindSerie(serieItem.Title);
                    var seasons = FindSeasons(a);

                    var serie = Activities.aContentUser.database.ReadSerieFromDatabase(serieItem.Title);

                    foreach (var item in seasons)
                    {
                        var href = item.InnerHtml.Split('=')[1].Split('"')[1];
                        var href2 = "http://www.projectfreetv.us/internet/" + href;

                        var seasonNumber = href.Split('/')[1].Split('-')[1].Split('.')[0];
                        var sub = new Season
                        {
                            Name = "Season " + seasonNumber,
                            Url = href2,
                            ParentId = serie.Id,
                            ParentTitle = serie.Title
                        };
                        Activities.aContentUser.database.InsertSeasonToDatabase(sub);
                    }
                }
                catch (Exception exc)
                {

                }
            }

        }



        public string FindSerie(string name)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    
                    string HtmlCode = client.DownloadString("http://www.projectfreetv.us/seriewatchstream");
                    HtmlDocument a = new HtmlDocument();
                    a.LoadHtml(HtmlCode);
                    var find = a.DocumentNode
                        .Descendants("td")
                        .Where(d => d.Attributes.Contains("class")
                        &&
                        d.Attributes["class"].Value.Contains("mnlcategorylist"));

                    //HtmlAgilityPack.HtmlNode w;

                    //Task k = new Task(() => { w = find.Where(m => m.InnerHtml.Contains(name)).SingleOrDefault(); });
                    //k.Start();

                    //name
                    var w = find.Where(m => m.InnerHtml.Contains(name)).SingleOrDefault();

                    string hrefValue = w.InnerHtml;
                    string href = hrefValue.Split('=')[1];
                    return href.Split('"')[1];
                    //zwraca linka


                }
                catch (Exception ea)
                {
                    return null;
                }
            }

        }

        public IEnumerable<HtmlNode> FindSeasons(string serie)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    string HtmlCode = client.DownloadString("http://www.projectfreetv.us" + "/" + serie);
                    HtmlDocument b = new HtmlDocument();
                    b.LoadHtml(HtmlCode);
                    var seasonNodes = b.DocumentNode
                        .Descendants("td")
                        .Where(c => c.Attributes.Contains("class")
                        &&
                        c.Attributes["class"].Value.Contains("mnlcategorylist"));
                    //var asd = 12; //inner text
                    return seasonNodes;
                }
                catch (Exception ax)
                {
                    return null;
                }
            }
        }

        public IEnumerable<HtmlNode> FindEpisodes(HtmlNode season)
        {
            var href = season.InnerHtml.Split('=')[1].Split('"')[1];
            using (WebClient client = new WebClient())
            {
                string HtmlCode = client.DownloadString("http://www.projectfreetv.us/internet/" + href);
                HtmlDocument ba = new HtmlDocument();
                ba.LoadHtml(HtmlCode);
                var episodesNodes = ba.DocumentNode.Descendants().Where(d =>
    d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("episode"));


                foreach (var item in episodesNodes)
                {
                    //item.Attributes["a"].Value.Contains("http://projectfreetv.us");
                    var a = 120;
                }
                return episodesNodes;
            }

        }


        //       <a href = "alone-2015/season-2.html" title="Alone Season 2"><b>Season 2</b></a> (14 Episodes, 
        //212  links) 

    }
}