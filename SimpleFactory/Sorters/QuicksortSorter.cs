using System;

namespace SimpleFactory
{
    public class QuicksortSorter<T> : AbstractSorter<T> where T : IComparable
    {
        public override string Name { get; } = "Quick Sorter";

        public override void Sort(T[] a)
        {
            Sort(a, 0, a.Length - 1);
        }

        private void Sort(T[] a, int lo, int hi)
        {
            if (hi <= lo)
            {
                return;
            }

            int j = Partition(a, lo, hi);
            Sort(a, lo, j - 1);
            Sort(a, j + 1, hi);
        }

        private int Partition(T[] a, int lo, int hi)
        {
            int i = lo, j = hi + 1;
            T v = a[lo];

            while (true)
            {
                while (Less(a[++i], v))
                {
                    if (i == hi)
                    {
                        break;
                    }
                }

                while (Less(v, a[--j]))
                {
                    if (j == lo)
                    {
                        break;
                    }
                }

                if (i >= j)
                {
                    break;
                }

                Exchange(a, i, j);
            }

            Exchange(a, lo, j);
            return j;
        }
    }
}
