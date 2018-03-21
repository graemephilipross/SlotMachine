using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Core;
using System.Collections.Generic;

namespace SlotMachineTest
{
    [TestClass]
    public class SlotMachineTest
    {
        [TestMethod]
        public void Test_Deposit_Win_Should_double_stake()
        {
            var grid = new Mock<IGrid>();
            grid.Setup(g => g.CalculateMultipler()).Returns(2m);
            Func<IGrid> gridFactory = () => grid.Object;

            var slotMachine = new SlotMachine(gridFactory);
            slotMachine.Deposit(1m);
            var outcome = slotMachine.PullHandle(1m);
            Assert.AreEqual(2m, outcome.Balance);
        }
    }
}
