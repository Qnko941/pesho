using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace RealEstateTools
{
    class Program
    {
 

        public class MirelaDownloader : IMirelaDownloader
        {
            public string GetWeeklyStatistics(DateTime date)
            {
                return "";
            }
        }


        public record class PricesRecord
        {
            public double Price1Total { get; set; }
            public double Price1PerSquare { get; set; }
            public double Price2 { get; set; }
            public double Price3 { get; set; }
        }

        public record class WeeklyReportDto
        {
            public IDictionary<string, PricesRecord> Regions;
        }



        private WeeklyReportDto GetData(DateTime date)
        {
            // var dataFileName = date.ToString("yyyy-MM-dd") + ".json";
            // if (File.Exists(dataFileName))
            // {
            //     var content = File.ReadAllText(dataFileName);
            //     return JsonSerializer.Deserialize<WeeklyReportDto>(content);
            // }

            var html = GetDataHtml(date);
            var result = Parse(html);

            // var jsonContent = JsonSerializer.Deserialize(result);
            // File.WriteAllText(dataFileName, jsonContent);
            return result;
        }

        private string GetDataHtml(DateTime date)
        {
            var dataFileName = date.ToString("yyyy-MM-dd") + ".html";
            if (File.Exists(dataFileName))
            {
                return File.ReadAllText(dataFileName);
            }

            using var client = new HttpClient();
            using var response = client.GetAsync("https://www.mirela.bg/index.php?p=stats_list&price_type=1&type=1&etype=543&city_id=3&week=" + date).Result;
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            File.WriteAllText(dataFileName, content);
            return content;
        }

        private WeeklyReportDto Parse(string html)
        {
            return null;
        }

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

            HttpResponseMessage response = client.GetAsync("https://www.mirela.bg/index.php?p=stats_list&price_type=1&type=1&etype=543&city_id=3&week=" + date).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                List<string> regions = getAllRegions(result);
                regions.ForEach(Console.WriteLine);
            }

        }

        static List<string> getAllRegions(string result)
        {
            List<string> regions = new List<string>();
            string[] subStrings = result.Split("<span class=\"style6\">");

            for (int i = 1; i < subStrings.Length; i++)
            {
                string input = subStrings[i];
                int index = input.IndexOf("<");
                input = input.Substring(0, index);

                regions.Add(input);
            }
            return regions;
        }
    }
}