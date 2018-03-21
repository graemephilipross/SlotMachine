using System;

namespace Core.Services
{
    public interface IWriter
    {
        void WriteError(Exception e);
        void WriteDeposit();
        void WriteStake();
        void WriteTurn(Outcome outcome);
        void WriteFinished();
    }
}
