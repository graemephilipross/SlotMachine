using System;

namespace Core
{
    [Flags]
    public enum SymbolType
    {
        Wildcard = 0,
        Apple = 1,
        Banana = 2,
        Pineapple = 4
    }

    public class Symbol
    {
        public SymbolType Type { get; set; }
        public decimal Coefficient { get; set; }
    }
}
