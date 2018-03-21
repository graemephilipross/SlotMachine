using System;
using Autofac;
using Core;
using Core.Services;
using Core.States;
using Host.services;

namespace Host.app_start
{
    public static class Container
    {
        public static ContainerBuilder CreateContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConsoleReader>().As<IReader>().SingleInstance();
            builder.RegisterType<ConsoleWriter>().As<IWriter>().SingleInstance();
            builder.RegisterType<SlotMachineConfig>().As<ISlotMachineConfig>().SingleInstance();
            builder.RegisterType<SymbolService>().As<ISymbolService>().SingleInstance();

            builder.Register(c => 
                new Grid(
                    c.Resolve<ISlotMachineConfig>().GetRowCount(), 
                    c.Resolve<ISlotMachineConfig>().ColCount(), 
                    c.Resolve<ISymbolService>()
                )
            ).As<IGrid>(); // instance per dep

            builder.Register<Func<IGrid>>(c => 
            {
                var context = c.Resolve<IComponentContext>();
                return () => context.Resolve<IGrid>();
            }).InstancePerLifetimeScope();

            builder.RegisterType<SlotMachine>().As<ISlotMachine>().InstancePerLifetimeScope();

            builder.RegisterType<PlayerStart>().Keyed<IState>(GameState.PlayerStart).InstancePerLifetimeScope();
            builder.RegisterType<PlayerTurn>().Keyed<IState>(GameState.PlayerTurn).InstancePerLifetimeScope();
            builder.RegisterType<Finished>().Keyed<IState>(GameState.Finished).InstancePerLifetimeScope();

            builder.Register<Func<GameState, IState>>(c =>
            {
                var context = c.Resolve<IComponentContext>();
                return state => context.ResolveKeyed<IState>(state);
            }).InstancePerLifetimeScope();

            builder.RegisterType<GameService>().InstancePerLifetimeScope();

            return builder;
        }
    }
}
