namespace Core
{
    public class Outcome
    {
        public Outcome(IGrid grid, decimal winnings, decimal balance)
        {
            Grid = grid;
            Winnings = winnings;
            Balance = balance;
        }
        public IGrid Grid { get; private set; }
        public decimal Winnings { get; private set; }
        public decimal Balance { get; }
        public bool IsGameOver() => Balance == 0m;
    }
}
