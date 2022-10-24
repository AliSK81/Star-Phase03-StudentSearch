using Newtonsoft.Json;
using StudentProject.Concretes;
using StudentProject.Services;
using JsonReader = StudentProject.Services.JsonReader;

namespace StudentProject
{
    public class Processor
    {
        private static Processor? _instance;
        private readonly StudentService _studentService;
        private readonly SearchService _searchService;

        private Processor()
        {
            var students = JsonReader.Instance.ReadJsonList<Student>(Settings.StudentsPath);
            var scores = JsonReader.Instance.ReadJsonList<StudentScore>(Settings.ScoresPath);

            var db = new StudentContext();
            db.AddStudents(students);
            db.AddScores(scores);
            _studentService = new StudentService(db);


            var builder = new InvertedIndexBuilder();
            var index = builder
                .Add(students)
                .Add(scores)
                .Build();
            _searchService = new SearchService(index);
        }

        public static Processor Instance
        {
            get
            {
                _instance ??= new();
                return _instance;
            }
        }

        public List<string> FindTopNStudetns(int n)
        {
            var res = new List<string>();

            var searchResult = _studentService.FindTopN(n);

            foreach (var student in searchResult)
            {
                res.Add(JsonConvert.SerializeObject(student, Formatting.Indented));
            }

            return res;
        }

        public void PrintTop3Students()
        {
            foreach (var student in FindTopNStudetns(3))
            {
                Console.WriteLine(student);
            }
        }

        public List<string> Search(string query)
        {
            var res = new List<string>();

            var searchResult = _searchService.Find(new SearchQuery(query));

            foreach (int studentNumber in searchResult)
            {
                var student = _studentService.FindStudent(studentNumber);

                res.Add(JsonConvert.SerializeObject(student, Formatting.Indented));
            }

            return res;
        }

        public void StartConsoleSearch()
        {
            while (true)
            {
                Console.Write("Enter query: ");
                var query = Console.ReadLine();

                if (query == null || query == "") break;

                foreach (string result in Search(query))
                {
                    Console.WriteLine(result);
                }
            }
        }
    }
}
