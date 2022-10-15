namespace StudentProject.Concretes
{
    public class SearchQuery
    {
        public ISet<string> AllOfTheseWords { get; }
        public ISet<string> AnyOfTheseWords { get; }
        public ISet<string> NoneOfTheseWords { get; }

        public SearchQuery(string query)
        {
            AllOfTheseWords = new HashSet<string>();
            AnyOfTheseWords = new HashSet<string>();
            NoneOfTheseWords = new HashSet<string>();

            foreach (var word in query.Split(" "))
            {
                if (word.StartsWith("+"))
                {
                    AnyOfTheseWords.Add(word[1..]);
                }
                else if (word.StartsWith("-"))
                {
                    NoneOfTheseWords.Add(word[1..]);
                }
                else
                {
                    AllOfTheseWords.Add(word);
                }
            }
        }
    }

}