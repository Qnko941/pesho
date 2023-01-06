namespace RealEstateApp
{
    interface ICacheProvider
    {
        string GetValue(string key);
    }
}