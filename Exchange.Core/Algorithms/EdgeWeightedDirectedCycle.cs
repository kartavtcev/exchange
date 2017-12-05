﻿using System;
using System.Collections.Generic;
using Exchange.Core.Algorithms.Graphs;
using Exchange.Core.Algorithms.LinkedLists;

namespace Exchange.Core.Algorithms
{
    public class EdgeWeightedDirectedCycle<T> where T : IComparable<T>
    {
        private readonly Dictionary<T, bool> marked = new Dictionary<T, bool>();
        private readonly Dictionary<T, Edge<T>> edgeTo = new Dictionary<T, Edge<T>>();
        private readonly Dictionary<T, bool> onStack = new Dictionary<T, bool>();
        private IStack<Edge<T>> cycle;

        public EdgeWeightedDirectedCycle(WeightedDiGraph<T> G)
        {
            foreach (var v in G.Vertexes())
            {
                if (!marked[v]) Dfs(G, v);
            }
        }

        private void Dfs(WeightedDiGraph<T> G, T v)
        {
            onStack[v] = true;
            marked[v] = true;
            foreach (var e in G.Adj(v))
            {
                T w = e.To();
                if (cycle != null) return; // if directed cycle found
                else if (!marked[w])
                {
                    edgeTo[w] = e;
                    Dfs(G, w);
                }
                else if (onStack[w]) 
                {
                    cycle = new StackLinkedList<Edge<T>>();

                    Edge<T> f = e;
                    while (f.From().CompareTo(w) != 0)
                    {
                        cycle.Push(f);
                        f = edgeTo[f.From()];
                    }

                    cycle.Push(f);
                    return;
                }
            }
            onStack[v] = false;
        }

        public bool HasCycle()
        {
            return cycle != null;
        }

        public IEnumerable<Edge<T>> Cycle()
        {
            return cycle;
        }
    }
}
