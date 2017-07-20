using System;

namespace SimpleFactory
{
    abstract public class AbstractSorter<T> : ISorter where T : IComparable
    {
        public virtual void Sort(T[] a)
        {
            Console.WriteLine("This sort method doesn't actually do anything except print this line out to the screen.\n");
        }

        protected bool Less(T v, T w)
        {
            return v.CompareTo(w) < 0;
        }

        protected void Exchange(T[] a, int i, int j)
        {
            T t = a[i];
            a[i] = a[j];
            a[j] = t;
        }
    }
}
