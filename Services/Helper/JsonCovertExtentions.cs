using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ManagementServices.Helper
{
    public static class JsonCovertExtentions
    {
        public static List<T> Deserialize<T>(this List<T> list, string pathFile)
        {
            string strJson = File.ReadAllText(pathFile);
            var dictionaries = JsonConvert.DeserializeObject<Dictionary<string, T>>(strJson);

            foreach (var item in dictionaries)
            {
                list.Add(item.Value);
            }
            return list;
        }
    }
}
