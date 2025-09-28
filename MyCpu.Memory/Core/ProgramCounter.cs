using MyCpu.Shared.Interfaces;

namespace MyCpu.Domain.Core
{
    public class ProgramCounter : IProgramCounter
    {
        private readonly int _maxAddress;

        private int _value;

        public ProgramCounter(int maxAddress)
        {
            _maxAddress = maxAddress;
            _value = 0;
        }

        public int Value => _value;

        public void Increment()
        {
            _value = (_value + 1) % _maxAddress;
        }

        public void Set(int address)
        {
            if (address < 0 || address >= _maxAddress)
                throw new ArgumentOutOfRangeException(nameof(address), "Invalid memory address for PC.");

            _value = address;
        }

        public void Reset()
        {
            _value = 0x100;
        }
    }
}
