namespace HatchlingCompany.Utils.Contracts
{
    public interface IDeserializer
    {
        object Deserialize(string json);
    }
}
