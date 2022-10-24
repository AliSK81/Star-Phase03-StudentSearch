using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using StudentProject.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Concretes
{
    public class StudentScore : ISearchabale
    {
        public int StudentNumber { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(StudentNumber))]
        public virtual Student Student { get; set; }
        public string Lesson { get; set; }
        [Key]
        public double Score { get; set; }

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
