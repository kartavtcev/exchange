using System;
using Exchange.Core.Models;

namespace Exchange.ConsoleUi
{
    class Program
    {
        private const string EnterInput = "Enter input:";
        private const string Exit = "exit";

        static void Main(string[] args)
        {
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
                        // TODO:
                    }
                }
                if(PriceUpdate.IsPriceUpdate(line))
                {
                    PriceUpdate priceUpdate = null;
                    if (PriceUpdate.TryParse(line, out priceUpdate))
                    {
                        // TODO:
                    }
                }
            }
        }
    }
}