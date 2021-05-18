using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureImageStorage.Extensions
{
    public static class TempDataExtensions
    {
        public static void PutObject<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T GetObject<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;

            tempData.TryGetValue(key, out o);

            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }
}
