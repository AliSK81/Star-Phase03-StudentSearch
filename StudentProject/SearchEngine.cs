using Newtonsoft.Json;
using StudentProject.Abstracts;
using StudentProject.Concretes;
using StudentProject.Services;
using JsonReader = StudentProject.Services.JsonReader;

namespace StudentProject
{
    public class SearchEngine
    {

        private static SearchEngine _instance;

        public static SearchEngine Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new();
                    _instance.Init();
                }
                return _instance;
            }
        }

        private SearchEngine() { }

        private readonly List<ISearchabale> _searchabales = new();
        private SearchService _searchService { get; set; }

        private void Init()
        {
            var students = JsonReader.Instance.ReadJsonList<Student>(Constants.StudentsPath);
            var scores = JsonReader.Instance.ReadJsonList<StudentScore>(Constants.ScoresPath);

            _searchabales.AddRange(students);
            _searchabales.AddRange(scores);

            var index = new InvertedIndexBuilder()
                .Add(students)
                .Add(scores)
                .Build();

            index.Print();

            _searchService = new SearchService(index);
        }

        public void Run()
        {
            while (true)
            {
                Console.Write("Enter query: ");
                var query = Console.ReadLine();

                if (query == "") break;

                try
                {
                    var searchResult = _searchService.Find(new SearchQuery(query));

                    foreach (int key in searchResult)
                    {
                        var resultObj = _searchabales.Where(s => s.GetKey() == key);
                        Console.WriteLine(JsonConvert.SerializeObject(resultObj));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}