using MyCpu.Shared.Enums;
using MyCpu.Shared.Interfaces;

namespace MyCpu.Domain.Core
{
    public sealed class RegisterAcc(IRegisters registers) : IRegisterAcc
    {
        private byte _value;
        private readonly IRegisters _registers = registers;

        public byte Value
        {
            get => _value;
            set
            {
                _value = value;
                var autoFlags = Flags.None;
                if (_value == 0) autoFlags |= Flags.Zero;
                if ((_value & 0x80) != 0) autoFlags |= Flags.Negative;
                SetFlags(autoFlags);
            }
        }

        public void SetFlags(Flags flags)
        {
            _registers.ApplyFlags(flags);
        }

        public void Reset()
        {
            _value = 0;
        }

        public Flags CurrentFlags => _registers.Flags;
    }
}
