using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Services;

namespace Core.States
{
    public class Finished : IState
    {
        private readonly IWriter _writer;
        private readonly IReader _reader;

        public Finished(IReader reader, IWriter writer)
        {
            _reader = reader;
            _writer = writer;
        }
        public GameState Next()
        {
            _writer.WriteFinished();

            return _reader.ReadPlayAgain() 
                ? GameState.PlayerStart 
                : GameState.Quit;
        }
    }
}
