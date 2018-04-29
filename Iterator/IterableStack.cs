using System.Collections;
using System.Collections.Generic;

namespace Iterator
{
    public class IterableStack<T> : IEnumerable<T>
    {
        private Node First;
        private int Num;

        private class Node
        {
            public T Item;
            public Node Next;
        }

        public bool IsEmpty()
        {
            return First == null;
        }

        public int Size()
        {
            return Num;
        }

        public void Push(T item)
        {
            Node oldFirst = First;
            First = new Node()
            {
                Item = item,
                Next = oldFirst,
            };
            Num++;
        }

        public T Pop()
        {
            T item = First.Item;
            First = First.Next;
            Num--;
            return item;
        }

        public T Peek()
        {
            return First.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = First;
            while (node != null)
            {
                yield return node.Item;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
