using StudentProject.Abstracts;

namespace StudentProject.Concretes
{

    public class Student : ISearchabale
    {
        public int StudentNumber { get; }
        public string FirstName { get; }
        public string LastName { get; }

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