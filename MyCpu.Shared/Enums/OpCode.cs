namespace MyCpu.Shared.Enums
{
    public enum OpCode
    {
        NOP = 0x00,
        LDA = 0x01,
        STA = 0x02,
        ADD = 0x03,
        SUB = 0x04,
        JMP = 0x05,
        AND = 0x06,
        OR = 0x07,
        XOR = 0x08,
        NOT = 0x09,
        CMP = 0x0A,
        HLT = 0xFF
    }
}
