using System;

namespace SimpleFactory
{
    public class TopDownMergeSorter<T> : AbstractSorter<T> where T : IComparable
    {
        public override string Name { get; } = "Top Down Merge Sorter";

        private T[] Aux { get; set; }

        public override void Sort(T[] a)
        {
            Aux = new T[a.Length];
            Sort(a, 0, a.Length - 1);
        }

        private void Sort(T[] a, int lo, int hi)
        {
            if (hi <= lo)
                return;

            int mid = lo + ((hi - lo) / 2);
            Sort(a, lo, mid);
            Sort(a, mid + 1, hi);
            Merge(a, lo, mid, hi);
        }

        // This method merges two sections of an array that themselves are already sorted
        private void Merge(T[] a, int lo, int mid, int hi)
        {
            int i = lo,
                j = mid + 1;

            // copy a to Aux
            for (int k = lo; k <= hi; k++)
            {
                Aux[k] = a[k];
            }

            for (int k = lo; k <= hi; k++)
            {
                // left half exhausted
                if (i > mid)
                    a[k] = Aux[j++];

                // right half exhauseted
                else if (j > hi)
                    a[k] = Aux[i++];

                // current key on right less than current key on left
                else if (Less(Aux[j], Aux[i]))
                    a[k] = Aux[j++];

                // current key on right greater than or equal to current key on left
                else
                    a[k] = Aux[i++];
            }
        }
    }
}
