using MyCpu.Application.Factories;
using MyCpu.Domain.Core;
using MyCpu.Shared.Enums;
using MyCpu.Shared.Interfaces;

CPU cpu = CPUFactory.CreateCPUWithComponents(out IMemory memory, out IRegisters registers);

byte[] program = new byte[]
{
    (byte)OpCode.LDA, 0x00,  // ACC = 200
    (byte)OpCode.SUB, 0x01,  // ACC += 100
    (byte)OpCode.HLT
};


// Inicializar memoria con valores
memory.WriteByte(0x00, 50);
memory.WriteByte(0x01, 100);

cpu.LoadProgram(program, 0x10); // que empiece desde la dirección 0x10
registers.PC.Set(0x10);      // Asegurarse que el PC apunte al inicio del programa
cpu.Run();

//Console.WriteLine(memory.ReadByte(0x02)); ; // 15
//Console.WriteLine(registers.Flags);       // Flags actualizadas correctamente
Console.WriteLine("ACC: " + registers.ACC.Value);       // 44
Console.WriteLine("Flags: " + registers.Flags);


void TestFlow()
{
    CPU cpu = CPUFactory.CreateCPUWithComponents(out IMemory memory, out IRegisters registers);

    // Programa de prueba
    byte[] program = new byte[]
    {
    (byte)OpCode.LDA, 0x00,   // ACC = Mem[0] -> 0x00
    (byte)OpCode.ADD, 0x01,   // ACC += Mem[1] -> overflow / carry posible
    (byte)OpCode.SUB, 0x02,   // ACC -= Mem[2] -> negativo posible
    (byte)OpCode.XOR, 0x03,   // ACC XOR Mem[3] -> solo para flags
    (byte)OpCode.NOT,          // ACC = ~ACC -> afecta Negative y Zero
    (byte)OpCode.HLT
    };

    // Inicializar memoria para disparar flags
    memory.WriteByte(0x00, 0x00); // Para disparar Zero
    memory.WriteByte(0x01, 0xFF); // Para disparar Carry/Overflow
    memory.WriteByte(0x02, 0x10); // Para generar negativo en SUB
    memory.WriteByte(0x03, 0xFF); // XOR para modificar flags

    cpu.LoadProgram(program);
    cpu.Run();

    // Resultados finales
    Console.WriteLine($"ACC: {registers.ACC.Value:X2}");
    Console.WriteLine($"Flags: {registers.Flags}");
    Console.WriteLine($"Zero: {registers.GetFlag(Flags.Zero)}");
    Console.WriteLine($"Carry: {registers.GetFlag(Flags.Carry)}");
    Console.WriteLine($"Negative: {registers.GetFlag(Flags.Negative)}");
    Console.WriteLine($"Overflow: {registers.GetFlag(Flags.Overflow)}");
}

TestFlow();