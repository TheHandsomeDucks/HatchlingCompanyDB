using HatchlingCompany.Utils.Contracts;
using Newtonsoft.Json;

namespace HatchlingCompany.Utils
{
    public class JSONDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
