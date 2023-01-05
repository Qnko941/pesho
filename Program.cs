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

            //napravi si date pole
            //parse html
            HttpResponseMessage response = client.GetAsync("https://www.mirela.bg/index.php?p=stats_list&price_type=1&type=1&etype=543&city_id=3&week=2006-01-02").Result;
            
            if(response.IsSuccessStatusCode) {
            string result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);

            }

        }
    }
}