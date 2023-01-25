using HtmlAgilityPack;

namespace RealEstateApp
{
    class HtmlDownloader : IHtmlDownloader
    {
        public List<IEnumerable<string>> downloadAllHtmls(DateOnly currentDate, DateOnly endDate)
        {

            using var client = new HttpClient();
            MirelaDownloader mirelaDownloader = new MirelaDownloader();

            List<IEnumerable<String>> allReportsList = new List<IEnumerable<String>>();
            while (currentDate < endDate)
            {
                HttpResponseMessage response = client.GetAsync("https://www.mirela.bg/index.php?p=stats_list&price_type=1&type=1&etype=543&city_id=3&week=" + currentDate).Result;
                var parsedDataForCurrentDate = HtmlAgilityPackParse(mirelaDownloader.GetDataHtml(currentDate));
                currentDate = currentDate.AddDays(7);
                allReportsList.Add(parsedDataForCurrentDate);
            }
            return allReportsList;
        }
    }
}