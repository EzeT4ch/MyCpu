namespace MyCpu.Shared.Enums
{
    [Flags]
    public enum Flags : byte
    {
        None = 0,
        Zero = 1 << 0,       // Z
        Carry = 1 << 1,   // N
        Negative = 1 << 2,  // H
        Overflow = 1 << 3       // C
    }
}
