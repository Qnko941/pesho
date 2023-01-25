using HtmlAgilityPack;

namespace RealEstateApp
{
    interface IParser
    {
        IEnumerable<string> HtmlAgilityPackParse(string html);

        PricesRow ParsePricesRow(HtmlNode row);

        Price ParsePrice(string text);
    }
}