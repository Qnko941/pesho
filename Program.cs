using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RealEstateApp
{
    class Program
    {

        static async Task Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<IMirelaProvider, MirelaProvider>();
        services.AddTransient<IHtmlDownloader, HtmlDownloader>();
        services.AddTransient<IParser, Parser>();
    })
    .Build();

            var downloader = host.Services.GetService<IMirelaProvider>();
            downloader.GetWeeklyStatistics();









            DateOnly startDate = new DateOnly(2006, 01, 02);
            DateOnly endDate = new DateOnly(2023, 01, 19);

            List<IEnumerable<String>> allHtmls = IHtmlDownloader.DownloadAllHtmls(startDate, endDate);



        }


    }
}
