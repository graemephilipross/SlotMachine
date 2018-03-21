using Core.Services;
using Host.app_start;
using Autofac;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Container.CreateContainer().Build();
            using (var session = container.BeginLifetimeScope())
            {
                session.Resolve<GameService>().Play();
            }
        }

        //static void Main(string[] args)
        //{
        //    // Poor mans DI (and lifetime management)
        //    var reader = new ConsoleReader();

        //    var writer = new ConsoleWriter();

        //    var slotMachineConfig = new SlotMachineConfig();

        //    var symbolService = new SymbolService();

        //    var girdFactory = new Func<Grid>(() => 
        //        new Grid(slotMachineConfig.GetRowCount(), slotMachineConfig.ColCount(), symbolService)
        //    );

        //    var slotMachineFactory = new Func<ISlotMachine>(() => 
        //        new SlotMachine(girdFactory)
        //    );

        //    var slotMachine = default(ISlotMachine);

        //    var stateProcessor = new Func<GameState, IState>(state =>
        //    {
        //        if (state == GameState.PlayerStart)
        //            return new PlayerStart(reader, writer, slotMachine = slotMachineFactory());
        //        if (state == GameState.PlayerTurn)
        //            return new PlayerTurn(reader, writer, slotMachine);
        //        if (state == GameState.Finished)
        //            return new Finished(reader, writer);
        //        throw new InvalidOperationException("Invalid state");
        //    });

        //    IGameService gameService = new GameService(stateProcessor, writer);
        //    gameService.Play();
        //}
    }
}
