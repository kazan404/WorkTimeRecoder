using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WorkTimeRecoder
{
    static class JsonTransrater
    {
        public static string SerializeToJson(object serializeObject)
        {
            return JsonConvert.SerializeObject(serializeObject);
        }

        public static T DesirializeFromJson<T>(string desirializeJsonData)
        {
            return JsonConvert.DeserializeObject<T>(desirializeJsonData);
        }
    }
}
