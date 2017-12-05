using System.Collections.Generic;

namespace Exchange.Core.Algorithms.LinkedLists
{
    public interface IStack<T> : IEnumerable<T>
    {
        T Pop();
        void Push(T item);
        int Count { get; }
        bool IsEmpty { get; }
    }
}
