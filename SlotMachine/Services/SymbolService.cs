using System;

namespace Core.Services
{
    public interface ISymbolService
    {
        Symbol GetRandomSymbol();
    }

    public class SymbolService : ISymbolService
    {
        private readonly Random _random;

        public SymbolService()
        {
            _random = new Random();
        }

        public Symbol GetRandomSymbol()
        {
            var weight = _random.Next(0, 99) + 1;
            if (weight <= 45) return new Symbol { Type = SymbolType.Apple, Coefficient = 0.4m };
            if (weight <= 80) return new Symbol { Type = SymbolType.Banana, Coefficient = 0.6m };
            if (weight <= 95) return new Symbol { Type = SymbolType.Pineapple, Coefficient = 0.8m };
            return new Symbol { Type = SymbolType.Wildcard, Coefficient = 0m };
        }
    }
}
