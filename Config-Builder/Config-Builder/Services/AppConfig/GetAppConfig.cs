using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Config_Builder.Services.AppConfig
{
    public class GetAppConfig
    {
        public static string JsonFromApp()
        {
            string appSets;

            using (var read = new StreamReader("appsettings.json"))
            {
                appSets = read.ReadToEnd();
            }

            return appSets;
        }

        public static string JsonFromDatabase()
        {
            var dummy = new
            {
                JWT = new
                {
                    Key = "Dummy Key",
                    Issuer = "Dummy Issuer",
                },
                SecretKey = "Ting Tong"
            };

            var dbSets = JsonConvert.SerializeObject(dummy, Formatting.Indented);

            return dbSets;
        }

        public static string JsonMerger()
        {
            #region App Settings JSON File Read and Parse to JObject (JSON Object)

            JObject appSetsJObject;

            using (var read = new StreamReader("appsettings.json"))
            {
                var appSets = read.ReadToEnd();
                appSetsJObject = JObject.Parse(appSets);
            }

            #endregion App Settings JSON File Read and Parse to JObject (JSON Object)

            #region Database Settings JSON File Read and Parse to JObject (JSON Object)

            var dummy = new
            {
                JWT = new
                {
                    Key = "Dummy Key",
                    Issuer = "Dummy Issuer",
                },
                SecretKey = "Ting Tong"
            };

            var dbSets = JsonConvert.SerializeObject(dummy, Formatting.Indented);
            var dbSetsJObject = JObject.Parse(dbSets);

            #endregion Database Settings JSON File Read and Parse to JObject (JSON Object)

            #region Merge App and Database Settings JSON Files

            appSetsJObject.Merge(dbSetsJObject, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });

            var mergedJson = JsonConvert.SerializeObject(appSetsJObject, Formatting.Indented);

            #endregion Merge App and Database Settings JSON Files

            return mergedJson;
        }
    }
}