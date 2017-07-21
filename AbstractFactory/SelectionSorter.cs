using System;

namespace SimpleFactory
{
    public class SelectionSorter<T> : AbstractSorter<T> where T : IComparable
    {
        public override string Name { get; } = "Selection Sorter";

        public override void Sort(T[] a)
        {
            int n = a.Length;
            for (int i = 0; i < n; i++)
            {
                int min = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (Less(a[j], a[min]))
                        min = j;

                    Exchange(a, i, min);
                }
            }
        }
    }
}
