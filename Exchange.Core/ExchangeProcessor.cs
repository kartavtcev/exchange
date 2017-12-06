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
                //if (!exchanges.Contains(update.Exchange)) exchanges.Add(update.Exchange);
                //if (!currencies.ContainsUP)

                var from = new ExchangeCurrency(update.Exchange, update.SourceCurrency);
                var to = new ExchangeCurrency(update.Exchange, update.DestinationCurrency);
                var weight = -Math.Log(update.Factor);

                // TODO:
                // add edge or update based on timestamp
                if (!graph.Vertexes().Contains(from)) graph.AddVertex(from);
                if (!graph.Vertexes().Contains(to)) graph.AddVertex(to);
                var oldEdge = graph.Adj(from).Where(e => e.To().Equals(to)).SingleOrDefault();
                var newEdge = new Edge<ExchangeCurrency>(from, to, weight, update.Timestamp);
                if (oldEdge != null && oldEdge.TimeStamp < update.Timestamp)
                {
                    graph.RemoveEdge(oldEdge);
                    graph.AddEdge(newEdge);
                }
                else if (oldEdge == null)
                {
                    graph.AddEdge(newEdge);
                }

                //graph.Edges().Where(e => e.From().Currency == update.DestinationCurrency);

                // ONESSS 111111111
                // add -Math.Log(1)

                // add same currency 1 edges if not exist
                

                //graph.Edges
                //var edge = Edge<>
            }
        }


        public ExchangeRateResponse ExchangeRate(ExchangeRateRequest request)
        {
            throw new NotImplementedException();
        }
        
    }
}
