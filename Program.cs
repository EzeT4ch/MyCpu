using MyCpu.Domain.Factories;

class Program
{
    static void Main()
    {
        // Opci�n 1: Simple
        var cpu = CPUFactory.CreateCPU();
        
        // Opci�n 2: Con acceso a los componentes
        var cpu2 = CPUFactory.CreateCPUWithComponents(out var memory, out var registers);
        
        // Usar la CPU
        byte[] program = { 0x01, 0x10, 0xFF };
        cpu.LoadProgram(program);
        cpu.Run();
    }
}