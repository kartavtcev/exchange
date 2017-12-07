using Xunit;
using Exchange.Core.Algorithms.LinkedLists;

namespace Exchange.Core.Tests.Algorithms.LinkedLists
{
    public class QueueLinkedListUnitTest
    {
        [Fact]
        public void TestQueue()
        {
            var queue = new QueueLinkedList<int>();
            queue.Enqueue(10);
            Assert.Equal(1, queue.Count);
            queue.Enqueue(20);
            Assert.Equal(2, queue.Count);
            Assert.False(queue.IsEmpty);
            Assert.Equal(10, queue.Dequeue());
            Assert.Equal(20, queue.Dequeue());
            Assert.True(queue.IsEmpty);
        }
    }
}