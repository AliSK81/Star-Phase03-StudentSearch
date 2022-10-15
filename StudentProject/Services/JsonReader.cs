using Newtonsoft.Json;

namespace StudentProject.Services
{

    public sealed class JsonReader
    {
        private static readonly JsonReader _instance = new();

        public static JsonReader Instance { get { return _instance; } }

        private JsonReader() { }

        public IList<T> ReadJsonList<T>(string path)
        {
            string json = File.ReadAllText(path);
            var jsonList = JsonConvert.DeserializeObject<List<T>>(json);

            if (jsonList == null)
            {
                throw new Exception("no content");
            }

            return jsonList;
        }

    }
}