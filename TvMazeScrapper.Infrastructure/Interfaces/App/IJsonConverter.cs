namespace TvMazeScrapper.Infrastructure.Interfaces.App
{
    public interface IJsonConverter
    {
        T Deserialize<T>(string stringToDeserialize, string dateTimeFormat = null);
        string Serialize(object objectToSerialize);
    }
}