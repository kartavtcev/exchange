using System;
using Exchange.Core.Models;
using Exchange.Core;

namespace Exchange.ConsoleUi
{
    class Program
    {
        private const string EnterInput = "Enter input text, 'exit' to exit:";
        private const string Exit = "exit";

        //static void Main(string[] args)
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
                        if (response != null)
                        {
                            Console.WriteLine(response);
                        }
                        else
                        {
                            Console.WriteLine("NO PATH: s<-->d vertexes are not connected OR infinite positive cycle is detected");
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