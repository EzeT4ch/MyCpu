using MyCpu.Domain.Enums;
using MyCpu.Domain.Factories;
using MyCpu.Domain.Interfaces;

class Program
{
    static void Main()
    {
        byte[] program = {
            (byte)OpCode.LDA, 0x10,  // ACC = memoria[0x10] (5)
            (byte)OpCode.AND, 0x11,  // ACC = ACC & memoria[0x11] (3) → 5 & 3 = 1
            (byte)OpCode.STA, 0x12,  // memoria[0x12] = ACC
            (byte)OpCode.HLT
        };
        var cpu2 = CPUFactory.CreateCPUWithComponents(out IMemory? memory, out IRegisters? registers);

        memory.WriteByte(0x10, 5);
        memory.WriteByte(0x11, 3);

        cpu2.LoadProgram(program);
        cpu2.Run();

        Console.WriteLine(memory.ReadByte(0x12)); // 1
        Console.WriteLine(registers.Flags);
    }
}