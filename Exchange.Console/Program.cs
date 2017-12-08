using System;
using Exchange.Core.Models;
using Exchange.Core;

namespace Exchange.ConsoleUi
{
    class Program
    {
        private const string EnterInput = "Enter input text, 'exit' to exit:";
        private const string Exit = "exit";

        static void Main()
        {
            var processor = new ExchangeProcessor();

            while (true)
            {
                Console.WriteLine(EnterInput);
                string line = Console.ReadLine();
                if (line == Exit) break;

                if (ExchangeRateRequest.IsExchangeRateRequest(line))
                {
                    ExchangeRateRequest exchangerr = null;
                    if (ExchangeRateRequest.TryParse(line, out exchangerr))
                    {
                        var response = processor.ExchangeRate(exchangerr);
                        if (response.IsCycle)
                        {
                            Console.WriteLine("Cycle was found. Arbitrage.");
                        }
                        else if (response.IsHasPath)
                        {
                            Console.WriteLine(response);
                        }
                        else if (!response.IsHasPath)
                        {
                            Console.WriteLine("No path.");
                        }
                    }
                }
                if (PriceUpdate.IsPriceUpdate(line))
                {
                    PriceUpdate priceUpdate = null;
                    if (PriceUpdate.TryParse(line, out priceUpdate))
                    {
                        processor.PriceUpdate(priceUpdate);
                    }
                }
            }
        }
    }
}