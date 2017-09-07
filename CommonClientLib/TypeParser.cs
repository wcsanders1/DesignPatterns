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

        public (Dictionary<int, T>, List<string>) GetTypeDictionaryAndNameList<T>() where T : class
        {
            var typeNames = new List<string>();
            var types     = GetTypeList<T>();
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

        private List<T> GetTypeList<T>() where T : class
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
