using StudentProject.Abstracts;

namespace StudentProject.Concretes
{

    public class InvertedIndex
    {
        private IDictionary<string, ISet<int>> _map = new Dictionary<string, ISet<int>>();

        public virtual ISet<int> Get(string key)
        {
            key = key.ToUpper();

            if (_map.ContainsKey(key))
            {
                return _map[key];
            }
            return new HashSet<int>();
        }

        public void Add(string key, int value)
        {
            key = key.ToUpper();
            if (!_map.ContainsKey(key))
            {
                _map[key] = new HashSet<int>();
            }
            _map[key].Add(value);
        }

        public void AddAll(string[] keys, int value)
        {
            foreach (var key in keys)
            {
                Add(key, value);
            }
        }

        public void Print()
        {
            foreach (var entry in _map)
            {
                Console.WriteLine($"{entry.Key} : {string.Join(", ", entry.Value)}");
            }
        }
    }


    public class InvertedIndexBuilder
    {

        private readonly InvertedIndex _index = new();

        public InvertedIndexBuilder Add(IEnumerable<ISearchabale> searchables)
        {
            foreach (var searchable in searchables)
            {
                _index.AddAll(searchable.GetValue().Split(Settings.Delim), searchable.GetKey());
            }
            return this;
        }

        public InvertedIndex Build()
        {
            return _index;
        }

    }
}