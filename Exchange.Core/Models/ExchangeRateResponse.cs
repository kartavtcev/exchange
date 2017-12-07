﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Exchange.Core.Models
{
    public class ExchangeRateResponse
    {
        const string BestRatesBegin = "BEST_RATES_BEGIN";
        const string BestRatesEnd = "BEST_RATES_END";
        const string Ws = " ";

        public ExchangeRateResponse(ExchangeCurrency source, ExchangeCurrency destination, double rate, IEnumerable<ExchangeCurrency> path)
        {
            this.Source = source;
            this.Destination = destination;
            this.Rate = rate;
            this.Path = new List<ExchangeCurrency>(path);
        }

        public ExchangeCurrency Source { get; set; }
        public ExchangeCurrency Destination { get; set; }

        public double Rate { get; set; }

        public IList<ExchangeCurrency> Path { get; set; }

        public override string ToString()
        {
            var start = $" {Source.Exchange} {Source.Currency} {Destination.Exchange} {Destination.Currency} ";
            var sb = new StringBuilder();
            sb.Append(BestRatesBegin);
            sb.Append(start);
            sb.Append(Rate);
            sb.Append(Environment.NewLine);
            sb.AppendFormat($"{Source.Exchange}, {Source.Currency}", Environment.NewLine);
            foreach (var v in Path) sb.AppendFormat($"{v.Exchange}, {v.Currency}", Environment.NewLine);
            sb.AppendFormat($"{Destination.Exchange}, {Destination.Currency}", Environment.NewLine);
            sb.Append(BestRatesEnd);
            return sb.ToString();
        }
    }
}