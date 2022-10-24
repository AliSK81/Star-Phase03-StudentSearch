using StudentProject.Concretes;

namespace StudentProject.Services
{
    public class StudentService
    {
        private readonly StudentContext _db;

        public StudentService(StudentContext db)
        {
            _db = db;
        }

        public void AddStudents(List<Student> students)
        {
            _db.Students.AddRange(students);
            _db.SaveChanges();
        }

        public void AddScores(List<StudentScore> scores)
        {
            _db.Scores.AddRange(scores);
            _db.SaveChanges();
        }

        public List<Student> FindTopN(int n)
        {
            return _db.Students.OrderByDescending(s => s.Scores.Average(ss => ss.Score)).Take(n).ToList();
        }

        public Student? FindStudent(int studentNumber)
        {
            return _db.Students.Find(studentNumber);
        }
    }
}
