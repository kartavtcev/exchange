using System;
using Exchange.Core.Models;
using Exchange.Core;
using System.Collections.Generic;

namespace Exchange.ConsoleUi
{
    class Program
    {
        private const string EnterInput = "Enter 'exit' to exit. Enter input:";
        private const string Exit = "exit";

        //static void Main(string[] args)
        static void Main()
        {
            var processor = new ExchangeProcessor();

            var update1 = "2017-11-01T09:42:23+02:00 KRAKEN BTC USD 1000.0";
            //var update2 = "2017-11-01T09:43:11+00:00 KRAKEN USD BTC 0.002";
            var update3 = "2017-11-01T09:43:23+00:00 GDAX BTC USD 1001.0";
            var update4 = "2017-11-01T09:43:11+00:00 KRAKEN USD ETH 0.029";
            var update5 = "2017-11-01T09:43:23+00:00 GDAX ETH USD 35.0";
            var updates = new List<string>(new string[] { update1, update3, update4, update5 });
                        
            var request = "EXCHANGE_RATE_REQUEST KRAKEN BTC GDAX USD";

            foreach (var u in updates)
            {
                PriceUpdate priceUpdate = null;
                if (PriceUpdate.TryParse(u, out priceUpdate))
                {
                    processor.PriceUpdate(priceUpdate);
                }
            }

            ExchangeRateRequest exchangerr = null;
            if (ExchangeRateRequest.TryParse(request, out exchangerr))
            {
                var response = processor.ExchangeRate(exchangerr);
                Console.WriteLine(response);
            }





            //while (true)
            //{
            //    Console.WriteLine(EnterInput);
            //    string line = Console.ReadLine();
            //    if (line == Exit) break;

            //    if (ExchangeRateRequest.IsExchangeRateRequest(line))
            //    {
            //        ExchangeRateRequest exchangerr = null;
            //        if (ExchangeRateRequest.TryParse(line, out exchangerr))
            //        {
            //            var response = processor.ExchangeRate(exchangerr);
            //            Console.WriteLine(response);
            //        }
            //    }
            //    if(PriceUpdate.IsPriceUpdate(line))
            //    {
            //        PriceUpdate priceUpdate = null;
            //        if (PriceUpdate.TryParse(line, out priceUpdate))
            //        {
            //            processor.PriceUpdate(priceUpdate);
            //        }
            //    }
            //}
        }
    }
}