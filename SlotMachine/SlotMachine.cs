using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services;

namespace Core
{
    public interface ISlotMachine
    {
        void Deposit(decimal seed);
        Outcome PullHandle(decimal stake);
    }

    public interface IGrid
    {
        decimal CalculateMultipler();
        IEnumerable<Row> Rows { get; set; }
    }

    public struct Row
    {
        public IEnumerable<Symbol> Symbols { get; set; }

        public decimal GetWinnings()
        {
            // check all other symbols (or wildcards) match
            var check = Symbols.Aggregate(0, (chk, s) =>
            {
                chk |= (int)s.Type;
                return chk;
            });

            var flag = (SymbolType)check;
            return new[] { SymbolType.Apple, SymbolType.Banana, SymbolType.Pineapple }.Contains(flag)
                ? Symbols.Sum(s => s.Coefficient)
                : 0;
        }
    }

    public class Grid : IGrid
    {
        public IEnumerable<Row> Rows { get; set; }

        public Grid(int rowCount, int columnCount, ISymbolService symbolService)
        {
            Rows = 
                Enumerable.Range(0, rowCount)
                    .Select(r => new Row
                    {
                        Symbols =
                            Enumerable.Range(0, columnCount)
                                .Select(c => symbolService.GetRandomSymbol())
                                .ToList()
                    })
                    .ToList();
        }

        public decimal CalculateMultipler() => Rows.Sum(r => r.GetWinnings());
    }

    public class SlotMachine : ISlotMachine
    {
        private readonly Func<IGrid> _gridFactory;

        private decimal _balance;

        public SlotMachine(Func<IGrid> gridFactory)
        {
            _gridFactory = gridFactory;
        }

        public void Deposit(decimal seed)
        {
            _balance = seed;
        }

        public Outcome PullHandle(decimal stake)
        {
            if (0 >= stake)
            {
                throw new ArgumentException("Stake must be a valid amount");
            }

            if (stake > _balance)
            {
                throw new Exception("Not enough balance to stake");
            }

            _balance -= stake;

            var grid = _gridFactory();
            var multiplier = grid.CalculateMultipler();
            var winnings = stake * multiplier;

            _balance += winnings;

            return new Outcome(grid, winnings, _balance);
        }
    }
}
