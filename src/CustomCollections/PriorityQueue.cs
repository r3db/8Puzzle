
namespace System.Collections.Generic
{
    public sealed class PriorityQueue<V>
    {
        private readonly Dictionary<int, Stack<V>> list = new Dictionary<int, Stack<V>>();
        private readonly SortedList<int, int> keys = new SortedList<int, int>();

        public void Enqueue(int priority, V value)
        {
            if (list.ContainsKey(priority) == false)
            {
                keys.Add(priority, 0);
                list.Add(priority, new Stack<V>());
            }

            list[priority].Push(value);
            Count += 1;
        }

        public V Dequeue()
        {
            int k = keys.Keys[0];

            Stack<V> pair = list[k];
            V v = pair.Pop();
            if (pair.Count == 0)
            {
                list.Remove(k);
                keys.Remove(k);
            }
            Count -= 1;
            return v;
        }

        public V Peek()
        {
            return list[keys.Keys[0]].Peek();
        }

        public bool IsEmpty
        {
            get { return Count == 0; }
        }

        public int Count { get; private set; }
    }
}
