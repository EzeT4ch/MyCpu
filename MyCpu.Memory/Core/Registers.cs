using MyCpu.Domain.Enums;
using MyCpu.Domain.Interfaces;

namespace MyCpu.Domain.Core
{
    public class Registers : IRegisters
    {
        public byte ACC { get; set; } // Accumulator
        public ushort PC { get; set; }

        public byte IR { get; set; }

        public byte SP { get; set; }

        public Flags Flags { get; private set; }

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
            ACC = 0;
            PC = 0;
            IR = 0;
            SP = 0xFF; // convención: stack crece hacia abajo
            Flags = Flags.None;
        }

        public override string ToString()
        {
            return $"ACC={ACC:X2} PC={PC:X2} SP={SP:X2} Flags=[{Flags}]";
        }
    }
}
