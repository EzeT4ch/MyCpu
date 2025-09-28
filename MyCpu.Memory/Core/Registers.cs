using MyCpu.Shared.Enums;
using MyCpu.Shared.Interfaces;

namespace MyCpu.Domain.Core
{
    public sealed class Registers : IRegisters
    {
        public IRegisterAcc ACC { get; set; }
        public IProgramCounter PC { get; set; }

        public byte IR { get; set; }

        public IStackPointer SP { get; set; }

        public Flags Flags { get; private set; }

        public Registers(int memorySize = 512)
        {
            ACC = new RegisterAcc(this);
            PC = new ProgramCounter(memorySize);
            SP = new StackPointer();
            Clear();
        }

        public void SetFlag(Flags flag, bool value)
        {
            if (value)
                Flags |= flag;
            else
                Flags &= ~flag;
        }

        public bool GetFlag(Flags flag) => (Flags & flag) != 0;

        public void Clear()
        {
            ((RegisterAcc)ACC).Reset();
            PC.Reset();
            IR = 0;
            SP.Reset(); // convención: stack crece hacia abajo o eso encontre
            Flags = Flags.None;
        }

        public override string ToString()
        {
            return $"ACC={ACC:X2} PC={PC:X2} SP={SP:X2} Flags=[{Flags}]";
        }

        public void ApplyFlags(Flags flags)
        {
            Flags &= ~(Flags.Zero | Flags.Carry | Flags.Negative | Flags.Overflow);
            Flags |= flags;
        }
    }
}
