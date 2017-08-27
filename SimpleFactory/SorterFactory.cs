using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleFactory
{
    public class SorterFactory<T> where T : IComparable
    {
        private Dictionary<string, Type> _sorters;

        public SorterFactory()
        {
            LoadSorters();
        }

        public AbstractSorter<T> CreateInstance(Sorters sorter)
        {
            var generic = typeof(T);
            var chosenSorter = GetSorterToCreate(sorter).MakeGenericType(generic);

            return (AbstractSorter<T>)Activator.CreateInstance(chosenSorter);
        }

        private Type GetSorterToCreate(Sorters sorter)
        {
            foreach (var entry in _sorters)
            {
                if (entry.Key.Contains(sorter.ToString().ToLower()))
                {
                    return _sorters[entry.Key];
                }
            }

            return null;
        }

        private void LoadSorters()
        {
            _sorters = new Dictionary<string, Type>();

            var typesInThisAssembly = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var type in typesInThisAssembly)
            {
                if (type.GetInterface(typeof(ISorter).ToString()) != null)
                {
                    _sorters.Add(type.Name.ToLower(), type);
                }
            }
        }
    }
}
