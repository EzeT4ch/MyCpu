namespace MyCpu.Domain.Interfaces
{
    public interface IALU
    {
        byte Add(byte a, byte b);
        byte Sub(byte a, byte b);
        byte And(byte a, byte b);
        byte Or(byte a, byte b);
        byte Xor(byte a, byte b);
        byte Not(byte a);
    }
}
