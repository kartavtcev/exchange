using System.Collections.Generic;

namespace Exchange.Core.Algorithms.LinkedLists
{
    public interface IQueue<T> : IEnumerable<T>
    {
        T Dequeue();
        void Enqueue(T item);
        int Count { get; }
        bool IsEmpty { get; }
    }
}
