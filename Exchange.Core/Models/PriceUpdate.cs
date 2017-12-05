using System;

namespace Exchange.Core.Models
{
    public class PriceUpdate
    {
        private static PriceUpdate empty = new PriceUpdate();
        public DateTime Timestamp { get; set; }
        public string Exchange { get; set; }
        public string SourceCurrency { get; set; }
        public string DestinationCurrency { get; set; }
        public double Factor { get; set; }

        public PriceUpdate Parse(string line)
        {
            if (line == null) return PriceUpdate.empty;
            var splitted = line.Split(' ');
            if (splitted.Length != 5) return PriceUpdate.empty;
            else
            {
                try
                {
                    var pu = new PriceUpdate
                    {
                        Timestamp = DateTime.Parse(splitted[0]),
                        Exchange = splitted[1],
                        SourceCurrency = splitted[2],
                        DestinationCurrency = splitted[3],
                        Factor = Double.Parse(splitted[4])
                    };
                    return pu;
                }
                catch (Exception ex)
                {
                    // TODO: log exception
                    return PriceUpdate.empty;
                }
            }
        }

        public bool IsValid(string line)
        {
            var update = Parse(line);
            return update.Timestamp != default(DateTime)
                && !string.IsNullOrWhiteSpace(update.Exchange)
                && !string.IsNullOrWhiteSpace(update.SourceCurrency)
                && !string.IsNullOrWhiteSpace(update.DestinationCurrency)
                && update.Factor != default(double);
        }
    }
}
