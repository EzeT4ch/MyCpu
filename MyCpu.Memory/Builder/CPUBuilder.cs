using MyCpu.Domain.Core;
using MyCpu.Domain.Interfaces;

namespace MyCpu.Domain.Builder
{
    public class CPUBuilder
    {
        private IMemory? _memory;
        private IRegisters? _registers;
        private IALU? _alu;

        public CPUBuilder WithMemory(IMemory memory)
        {
            _memory = memory;
            return this;
        }

        public CPUBuilder WithMemory(int size)
        {
            _memory = new Memory(size);
            return this;
        }

        public CPUBuilder WithRegisters(IRegisters registers)
        {
            _registers = registers;
            return this;
        }

        public CPUBuilder WithALU(IALU alu)
        {
            _alu = alu;
            return this;
        }

        public CPU Build()
        {
            _registers ??= new Registers();
            _memory ??= new Memory(256);
            _alu ??= new ALU(_registers);

            return new CPU(_memory, _registers, _alu);
        }
    }
}