using System;
using System.Text.RegularExpressions;
using Core.Services;

namespace Host.services
{
    public class ConsoleReader : IReader
    {
        private static readonly Regex Dolla;
        private static readonly Regex TryAgainRegex;

        static ConsoleReader()
        {
            Dolla = new Regex(@"^[0-9]([.,][0-9]{1,2})?$");
            TryAgainRegex = new Regex(@"^[YyNn]$");
        }
        public decimal ReadDeposit() => ReadAmount();

        public decimal ReadStake() => ReadAmount();

        public decimal ReadAmount() => Read(Dolla, Convert.ToDecimal);

        public bool ReadPlayAgain() => Read(TryAgainRegex, input => input.ToLowerInvariant() == "y");

        public T Read<T>(Regex regex, Func<string, T> onSuccess)
        {
            var input = Console.ReadLine() ?? "";
            var match = regex.Match(input);
            if (match.Success)
            {
                return onSuccess(input);
            }

            throw new ArgumentException("Invalid option.Please try again");
        }
    }
}
