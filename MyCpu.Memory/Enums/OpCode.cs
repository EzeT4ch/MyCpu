namespace MyCpu.Domain.Enums
{
    public enum OpCode
    {
        NOP = 0x00, // No operation
        LDA = 0x01, // Load ACC from memory
        STA = 0x02, // Store ACC to memory
        ADD = 0x03, // Add memory to ACC
        SUB = 0x04, // Subtract memory from ACC
        JMP = 0x05, // Jump to address
        HLT = 0xFF  // Halt
    }
}
