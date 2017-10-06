﻿using System;
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
            var typeNames = new List<string>();
            var types     = GetInstantiatedTypeList<T>();
            types.ForEach(type =>
            {
                var nameArray  = type.ToString().Split('.');
                var nameString = nameArray[nameArray.Length - 1];
                var name       = TxtParser.PascalToStringArray(nameString)[0];
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

        private List<T> GetInstantiatedTypeList<T>() where T : class
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
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

            var tpList = new List<T>();
            types.ForEach(tp =>
            {
                tpList.Add(Activator.CreateInstance(tp) as T);
            });

            return tpList;
        }
    }
}
