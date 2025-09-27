using MyCpu.Shared.Enums;

namespace MyCpu.Shared.Structures
{
    public readonly struct AluResult(byte value, Flags flags)
    {
        public byte Value { get; } = value;
        // Return N flags after operation
        public Flags Flags { get; } = flags;
    }
}
