using Xunit;
using Exchange.Core.Algorithms.Queues;

namespace Exchange.Core.Tests.Algorithms.Queues
{
    public class IndexMinPQUnitTests
    {
        [Fact]
        public void Test()
        {
            var pq = new IndexMinPQ<int>(10);

            pq.Enqueue(1, 20);
            pq.Enqueue(2, 15);

            Assert.Equal(15, pq.MinKey());

            pq.Enqueue(1, 10);

            Assert.Equal(10, pq.MinKey());

            pq.Enqueue(3, 11);

            Assert.Equal(1, pq.DelMin());

            Assert.Equal(11, pq.MinKey());

            Assert.Equal(2, pq.Count);

            Assert.False(pq.IsEmpty);

            pq.Enqueue(2, 10);

            Assert.Equal(10, pq.MinKey());
            Assert.Equal(2, pq.DelMin());
        }
    }
}
