using MyCpu.Domain.Core;
using MyCpu.Shared.Interfaces;

namespace MyCpu.Application.Factories;

public static class CPUFactory
{
    public static CPU CreateCPU(int memorySize = 256)
    {
        Registers registers = new ();
        Domain.Core.Memory memory = new (memorySize);
        ALU alu = new (registers);

        return new CPU(memory, registers, alu);
    }

    public static CPU CreateCPUWithComponents(out IMemory memory, out IRegisters registers, int memorySize = 256)
    {
        registers = new Registers();
        memory = new Memory(memorySize);
        ALU alu = new (registers);

        return new (memory, registers, alu);
    }
}