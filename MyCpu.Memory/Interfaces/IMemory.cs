namespace MyCpu.Domain.Interfaces
{
    public interface IMemory
    {
        byte ReadByte(int address);
        void WriteByte(int address, byte value);
        ushort ReadWord(int address);
        void WriteWord(int address, ushort value);
        void Clear();
        int GetSize();
        string Dump(int start, int length);
    }
}
