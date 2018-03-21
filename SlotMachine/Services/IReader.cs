namespace Core.Services
{
    public interface IReader
    {
        decimal ReadDeposit();
        decimal ReadStake();
        bool ReadPlayAgain();
    }
}
