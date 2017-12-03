using System.Collections.Generic;
using System.Linq;

namespace CommonClientLib.ExtensionMethods
{
    public static class IEnumerableExtensionMethods
    {
        public static IEnumerable<T> GetAllNestedTypes<T>(this IEnumerable<T> objects) where T : class
        {
            if (objects == null || objects.Count() <= 0)
            {
                return null;
            }

            return GetAllNestedTypes(objects, new List<T>());
        }

        private static IEnumerable<T> GetAllNestedTypes<T>(IEnumerable<T> objectsToSearch, List<T> collectedObjects)
        {
            if (objectsToSearch == null)
            {
                return null;
            }

            foreach (var obj in objectsToSearch)
            {
                collectedObjects.Add(obj);

                var objType = obj.GetType();
                foreach (var property in objType.GetProperties())
                {
                    if (property.PropertyType == typeof(List<T>))
                    {
                        var moreObjects = (List<T>)property.GetValue(obj);
                        GetAllNestedTypes(moreObjects, collectedObjects);
                    }
                }
            }

            return collectedObjects;
        }
    }
}
