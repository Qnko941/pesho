using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using HtmlAgilityPack;

namespace RealEstateApp
{
    class Program
    {

        static void Main(string[] args)
        {

            DateOnly startDate = new DateOnly(2006, 01, 02);
            DateOnly endDate = new DateOnly(2023, 01, 19);
            List<IEnumerable<String>> allHtmls = downloader.downloadAllHtmls(startDate, endDate);



        }


    }
}
