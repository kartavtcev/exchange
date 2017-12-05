using System;
using System.Collections.Generic;
using Exchange.Core.Algorithms.LinkedLists;
using Exchange.Core.Algorithms.Graphs;

namespace Exchange.Core.Algorithms
{
    public class BellmanFord<T> where T : IComparable<T>
    {
        private readonly Dictionary<T, double> distTo = new Dictionary<T, double>();
        private readonly Dictionary<T, Edge<T>> edgeTo = new Dictionary<T, Edge<T>>();
        private readonly Dictionary<T, bool> onQueue = new Dictionary<T, bool>();
        private IList<T> vertexes;
        private IQueue<T> queue = new QueueLinkedList<T>();
        private int cost; // number of calls to relax()
        private IEnumerable<Edge<T>> cycle;        

        public BellmanFord(WeightedDiGraph<T> G, T s)
        {
            vertexes = new List<T>(G.Vertexes());
            foreach (var v in G.Vertexes())
            {
                distTo[v] = double.MaxValue; // .MinValue
            }
            distTo[s] = 0.0;

            queue.Enqueue(s);
            onQueue[s] = true;
            while (!queue.IsEmpty && !HasNegativeCycle())
            {
                T v = queue.Dequeue();
                onQueue[v] = false;
                Relax(G, v);
            }
        }

        private void Relax(WeightedDiGraph<T> G, T v)
        {
            foreach (var e in G.Adj(v))
            {
                T w = e.To();
                if (distTo[w] > distTo[v] + e.Weight)
                {
                    distTo[w] = distTo[v] + e.Weight;
                    edgeTo[w] = e;
                    if (!onQueue[w])
                    {
                        queue.Enqueue(w);
                        onQueue[w] = true;
                    }
                }
                if (cost++ % G.V() == 0)
                {
                    FindNegativeCycle();
                    if (HasNegativeCycle()) return; // found a negative cycle
                }
            }
        }

        private bool HasNegativeCycle() { return cycle != null; }

        public IEnumerable<Edge<T>> NegativeCycle()
        {
            return cycle;
        }

        private void FindNegativeCycle()
        {
            var spt = new WeightedDiGraph<T>();
            foreach (var e in edgeTo.Values)
            {
                spt.AddEdge(e);
            }
            var finder = new EdgeWeightedDirectedCycle<T>(spt);
            cycle = finder.Cycle();
        }

        public double DistTo(T v)
        {
            if (!vertexes.Contains(v)) throw new IndexOutOfRangeException("Graph does not contain parameter vertex");
            if (HasNegativeCycle()) throw new Exception("Negative cost cycle exists");
            return distTo[v];
        }

        public bool HasPathTo(T v)
        {
            if (!vertexes.Contains(v)) throw new IndexOutOfRangeException("Graph does not contain parameter vertex");
            return distTo[v] < Double.PositiveInfinity;
        }

        public IEnumerable<Edge<T>> PathTo(T v)
        {
            if (!vertexes.Contains(v)) throw new IndexOutOfRangeException("Graph does not contain parameter vertex");
            if (HasNegativeCycle()) throw new Exception("Negative cost cycle exists");
            if (!HasPathTo(v)) return null;
            var path = new StackLinkedList<Edge<T>>();
            for (var e = edgeTo[v]; e != null; e = edgeTo[e.From()])
            {
                path.Push(e);
            }
            return path;
        }
    }
}
