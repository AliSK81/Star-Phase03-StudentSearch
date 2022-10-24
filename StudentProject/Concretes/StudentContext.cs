using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StudentProject.Concretes
{
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentScore> Scores { get; set; }

        public string DbPath { get; }

        public StudentContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "studentaab.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");

        public void AddStudents(List<Student> students)
        {
            Students.AddRange(students);
            Save();
        }

        public void AddScores(List<StudentScore> scores)
        {
            Scores.AddRange(scores);
            Save();
        }

        private void Save()
        {
            try
            {
                base.SaveChanges();
            }
            catch (DbUpdateException) { }
        }
    }

}
