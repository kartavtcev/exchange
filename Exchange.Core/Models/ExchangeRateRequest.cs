using System;

namespace Exchange.Core.Models
{
    public class ExchangeRateRequest
    {
        public const string EXCHANGE_RATE_REQUEST = "EXCHANGE_RATE_REQUEST";
        private static ExchangeRateRequest empty = new ExchangeRateRequest();

        public ExchangeCurrency Source { get; set; }
        public ExchangeCurrency Destination { get; set; }

        public ExchangeRateRequest Parse(string line)
        {
            if (line == null) return ExchangeRateRequest.empty;
            if (!IsExchangeRateRequest(line)) return ExchangeRateRequest.empty;
            var splitted = line.Split(' ');
            if (splitted.Length != 5) return ExchangeRateRequest.empty;
            else
            {
                try
                {
                    var constant = splitted[0];
                    if (constant != EXCHANGE_RATE_REQUEST) throw new ArgumentOutOfRangeException();
                    var exchangerr = new ExchangeRateRequest
                    {
                        Source = new ExchangeCurrency
                        {
                            Exchange = splitted[1],
                            Currency = splitted[2]
                        },
                        Destination = new ExchangeCurrency
                        {
                            Exchange = splitted[3],
                            Currency = splitted[4]
                        }
                    };
                    return exchangerr;
                }
                catch (Exception ex)
                {
                    // TODO: log exception
                    return ExchangeRateRequest.empty;
                }
            }
        }

        public bool IsValid(string line)
        {
            var request = Parse(line);
            return request.Source != null
                && request.Destination != null
                && !string.IsNullOrWhiteSpace(request.Source.Exchange)
                && !string.IsNullOrWhiteSpace(request.Source.Currency)
                && !string.IsNullOrWhiteSpace(request.Destination.Exchange)
                && !string.IsNullOrWhiteSpace(request.Destination.Currency);
        }

        public bool IsExchangeRateRequest(string line)
        {
            return !string.IsNullOrWhiteSpace(line) 
                && line.Length > EXCHANGE_RATE_REQUEST.Length
                && line.Substring(0, EXCHANGE_RATE_REQUEST.Length).Equals(EXCHANGE_RATE_REQUEST);
        }
    }
}
