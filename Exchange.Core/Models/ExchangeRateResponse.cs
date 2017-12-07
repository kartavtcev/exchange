using System;
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

        private double _rate;
        public double Rate
        {
            get { return _rate; }
            set { _rate = Math.Round(value, 5, MidpointRounding.AwayFromZero); }
        }

        public IList<ExchangeCurrency> Path { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(BestRatesBegin);
            sb.Append($" {Source.Exchange} {Source.Currency} {Destination.Exchange} {Destination.Currency} ");
            sb.AppendFormat($"{Rate} \n", Environment.NewLine);
            //sb.AppendFormat($"{Source.Exchange}, {Source.Currency} \n", Environment.NewLine);
            foreach (var v in Path) sb.AppendFormat($"{v.Exchange}, {v.Currency} \n", Environment.NewLine);
            //sb.AppendFormat($"{Destination.Exchange}, {Destination.Currency} \n", Environment.NewLine);
            sb.Append(BestRatesEnd);
            return sb.ToString();
        }
    }
}
