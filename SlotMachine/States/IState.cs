using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.States
{
    public enum GameState
    {
        PlayerStart,
        PlayerTurn,
        Finished,
        Quit
    }
    public interface IState
    {
        GameState Next();
    }
}
