using MyCpu.Application.Factories;
using MyCpu.Domain.Core;
using MyCpu.Shared.Enums;
using MyCpu.Shared.Interfaces;

CPU cpu = CPUFactory.CreateCPUWithComponents(out IMemory memory, out IRegisters registers);

byte[] program = new byte[]
{
    (byte)OpCode.LDA, 0x00, // ACC = Mem[0]
    (byte)OpCode.ADD, 0x01, // ACC += Mem[1]
    (byte)OpCode.STA, 0x02, // Mem[2] = ACC
    (byte)OpCode.HLT        // Stop
};

// Inicializar memoria con valores
memory.WriteByte(0x00, 5);
memory.WriteByte(0x01, 10);

cpu.LoadProgram(program, 0x10); // que empiece desde la dirección 0x10
registers.PC.Set(0x10);      // Asegurarse que el PC apunte al inicio del programa
cpu.Run();

Console.WriteLine(memory.ReadByte(0x02)); ; // 15
Console.WriteLine(registers.Flags);       // Flags actualizadas correctamente
