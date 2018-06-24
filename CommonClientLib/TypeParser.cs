using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonClientLib
{
    public class TypeParser
    {
        private TextParser TxtParser;

        public TypeParser(TextParser txtParser)
        {
            TxtParser = txtParser;
        }

        /// <summary>
        /// The order of the <c>List</c> is alphabetical and corresponds to keys of the <c>Dictionary</c>, which 
        /// makes it easy to display and print the list to the client, who can then choose a type from the dictionary
        /// by its key.
        /// </summary>
        /// <typeparam name="T">The type returned, if the type exists in the assembly.</typeparam>
        /// <returns><c>Dictionary</c> of <c>int</c> and <c>T</c>, and a <c>List</c> of the names of each <c>T</c></returns>
        public (Dictionary<int, T>, List<string>) GetInstantiatedTypeDictionaryAndNameList<T>() where T : class
        {
            var types = GetInstantiatedTypeList<T>();

            return GetDictAndList(types);
        }

        /// <summary>
        /// The order of the <c>List</c> is alphabetical and corresponds to keys of the <c>Dictionary</c>, which 
        /// makes it easy to display and print the list to the client, who can then choose a type from the dictionary
        /// by its key. Use this method rather than <see cref="GetInstantiatedTypeDictionaryAndNameList{T}"/> when
        /// the type requires arguments in its constructor.
        /// </summary>
        /// <typeparam name="T">The type returned, if the type exists in the assembly.</typeparam>
        /// <returns><c>Dictionary</c> of <c>int</c> and <c>T</c>, and a <c>List</c> of the names of each <c>T</c></returns>
        public (Dictionary<int, Type>, List<string>) GetTypeDictionaryAndNameList<T>() where T : class
        {
            var types = GetTypeList<T>();

            return GetDictAndList(types);
        }

        /// <summary>
        /// Gets the type associated with the key.
        /// </summary>
        /// <typeparam name="T">Type sought.</typeparam>
        /// <param name="strChoice">Key sought.</param>
        /// <param name="types">Dictionary of types.</param>
        /// <param name="type">Type returned.</param>
        /// <returns>False if key doesn't exist or key invalid.</returns>
        public bool TryGetType<T>(string strChoice, Dictionary<int, T> types, out T type) where T : class
        {
            if (!int.TryParse(strChoice, out var intChoice))
            {
                type = null;
                return false;
            }

            if (!types.TryGetValue(intChoice, out type))
            {
                type = null;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns a list of values of an enum as strings.
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <returns>List of values of enum as strings</returns>
        public List<string> GetEnumValuesList<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                return null;
            }

            return Enum.GetValues(typeof(T)).Cast<T>().Select(v => v.ToString()).ToList();
        }

        private List<T> GetInstantiatedTypeList<T>() where T : class
        {
            var types = GetTypeList<T>();
            var tpList = new List<T>();
            types.ForEach(tp =>
            {
                tpList.Add(Activator.CreateInstance(tp) as T);
            });

            return tpList;
        }

        private List<Type> GetTypeList<T>() where T : class
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type =>
                {
                    if (typeof(T).IsInterface)
                    {
                        return type.GetInterface(typeof(T).ToString()) != null;
                    }

                    return type.IsSubclassOf(typeof(T));
                })
                .ToList();
        }

        private (Dictionary<int, T>, List<string>) GetDictAndList<T>(List<T> types) where T : class
        {
            var typeNames = new List<string>();
            types.ForEach(type =>
            {
                var nameArray = type.ToString().Split('.');
                var nameString = nameArray[nameArray.Length - 1];
                var name = TxtParser.PascalToStringArray(nameString)[0];
                typeNames.Add(name);
            });

            typeNames.Sort();

            var key = 1;
            var typeDict = new Dictionary<int, T>();
            typeNames.ForEach(name =>
            {
                var type = types.Where(x => x.ToString().Contains(name))
                                .FirstOrDefault();

                typeDict.Add(key++, type);
            });

            return (typeDict, typeNames);
        }
    }
}
