using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
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
            var rows = htmlSnippet.DocumentNode.SelectNodes("//tr").Skip(3).Select(ParsePricesRow);
            var options = new JsonSerializerOptions { 
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true };
            var json = JsonSerializer.Serialize(rows, options);
            tds.Add(json);
            return tds;
        }

        private static Price ParsePrice(string text)
        {
            if (string.IsNullOrWhiteSpace(text) || text == "-")
            {
                return null;
            }
            var parsedString = text.Replace("&nbsp;", " ");
            //&euro; 22 900&euro; 477/м2,
            var priceParts = parsedString.Split("&euro; ");
            var total = double.Parse(priceParts[1].Replace(" ", ""));
            var psm = double.Parse(priceParts[2].Replace(" ", "").Replace("/м2", ""));
            //parsedString = parsedString.Replace("&euro;", "");


            return new Price
            {
                Total = total,
                Psm = psm
            };
        }
        private static PricesRow ParsePricesRow(HtmlNode row)
        {
            var cells = row.ChildNodes;
            System.Console.WriteLine(cells[0].InnerText);
            return new PricesRow
            {
                Region = cells[0].InnerText,
                Room1 = ParsePrice(cells[1].InnerText),
                Room2 = ParsePrice(cells[2].InnerText),
                Room3 = ParsePrice(cells[3].InnerText),
            };
        }
    }

    class Price
    {
        public double Total { get; set; }
        public double Psm { get; set; }
    }
    class PricesRow
    {
        public string Region { get; set; }
        public Price Room1 { get; set; }
        public Price Room2 { get; set; }
        public Price Room3 { get; set; }
    }
}
