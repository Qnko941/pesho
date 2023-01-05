using System;
using System.Net;
using System.Net.Http;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Making API Call...");
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