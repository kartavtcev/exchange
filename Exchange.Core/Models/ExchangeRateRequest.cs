namespace Exchange.Core.Models
{
    public class ExchangeRateRequest
    {
        public ExchangeCurrency Source { get; set; }
        public ExchangeCurrency Destination { get; set; }
    }
}
