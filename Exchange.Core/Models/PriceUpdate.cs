using System;
using System.Globalization;

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

        public static bool TryParse(string line, out PriceUpdate priceUpdate)
        {
            var update = Parse(line);
            priceUpdate = update;

            return update.Timestamp != default(DateTime)
                && !string.IsNullOrWhiteSpace(update.Exchange)
                && !string.IsNullOrWhiteSpace(update.SourceCurrency)
                && !string.IsNullOrWhiteSpace(update.DestinationCurrency)
                && update.Factor != default(double);
        }

        public static bool IsPriceUpdate(string line)
        {
            DateTime dateTime;
            return !string.IsNullOrWhiteSpace(line)
                && DateTime.TryParseExact(line.Split(' ')[0], "o", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
        }

        private static PriceUpdate Parse(string line)
        {
            if (line == null) return PriceUpdate.empty;
            var splitted = line.Split(' ');
            if (splitted.Length != 5) return PriceUpdate.empty;
            else
            {
                try
                {
                    DateTime dateTime;
                    DateTime.TryParseExact(splitted[0], "o", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);

                    var pu = new PriceUpdate
                    {
                        Timestamp = dateTime,
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
    }
}
