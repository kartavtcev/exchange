using System;
using System.Collections.Generic;
using System.Text;
using Exchange.Core.Algorithms;
using Exchange.Core.Algorithms.Graphs;
using Xunit;

namespace Exchange.Core.Tests.Algorithms
{
    public class DijkstraUnitTest
    {
        //[Fact]
        //public void Test()
        //{
        //    var G = directedEdgeWeightedGraph();
        //    var dijkstra = new Dijkstra(G, 0);
        //    for (var v = 1; v < G.V(); ++v)
        //    {
        //        if (!dijkstra.HasPathTo(v))
        //        {
        //            Console.WriteLine("Path not found for {0}", v);
        //            continue;
        //        }
        //        //IEnumerable<Edge> path = dijkstra.PathTo(v);
        //        //Console.WriteLine(ToString(path));
        //    }
        //}

        //private String ToString(IEnumerable<Edge> path)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    bool first = true;
        //    foreach (var e in path)
        //    {
        //        if (first)
        //        {
        //            first = false;
        //            sb.Append(e.from());
        //        }
        //        sb.Append("=(").Append(e.Weight).Append(")=>").Append(e.to());
        //    }
        //    return sb.ToString();
        //}

        //private static WeightedDiGraph directedEdgeWeightedGraph()
        //{
        //    var g = new WeightedDiGraph(8);

        //    g.addEdge(new Edge(0, 1, 5.0));
        //    g.addEdge(new Edge(0, 4, 9.0));
        //    g.addEdge(new Edge(0, 7, 8.0));
        //    g.addEdge(new Edge(1, 2, 12.0));
        //    g.addEdge(new Edge(1, 3, 15.0));
        //    g.addEdge(new Edge(1, 7, 4.0));
        //    g.addEdge(new Edge(2, 3, 3.0));
        //    g.addEdge(new Edge(2, 6, 11.0));
        //    g.addEdge(new Edge(3, 6, 9.0));
        //    g.addEdge(new Edge(4, 5, 5.0));
        //    g.addEdge(new Edge(4, 6, 20.0));
        //    g.addEdge(new Edge(4, 7, 5.0));
        //    g.addEdge(new Edge(5, 2, 1.0));
        //    g.addEdge(new Edge(5, 6, 13.0));
        //    g.addEdge(new Edge(7, 5, 6.0));
        //    g.addEdge(new Edge(7, 2, 7.0));
        //    return g;
        //}
    }
}
