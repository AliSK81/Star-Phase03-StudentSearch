using StudentProject.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace StudentProject.Concretes
{

    public class Student : ISearchabale
    {
        [Key]
        public int StudentNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual List<StudentScore> Scores { get; } = new();

        public Student(int studentNumber, string firstName, string lastName)
        {
            StudentNumber = studentNumber;
            FirstName = firstName;
            LastName = lastName;
        }

        public int GetKey()
        {
            return StudentNumber;
        }

        public string GetValue()
        {
            return StudentNumber + " " + FirstName + " " + LastName;
        }
    }

}