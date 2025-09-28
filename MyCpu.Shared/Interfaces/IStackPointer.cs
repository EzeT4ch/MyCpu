namespace MyCpu.Shared.Interfaces
{
    public interface IStackPointer
    {
        byte Value { get; }
        void Reset();
        void Push();
        void Pop();
    }
}
