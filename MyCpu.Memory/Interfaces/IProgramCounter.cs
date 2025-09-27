namespace MyCpu.Domain.Interfaces
{
    public interface IProgramCounter
    {
        int Value { get; }
        void Increment();
        void Set(int address);
        void Reset();
    }
}
