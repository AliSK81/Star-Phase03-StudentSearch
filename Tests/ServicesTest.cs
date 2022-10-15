
using NSubstitute;
using StudentProject;
using StudentProject.Concretes;
using StudentProject.Services;

namespace ServicesTest
{
    public class JsonReaderTest
    {

        [Fact]
        public void ReadStudentListTest()
        {
            JsonReader.Instance.ReadJsonList<Student>(Constants.StudentsPath);
        }

        [Fact]
        public void ReadScoresListTest()
        {
            JsonReader.Instance.ReadJsonList<StudentScore>(Constants.ScoresPath);
        }

        [Fact]
        public void StudentsFileNotFoundExeption()
        {
            Assert.Throws<FileNotFoundException>(() => JsonReader.Instance.ReadJsonList<Student>(Constants.StudentsPath + ".bak"));
        }

        [Fact]
        public void ScoresFileNotFoundException()
        {
            Assert.Throws<FileNotFoundException>(() => JsonReader.Instance.ReadJsonList<StudentScore>(Constants.ScoresPath + ".bak"));
        }
    }

    public class SearchServiceTest
    {
        [Theory]
        [InlineData("ali", 1, 3)]
        [InlineData("ali ebrahimi", 3)]
        [InlineData("ali +ebrahimi", 1, 2, 3, 5)]
        [InlineData("ebrahimi -2", 3, 5)]

        public void SearchQueryTest(string query, params int[] expectedResult)
        {
            var invertedIndex = Substitute.For<InvertedIndex>();
            SearchService searchService = new SearchService(invertedIndex);

            invertedIndex.Get("ali").Returns(new HashSet<int>() { 1, 3 });
            invertedIndex.Get("ebrahimi").Returns(new HashSet<int>() { 2, 3, 5 });
            invertedIndex.Get("1").Returns(new HashSet<int>() { 1 });
            invertedIndex.Get("2").Returns(new HashSet<int>() { 2 });
            invertedIndex.Get("3").Returns(new HashSet<int>() { 3 });


            var searchResult = searchService.Find(new SearchQuery(query)).ToArray();
            Array.Sort(searchResult);

            Assert.Equal(expectedResult, searchResult);
        }

        [Fact]
        public void NoResultException()
        {
            var invertedIndex = Substitute.For<InvertedIndex>();
            SearchService searchService = new SearchService(invertedIndex);

            Assert.Throws<Exception>(() => searchService.Find(new SearchQuery("ebrahimi")));
        }

    }
}