using System;
using System.Collections.Generic;
using System.Text;
using Exchange.Core.Algorithms.Graphs;
using Exchange.Core.Algorithms.Stack;

namespace Exchange.Core.Algorithms
{
    public class BellmanFulkerson<T> where T : IComparable<T>
    {
        private readonly Dictionary<T, double> cost = new Dictionary<T, double>();
        private readonly Dictionary<T, Edge<T>> edgeTo = new Dictionary<T, Edge<T>>();
        private T s;
        private bool negativeCycles;

        public BellmanFulkerson(WeightedDiGraph<T> G, T s)
        {
            this.s = s;
            foreach (var v in G.Vertexes())
            {
                cost.Add(v, Double.MaxValue); // Double.MinValue
            }

            cost[s] = 0;

            for (var j = 0; j < G.V(); ++j)
            {
                foreach (var v in G.Vertexes())
                {
                    foreach (var e in G.Adj(v))
                    {
                        Relax(G, e);
                    }
                }
            }

            negativeCycles = false;
            foreach (var v in G.Vertexes())
            {
                foreach (var e in G.Adj(v))
                {
                    if (Relax(G, e))
                    {
                        negativeCycles = true;
                    }
                }
            }
        }

        private bool Relax(WeightedDiGraph<T> G, Edge<T> e)
        {
            var v = e.From();
            var w = e.To();
            if (cost[w] > cost[v] + e.Weight)
            {
                cost[w] = cost[v] + e.Weight;
                edgeTo[w] = e;

                return true;
            }
            return false;
        }

        public bool HasPathTo(T v)
        {
            return cost[v] < Double.MaxValue;
        }

        public bool HashNegativeCycles()
        {
            return negativeCycles;
        }

        public IEnumerable<Edge<T>> PathTo(T v)
        {
            var path = new StackLinkedList<Edge<T>>();
            for (var x = v; x.CompareTo(this.s) != 0; x = edgeTo[x].Other(x))
            {
                path.Push(edgeTo[x]);
            }

            return path;
        }
    }
}
