using MyCpu.Shared.Structures;

namespace MyCpu.Shared.Interfaces
{
    public interface IALU
    {
        AluResult Add(byte a, byte b);
        AluResult Sub(byte a, byte b);
        AluResult And(byte a, byte b);
        AluResult Or(byte a, byte b);
        AluResult Xor(byte a, byte b);
        AluResult Not(byte a);
    }
}
