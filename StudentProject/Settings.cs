using System.Reflection;

namespace StudentProject
{
    public class Settings
    {
        public static string ResDir { get; set; } = Assembly.GetExecutingAssembly().Location + @"\..\Resources\";
        public static string StudentsPath { get; set; } = ResDir + "students.json";
        public static string ScoresPath { get; set; } = ResDir + "scores.json";
        public static string Delim { get; set; } = " ";
    }

}