using System.Collections.Generic;
using Xunit;
using Exchange.Core.Models;


namespace Exchange.Core.Tests
{
    public class ExchangeProcessorUnitTests
    {
        [Fact]
        public void BestPathIsFoundByProcessorWhenExists()
        {
            // Arrange
            var processor = new ExchangeProcessor();

            var update1  = "2017-11-01T09:42:23+02:00 KRAKEN BTC USD 10000.0";
            var update11 = "2017-11-01T09:42:23+00:00 KRAKEN BTC USD 10002.0";
            var update111 = "2017-11-01T09:42:23-01:00 KRAKEN BTC USD 1000.0";
            var update1111 = "2017-11-01T09:42:00+02:00 KRAKEN BTC USD 10001.0";

            var update2 = "2017-11-01T09:43:11+00:00 KRAKEN USD BTC 0.0005";
            var update3 = "2017-11-01T09:43:23+00:00 GDAX BTC USD 990.0";
            var update4 = "2017-11-01T09:43:11+00:00 KRAKEN USD ETH 0.035";
            var update5 = "2017-11-01T09:43:23+00:00 GDAX ETH USD 28.0";
            var updates = new List<string>(new string[] { update1, update11, update111, update1111, update2, update3, update4, update5 });

            var request = "EXCHANGE_RATE_REQUEST KRAKEN BTC GDAX USD";

            // Act
            foreach (var u in updates)
            {
                PriceUpdate priceUpdate = null;
                if (PriceUpdate.TryParse(u, out priceUpdate))
                {
                    processor.PriceUpdate(priceUpdate);
                }
            }

            ExchangeRateRequest exchangerr = null;
            ExchangeRateResponse response = null;
            if (ExchangeRateRequest.TryParse(request, out exchangerr))
            {
                response = processor.ExchangeRate(exchangerr);
            }

            // Assert
            var expectedPath = new List<ExchangeCurrency>() { new ExchangeCurrency("KRAKEN", "BTC"), new ExchangeCurrency("KRAKEN", "USD"), new ExchangeCurrency("GDAX", "USD") };
            Assert.Equal(expectedPath, response.Path);
            Assert.Equal(1000.0, response.Rate);
        }
    }
}
