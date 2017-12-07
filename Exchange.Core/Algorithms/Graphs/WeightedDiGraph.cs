using System;
using System.Collections.Generic;
using System.Linq;

namespace Exchange.Core.Algorithms.Graphs
{
    public class WeightedDiGraph<T> where T: IComparable<T>
    {
        private readonly Dictionary<T, List<Edge<T>>> adjList = new Dictionary<T, List<Edge<T>>>();

        public void AddEdge(Edge<T> e)
        {
            var v = e.From();
            var vv = e.To();
            AddVertex(v);
            AddVertex(vv);
            adjList[v].Add(e);
        }

        public bool HasEdgeBetween(T from, T to)
        {
            return adjList.ContainsKey(from) && adjList[from].Any(edge => edge.To().CompareTo(to) == 0);
        }

        public void RemoveEdge(Edge<T> e)
        {
            var v = e.From();
            if (adjList.ContainsKey(v) && adjList[v].Contains(e)) adjList[v].Remove(e);
        }

        public void AddVertex(T v)
        {
            if (!adjList.ContainsKey(v)) adjList.Add(v, new List<Edge<T>>());
        }


        public List<Edge<T>> Adj(T v)
        {
            if (adjList.ContainsKey(v)) return adjList[v];
            else
            {
                adjList.Add(v, new List<Edge<T>>());
                return adjList[v];
            }
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
