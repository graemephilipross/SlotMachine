using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Services;

namespace Core.States
{
    public class PlayerTurn : IState
    {
        private readonly ISlotMachine _slotMachine;
        private readonly IReader _reader;
        private readonly IWriter _writer;

        public PlayerTurn(IReader reader, IWriter writer, ISlotMachine slotMachine)
        {
            _writer = writer;
            _reader = reader;
            _slotMachine = slotMachine;
        }
        public GameState Next()
        {
            _writer.WriteStake();

            var stake = _reader.ReadStake();
            var outcome = _slotMachine.PullHandle(stake);

            _writer.WriteTurn(outcome);

            return outcome.IsGameOver() 
                ? GameState.Finished 
                : GameState.PlayerTurn;
        }
    }
}
