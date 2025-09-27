using MyCpu.Domain.Factories;
using MyCpu.Domain.Interfaces;

class Program
{
    static void Main()
    {
        var cpu2 = CPUFactory.CreateCPUWithComponents(out IMemory? memory, out IRegisters? registers);

        byte[] program = { 0x01, 0x10, 0xFF };
        cpu2.LoadProgram(program);
        cpu2.Run();
    }
}