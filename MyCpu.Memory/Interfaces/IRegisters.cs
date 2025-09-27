using MyCpu.Domain.Enums;

namespace MyCpu.Domain.Interfaces
{
    public interface IRegisters
    {
        byte ACC { get; set; }
        ushort PC { get; set; }
        byte IR { get; set; }
        byte SP { get; set; }
        Flags Flags { get; }
        void SetFlag(Flags flag, bool value);
        bool GetFlag(Flags flag);
        void Clear();
    }
}
