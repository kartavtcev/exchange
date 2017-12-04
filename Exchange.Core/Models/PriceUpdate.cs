using System;

namespace Exchange.Core.Models
{
    public class PriceUpdate
    {
        public DateTime Timestamp { get; set; }
        public string Exchange { get; set; }
        public string SourceCurrency { get; set; }
        public string DestinationCurrency { get; set; }
        public double Factor { get; set; }
    }
}
