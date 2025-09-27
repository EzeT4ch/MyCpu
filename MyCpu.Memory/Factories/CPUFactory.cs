using MyCpu.Domain.Core;
using MyCpu.Domain.Interfaces;

namespace MyCpu.Domain.Factories
{
    public static class CPUFactory
    {
        public static CPU CreateCPU(int memorySize = 256)
        {
            Registers registers = new ();
            Core.Memory memory = new (memorySize);
            ALU alu = new (registers);

            return new CPU(memory, registers, alu);
        }

        public static CPU CreateCPUWithComponents(out IMemory memory, out IRegisters registers, int memorySize = 256)
        {
            registers = new Registers();
            memory = new Domain.Core.Memory(memorySize);
            ALU alu = new (registers);

            return new (memory, registers, alu);
        }
    }
}