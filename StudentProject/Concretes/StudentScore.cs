using StudentProject.Abstracts;

namespace StudentProject.Concretes
{
    public class StudentScore : ISearchabale
    {
        public int StudentNumber { get; }
        public string Lesson { get; }
        public double Score { get; }

        public StudentScore(int studentNumber, string lesson, double score)
        {
            StudentNumber = studentNumber;
            Lesson = lesson;
            Score = score;
        }

        public int GetKey()
        {
            return StudentNumber;
        }

        public string GetValue()
        {
            return StudentNumber + " " + Lesson + " " + Score;
        }
    }
}
