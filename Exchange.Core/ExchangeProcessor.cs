using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exchange.Core.Algorithms.Graphs;
using Exchange.Core.Models;

namespace Exchange.Core
{
    public class ExchangeProcessor
    {
        private readonly WeightedDiGraph<ExchangeCurrency> graph = new WeightedDiGraph<ExchangeCurrency>();
        private static readonly Object lockTheEntireGraph = new Object();
        //private IList<string> exchanges = new List<string>();
        //private IList<string> currencies = new List<string>();


        public void PriceUpdate(PriceUpdate update)
        {
            lock (lockTheEntireGraph)
            {
                var from = new ExchangeCurrency(update.Exchange, update.SourceCurrency);
                var to = new ExchangeCurrency(update.Exchange, update.DestinationCurrency);
                var weight = -Math.Log(update.Factor);

                if (!graph.Vertexes().Contains(from)) graph.AddVertex(from);
                if (!graph.Vertexes().Contains(to)) graph.AddVertex(to);
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
            
            throw new NotImplementedException();
        }
        
    }
}
