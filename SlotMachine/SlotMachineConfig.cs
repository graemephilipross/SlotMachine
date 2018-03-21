namespace Core
{
    public interface ISlotMachineConfig
    {
        int GetRowCount();
        int ColCount();
    }

    public class SlotMachineConfig : ISlotMachineConfig
    {
        public int GetRowCount() => 4;
        public int ColCount() => 3;
    }
}
