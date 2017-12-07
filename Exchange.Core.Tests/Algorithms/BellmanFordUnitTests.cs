using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Exchange.Core.Algorithms;
using Exchange.Core.Algorithms.Graphs;

namespace Exchange.Core.Tests.Algorithms
{
    public class BellmanFordUnitTests
    {
        [Fact]
        public void Test()
        {
            WeightedDiGraph<TestKey> graph = DirectedEdgeWeightedGraph();
            var bellmanFord = new BellmanFord<TestKey>(graph, key0);
            foreach (var v in graph.Vertexes())
            {
                if (!bellmanFord.HasPathTo(v)) throw new NotImplementedException("all vertexes in the graph must have a path");
                IEnumerable<Edge<TestKey>> path = bellmanFord.PathTo(v);
                if (v == key3)
                {
                    var pathList = new List<Edge<TestKey>>(path);
                    Assert.Equal(3, pathList.Count);
                    Assert.Equal(18, pathList.Sum(e => e.Weight));
                }
                if (v == key6)
                {
                    var pathList = new List<Edge<TestKey>>(path);
                    Assert.Equal(3, pathList.Count);
                    Assert.Equal(26, pathList.Sum(e => e.Weight));
                }
            }
        }

        private WeightedDiGraph<TestKey> DirectedEdgeWeightedGraph()
        {
            var g = new WeightedDiGraph<TestKey>();

            var key1 = new TestKey("1");
            var key2 = new TestKey("2");
            var key4 = new TestKey("4");
            var key5 = new TestKey("5");
            var key7 = new TestKey("7");

            g.AddEdge(new Edge<TestKey>(key0, key1, 5.0));
            g.AddEdge(new Edge<TestKey>(key0, key4, 9.0));
            g.AddEdge(new Edge<TestKey>(key0, key7, 8.0));
            g.AddEdge(new Edge<TestKey>(key1, key2, 12.0));
            g.AddEdge(new Edge<TestKey>(key1, key3, 15.0));
            g.AddEdge(new Edge<TestKey>(key1, key7, 4.0));
            g.AddEdge(new Edge<TestKey>(key2, key3, 3.0));
            g.AddEdge(new Edge<TestKey>(key2, key6, 11.0));
            g.AddEdge(new Edge<TestKey>(key3, key6, 9.0));
            g.AddEdge(new Edge<TestKey>(key4, key5, 5.0));
            g.AddEdge(new Edge<TestKey>(key4, key6, 20.0));
            g.AddEdge(new Edge<TestKey>(key4, key7, 5.0));
            g.AddEdge(new Edge<TestKey>(key5, key2, 1.0));
            g.AddEdge(new Edge<TestKey>(key5, key6, 13.0));
            g.AddEdge(new Edge<TestKey>(key7, key5, 6.0));
            g.AddEdge(new Edge<TestKey>(key7, key2, 7.0));
            return g;
        }

        private class TestKey : IComparable<TestKey>
        {
            public TestKey(string key)
            {
                Key = key;
            }
            public string Key { get; set; }

            public int CompareTo(TestKey other)
            {
                return (this.Key).CompareTo(other.Key);
            }

            public override string ToString()
            {
                return Key;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (Key != null ? Key.GetHashCode() : 0);
                }
            }

            public override bool Equals(object obj)
            {
                if (!(obj is TestKey))
                    return false;
                var excc = (TestKey)obj;
                return this.Key == excc.Key;
            }
        }
        private TestKey key0 = new TestKey("0");
        private TestKey key3 = new TestKey("3");
        private TestKey key6 = new TestKey("6");
    }
}
