namespace RealEstateApp
{
    class MirelaDownloader : IMirelaDownloader
    {
        private readonly IMirelaDownloader _downloader;

        public MirelaDownloader()
        {
        }

        public MirelaDownloader(IMirelaDownloader downloader)
        {
            _downloader = downloader;
        }

        public string GetDataHtml(DateOnly date)
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

        public string GetWeeklyStatistics(DateTime date)
        {
            return null;
        }
    }

}