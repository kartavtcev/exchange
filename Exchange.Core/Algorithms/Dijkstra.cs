using System;
using System.Collections.Generic;
using Exchange.Core.Algorithms.Graphs;
using Exchange.Core.Algorithms.Queues;

namespace Exchange.Core.Algorithms
{
    public class Dijkstra<T> where T : IComparable<T>
    {
        private T s;
        private bool[] marked;
        private Edge<T>[] edgeTo;
        private double[] cost;
        private IndexMinPQ<Double> pq;
        private List<T> vertexes;

        public Dijkstra(WeightedDiGraph<T> G, T s)
        {
            this.s = s;
            int V = G.V();
            marked = new bool[V];
            edgeTo = new Edge<T>[V];
            cost = new double[V];

            for (var i = 0; i < V; ++i)
            {
                cost[i] = Double.MaxValue;
            }

            cost[this.vertexes.IndexOf(s)] = 0;

            this.vertexes = new List<T>(G.Vertexes());
            pq = new IndexMinPQ<Double>(V);


            pq.Insert(this.vertexes.IndexOf(s), 0);

            while (!pq.IsEmpty)
            {
                var v = pq.DelMin();
                marked[v] = true;
                foreach (var e in G.Adj(this.vertexes[v]))
                {
                    Relax(G, e);
                }
            }
        }

        private void Relax(WeightedDiGraph<T> G, Edge<T> e)
        {
            int v = this.vertexes.IndexOf(e.From());
            int w = this.vertexes.IndexOf(e.To());
            if (cost[w] > cost[v] + e.Weight)
            {
                cost[w] = cost[v] + e.Weight;
                edgeTo[w] = e;
                if (!pq.Contains(w))
                {
                    pq.Insert(w, cost[w]);
                }
                else
                {
                    pq.DecreaseKey(w, cost[w]);
                }
            }
        }

        public bool HasPathTo(T v)
        {
            return marked[this.vertexes.IndexOf(v)];
        }

        public IEnumerable<Edge<T>> PathTo(T v)
        {
            var path = new List<Edge<T>>();
            for (var x = v; x.CompareTo(s) != 0; x = edgeTo[this.vertexes.IndexOf(x)].From())
            {
                path.Add(edgeTo[this.vertexes.IndexOf(x)]);
            }
            path.Reverse();
            return path;
        }
    }
}
