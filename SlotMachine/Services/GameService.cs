using System;
using Core.States;

namespace Core.Services
{
    public interface IGameService
    {
        void Play();
    }

    public class GameService : IGameService
    {
        private GameState _state;
        private readonly Func<GameState, IState> _lookup;
        private readonly IWriter _writer;

        public GameService(Func<GameState, IState> lookup, IWriter writer)
        {
            _state = GameState.PlayerStart;
            _lookup = lookup;
            _writer = writer;
        }

        public void Play()
        {
            if (_state == GameState.Quit)
            {
                return;
            }

            try
            {
                _state = _lookup(_state)?.Next() ?? GameState.Quit;
            }
            catch (Exception e)
            {
                _writer.WriteError(e);
            }
            finally
            {
                Play();
            }
        }
    }
}
