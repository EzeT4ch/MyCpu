namespace MyCpu.Shared.Enums
{
    [Flags]
    public enum Flags : byte
    {
        None = 0,
        Zero = 1 << 0,       // Z
        Negative = 1 << 1,   // N
        Overflow = 1 << 2,  // H
        Carry = 1 << 3       // C
    }
}
