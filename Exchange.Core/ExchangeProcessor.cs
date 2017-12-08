using System;
using System.Collections.Generic;
using System.Linq;
using Exchange.Core.Algorithms.Graphs;
using Exchange.Core.Models;
using Exchange.Core.Algorithms;

namespace Exchange.Core
{
    public class ExchangeProcessor
    {
        private readonly WeightedDiGraph<ExchangeCurrency> graph = new WeightedDiGraph<ExchangeCurrency>();
        private static readonly Object lockTheEntireGraph = new Object();
        
        public void PriceUpdate(PriceUpdate update)
        {
            lock (lockTheEntireGraph)
            {
                var from = new ExchangeCurrency(update.Exchange, update.SourceCurrency);
                var to = new ExchangeCurrency(update.Exchange, update.DestinationCurrency);
                double weight = -Math.Log(update.Factor);

                var oldEdge = graph.Adj(from).Where(e => e.To().CompareTo(to) == 0).SingleOrDefault();
                var newEdge = new Edge<ExchangeCurrency>(from, to, weight, update.Timestamp);
                if (oldEdge != null && oldEdge.TimeStamp < update.Timestamp)
                {
                    graph.RemoveEdge(oldEdge);
                    graph.AddEdge(newEdge);
                }
                else if (oldEdge == null) // add 1<-->1 edges by currency
                {
                    graph.AddEdge(newEdge);
                    foreach (var v in graph.Vertexes()
                        .Where(v => v.Currency == update.SourceCurrency)
                        .Where(v => v.CompareTo(from) != 0))
                    {
                        if (!graph.HasEdgeBetween(v, from)) graph.AddEdge(new Edge<ExchangeCurrency>(v, from, -Math.Log(1)));
                        if (!graph.HasEdgeBetween(from, v)) graph.AddEdge(new Edge<ExchangeCurrency>(from, v, -Math.Log(1)));
                    }

                    foreach (var v in graph.Vertexes()
                        .Where(v => v.Currency == update.DestinationCurrency)
                        .Where(v => v.CompareTo(to) != 0))
                    {
                        if (!graph.HasEdgeBetween(v, to)) graph.AddEdge(new Edge<ExchangeCurrency>(v, to, -Math.Log(1)));
                        if (!graph.HasEdgeBetween(to, v)) graph.AddEdge(new Edge<ExchangeCurrency>(to, v, -Math.Log(1)));
                    }
                }
            }
        }

        public ExchangeRateResponse ExchangeRate(ExchangeRateRequest request)
        {
            lock (lockTheEntireGraph)
            {
                var response = new ExchangeRateResponse();

                var spt = new BellmanFord<ExchangeCurrency>(graph, request.Source);
                if (spt.HasNegativeCycle())
                {
                    response.IsCycle = true;

                    var cycle = spt.NegativeCycle();
                    response.Cycle = EdgesToVertexes(cycle);
                    return response; // TODO: negative cycle ==> arbitrage opportunity
                }
                if (spt.HasPathTo(request.Destination))
                {
                    response.IsHasPath = true;

                    var path = spt.PathTo(request.Destination);
                    var vertexesOnPath = EdgesToVertexes(path);
                    double rate = 1;
                    foreach (var edge in path) rate *= Math.Exp(-edge.Weight);

                    response.Source = request.Source;
                    response.Destination = request.Destination;
                    response.Rate = rate;
                    response.Path = vertexesOnPath;
                    return response;
                }
                if (!spt.HasPathTo(request.Destination))
                {
                    response.IsHasPath = false;
                    return response;
                }

                throw new NotImplementedException();
            }
        }

        private IList<ExchangeCurrency> EdgesToVertexes(IEnumerable<Edge<ExchangeCurrency>> edges)
        {
            var vertexes = new List<ExchangeCurrency>();
            if (edges != null && edges.Count() >= 1) vertexes.Add(edges.First().From());
            foreach (var edge in edges) vertexes.Add(edge.To());
            return vertexes;
        }
    }
}
