namespace RealEstateApp
{
    class MirelaDownloader : IMirelaDownloader
    {
        private readonly ICacheProvider _cache;

        public MirelaDownloader(ICacheProvider cache)
        {
            _cache = cache;
        }

        public string GetWeeklyStatistics(DateTime date)
        {
            var cacheKey = $"WeeklyStatistics_{date:yyyyMMdd}";
            var result = _cache.GetValue(cacheKey);
            if (result == null)
            {
                result = "Download";
                _cache.SetValue(cacheKey, result);
            }

            return result;
        }
    }



    public class MDTest
    {
        public void Given_When_Then()
        {
            var cacheProvider = Moq.GetService<ICacheProvider>();
            cacheProvider.Setup(x => x.GetValue()).Return(null);

            var unit = new MirelaDownloader(cacheProvider.Instance);
        }
    }
}