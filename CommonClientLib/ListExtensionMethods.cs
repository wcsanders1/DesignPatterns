using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonClientLib
{
    public static class ListExtensionMethods
    {
        public static List<T> GetAllNestedTypes<T>(this List<T> objects) where T : class
        {
            if (objects == null || objects.Count() <= 0)
            {
                return null;
            }

            return GetAllNestedTypes(objects, new List<T>());
        }

        private static List<T> GetAllNestedTypes<T>(List<T> objectsToSearch, List<T> collectedObjects)
        {
            foreach (var obj in objectsToSearch)
            {

                
            }

            return collectedObjects;
        }
    }
}
