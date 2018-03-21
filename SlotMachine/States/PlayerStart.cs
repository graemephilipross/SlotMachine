using Core.Services;

namespace Core.States
{
    public class PlayerStart : IState
    {
        private readonly IReader _reader;
        private readonly ISlotMachine _slotMachine;
        private readonly IWriter _writer;

        public PlayerStart(IReader reader, IWriter writer, ISlotMachine slotMachine)
        {
            _writer = writer;
            _slotMachine = slotMachine;
            _reader = reader;
        }

        public GameState Next()
        {
            _writer.WriteDeposit();

            var seed = _reader.ReadDeposit();
            _slotMachine.Deposit(seed);

            return GameState.PlayerTurn;
        }
    }
}
