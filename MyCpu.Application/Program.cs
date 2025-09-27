using MyCpu.Application.Factories;
using MyCpu.Domain.Core;
using MyCpu.Shared.Enums;
using MyCpu.Shared.Interfaces;

byte[] program = {
    (byte)OpCode.LDA, 0x10,  // ACC = memoria[0x10] (5)
    (byte)OpCode.AND, 0x11,  // ACC = ACC & memoria[0x11] (3) → 5 & 3 = 1
    (byte)OpCode.STA, 0x12,  // memoria[0x12] = ACC
    (byte)OpCode.HLT
};

CPU cpu = CPUFactory.CreateCPUWithComponents(out IMemory memory, out IRegisters registers);

memory.WriteByte(0x10, 5);
memory.WriteByte(0x11, 3);

cpu.LoadProgram(program);
cpu.Run();

Console.WriteLine(memory.ReadByte(0x12)); // 1
Console.WriteLine(registers.Flags);       // Flags actualizadas correctamente
