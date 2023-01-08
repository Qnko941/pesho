namespace RealEstateApp
{
    public interface IMirelaDownloader
    {
        string GetWeeklyStatistics(DateTime date);

        string GetDataHtml(DateOnly date);


    }
}