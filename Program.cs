using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using HtmlAgilityPack;

namespace RealEstateApp
{
    class Program
    {

        // public WeeklyReportDto GetData(DateOnly date)
        // {
        //     MirelaDownloader mirelaDownloader = new MirelaDownloader();
        //     var dataFileName = date.ToString("yyyy-MM-dd") + ".json";
        //     if (File.Exists(dataFileName))
        //     {
        //         var content = File.ReadAllText(dataFileName);
        //         return JsonSerializer.Deserialize<WeeklyReportDto>(content);
        //     }

        //     var html = mirelaDownloader.GetDataHtml(date);
        //     var result = Parse(html);

        //     var jsonContent = JsonSerializer.Deserialize<WeeklyReportDto>(result);
        //     File.WriteAllText(dataFileName, jsonContent);
        //     return result;
        // }

        //# Setup DI
        //#Iterate all dates
        //#For each date get html and save it locally.
        //#make Model and DTO class(es)
        //#Parse html
        //#Make report based on model
        //#Export to csv
        static void Main(string[] args)
        {
            using var client = new HttpClient();

            DateOnly date = new DateOnly(2006, 01, 02);
            Console.WriteLine(date);
            MirelaDownloader mirelaDownloader = new MirelaDownloader();


            HttpResponseMessage response = client.GetAsync("https://www.mirela.bg/index.php?p=stats_list&price_type=1&type=1&etype=543&city_id=3&week=" + date).Result;
            var a = HtmlAgilityPackParse(mirelaDownloader.GetDataHtml(date));

            foreach (var item in a)
            {
                Console.WriteLine(item);
            }

        }

        public static IEnumerable<string> HtmlAgilityPackParse(string html)
        {
            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(html);

            List<string> tds = new List<string>();

            foreach (HtmlNode link in htmlSnippet.DocumentNode.SelectNodes("//tr").Skip(3))
            {

                var a = link.InnerText.Split("&euro;&nbsp;");
                foreach (var item in a)
                {
                    Console.WriteLine(item);
                }
                // var b = link.SelectNodes("//td");

                // foreach (var item in b)
                // {
                //     Console.WriteLine(item.InnerText);
                // }
                // var arr = link.InnerText.Split("euro;");

                // if (arr.Length > 2)
                // {
                //     foreach (var item in arr)
                //     {
                //         Console.WriteLine(item);
                //     }
                // }
                //tds.Add(att.Value);
            }
            return tds;
        }
    }
}