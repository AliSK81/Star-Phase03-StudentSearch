using StudentProject.Abstracts;
using StudentProject.Concretes;

namespace StudentProject.Services
{
    public class SearchService
    {
        private readonly InvertedIndex _index;

        public SearchService(InvertedIndex index)
        {
            _index = index;
        }

        public HashSet<int> Find(SearchQuery query)
        {
            HashSet<int> searchResult = new HashSet<int>();

            FindAllOfTheseWords(query, searchResult);
            FindAnyOfTheseWords(query, searchResult);
            RemoveNoneOfTheseWords(query, searchResult);

            if (searchResult.Count == 0)
            {
                throw new Exception("No result.");
            }

            return searchResult;
        }

        private void FindAllOfTheseWords(SearchQuery query, ISet<int> searchResult)
        {
            foreach (string word in query.AllOfTheseWords)
            {
                if (searchResult.Count == 0)
                {
                    searchResult.UnionWith(_index.Get(word));
                }
                else
                {
                    searchResult.IntersectWith(_index.Get(word));
                }
            }
        }

        private void FindAnyOfTheseWords(SearchQuery query, ISet<int> searchResult)
        {
            foreach (string word in query.AnyOfTheseWords)
            {
                searchResult.UnionWith(_index.Get(word));
            }
        }

        private void RemoveNoneOfTheseWords(SearchQuery query, ISet<int> searchResult)
        {
            foreach (string word in query.NoneOfTheseWords)
            {
                foreach (var docId in _index.Get(word))
                {
                    searchResult.Remove(docId);
                }
            }
        }
    }

}