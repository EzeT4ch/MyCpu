using MyCpu.Shared.Enums;

namespace MyCpu.Shared.Interfaces
{
    public interface IRegisterAcc
    {
        byte Value { get; set; }
        void SetFlags(Flags flags);
        Flags CurrentFlags { get; }
    }
}
