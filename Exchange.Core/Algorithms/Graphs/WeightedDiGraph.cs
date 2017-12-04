using System;
using System.Collections.Generic;

namespace Exchange.Core.Algorithms.Graphs
{
    public class WeightedDiGraph<T> where T: IComparable<T>
    {
        private readonly Dictionary<T, List<Edge<T>>> adjList = new Dictionary<T, List<Edge<T>>>();

        public void AddEdge(Edge<T> e)
        {
            var v = e.From();
            if (!adjList.ContainsKey(v)) adjList.Add(v, new List<Edge<T>>());
            adjList[v].Add(e);
        }

        public void AddVertex(T v)
        {
            if (!adjList.ContainsKey(v)) adjList.Add(v, new List<Edge<T>>());
        }

        public List<Edge<T>> Adj(T v)
        {
            return adjList[v];
        }

        public int V()
        {
            return adjList.Keys.Count;
        }

        public IList<Edge<T>> Edges()
        {
            var result = new List<Edge<T>>();
            foreach (var edges in adjList.Values)
            {
                result.AddRange(edges);
            }
            return result;
        }


        public IEnumerable<T> Vertexes()
        {
            return adjList.Keys;
        }
    }
}
