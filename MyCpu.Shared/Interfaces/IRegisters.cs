using MyCpu.Shared.Enums;

namespace MyCpu.Shared.Interfaces
{
    public interface IRegisters
    {
        IRegisterAcc ACC { get; set; }
        IProgramCounter PC { get; set; }
        byte IR { get; set; }
        byte SP { get; set; }
        Flags Flags { get; }
        void SetFlag(Flags flag, bool value);
        void ApplyFlags(Flags flags);
        bool GetFlag(Flags flag);
        void Clear();
    }
}
