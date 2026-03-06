using System.IO;
//using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LegoTestSite.FileRelated
{
    public static class SensitiveReader
    {
        private class LoginClass
        {
            public string UserID;
            public string ServerIP;
            public string Password;
            public string DatabaseName;
        }
        private static LoginClass _credentials = new();

        private static string _filePath = @"..\LegoTestSite";
        private static string _fileName = "Sensitive.json";
        private static string GetSensitiveJSONFilePath()
        {
            return $"{_filePath}\\{_fileName}";
        }

        public static void PrepLoginCredentials()
        {
            var senstitiveJsonString = File.ReadAllText(GetSensitiveJSONFilePath());

            dynamic dynObject;

            using (StreamReader r = new StreamReader(GetSensitiveJSONFilePath()))
            {
                string json = r.ReadToEnd();
                JObject jObject = JObject.Parse(json);
                dynObject = JsonConvert.DeserializeObject(jObject.ToString());
            }
            

            if (dynObject is not null)
            {
                _credentials = new LoginClass()
                {
                    UserID = dynObject.UserID,
                    ServerIP = dynObject.ServerIP,
                    Password = dynObject.Password,
                    DatabaseName = dynObject.DatabaseName
                };
            }
        }

        public static string GetUserID()
        {
            return _credentials.UserID;
        }
        public static string GetServerIP()
        {
            return _credentials.ServerIP;
        }
        public static string GetPassword()
        {
            return _credentials.Password;
        }
        public static string GetDatabaseName()
        {
            return _credentials.DatabaseName;
        }
    }
}
