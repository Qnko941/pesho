namespace RealEstateApp
{
    public record class WeeklyReportDto
    {
        PricesRecord pricesRecord;
        IDictionary<string, PricesRecord> Regions;
    }
}