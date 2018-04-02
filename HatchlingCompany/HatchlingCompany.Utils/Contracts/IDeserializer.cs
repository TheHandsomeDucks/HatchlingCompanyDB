namespace HatchlingCompany.Utils.Contracts
{
    public interface IDeserializer<T>
    {
        T Deserialize(string json);
    }
}
