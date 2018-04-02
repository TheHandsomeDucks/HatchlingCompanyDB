using Newtonsoft.Json;
using HatchlingCompany.Utils.Contracts;

namespace HatchlingCompany.Utils
{
    public class JSONSerializer : ISerializer
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
