using MyCpu.Shared.Interfaces;

namespace MyCpu.Domain.Core
{
    public class StackPointer : IStackPointer
    {
        private byte _value;
        public byte Value => _value;


        public StackPointer()
        {
            Reset();
        }

        public void Pop()
        {
            _value++;
        }

        public void Push()
        {
            _value--;
        }

        public void Reset()
        {
            _value = 0xFF;
        }
    }
}
