using System;

namespace SimpleFactory
{
    public class InsertionSorter<T> : AbstractSorter<T> where T : IComparable
    {
        public override void Sort(T[] a)
        {
            int n = a.Length;
            for (int i = 1; i < n; i++)
            {
                for (int j = i; j > 0 && Less(a[j], a[j - 1]); j--)
                {
                    Exchange(a, j, j - 1);
                }
            }
        }
    }
}
