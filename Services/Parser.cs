using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using HtmlAgilityPack;

namespace RealEstateApp
{
    public class Parser : IParser
    {
        public IEnumerable<string> HtmlAgilityPackParse(string html)
        {
            HtmlDocument htmlSnippet = new HtmlDocument();
            htmlSnippet.LoadHtml(html);

            List<string> tds = new List<string>();
            var rows = htmlSnippet.DocumentNode.SelectNodes("//tr").Skip(3).Select(ParsePricesRow);
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(rows, options);
            tds.Add(json);
            return tds;
        }

        public PricesRow ParsePricesRow(HtmlNode row)
        {
            var cells = row.ChildNodes;
            return new PricesRow
            {
                Region = cells[0].InnerText,
                Room1 = ParsePrice(cells[1].InnerText),
                Room2 = ParsePrice(cells[2].InnerText),
                Room3 = ParsePrice(cells[3].InnerText),
            };
        }

        public Price ParsePrice(string text)
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
    }
}