using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Services;

namespace Host.services
{
    public class ConsoleWriter : IWriter
    {
        public void WriteError(Exception e)
        {
            Console.WriteLine("An error has occured: {0}", e);
        }

        public void WriteDeposit()
        {
            Console.WriteLine("Please deposit money you would like to play with");
        }

        public void WriteStake()
        {
            Console.WriteLine("Enter stake amount");
        }

        public void WriteTurn(Outcome outcome)
        {
            outcome.Grid.Rows.ToList().ForEach(row =>
            {
                Console.WriteLine();
                row.Symbols.ToList().ForEach(c => c.PrintSymbol());
            });

            Console.WriteLine();
            Console.WriteLine("You have won: {0}", outcome.Winnings);
            Console.WriteLine("Current balance is: {0}", outcome.Balance);
        }

        public void WriteFinished()
        {
            Console.WriteLine("Sorry, game over. Play again Y / N ?");
        }
    }

    public static class SymbolExtensions
    {
        private static readonly IDictionary<SymbolType, char> Printer;

        static SymbolExtensions()
        {
            Printer = new Dictionary<SymbolType, char>
            {
                {SymbolType.Apple, 'A'},
                {SymbolType.Banana, 'B'},
                {SymbolType.Pineapple, 'P'},
                {SymbolType.Wildcard, '*'},
            };
        }
        public static void PrintSymbol(this Symbol symbol)
        {
            char print;
            var res = Printer.TryGetValue(symbol.Type, out print) ? print : 'x';
            Console.Write(res);
        }
    }
}
