using StudentProject;
using System.Reflection;
using System.Runtime.Versioning;

namespace Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Processor.Instance.PrintTop3Students();

            Processor.Instance.StartConsoleSearch();
        }
    }
}