using MyCpu.Shared.Interfaces;

namespace MyCpu.Domain.Core
{
    public class Memory : IMemory
    {
        private readonly byte[] _data;
        private readonly int Size;

        public Memory(int size)
        {
            Size = size;
            _data = new byte[size];
        }

        /// <summary>
        /// Reads a byte from the specified address within the data buffer.
        /// </summary>
        /// <param name="address">The zero-based index of the byte to read. Must be greater than or equal to 0 and less than the size of the
        /// buffer.</param>
        /// <returns>The byte value located at the specified address.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="address"/> is less than 0 or greater than or equal to the size of the buffer.</exception>
        public byte ReadByte(int address)
        {
            // TODO: exception handling for out-of-bounds access
            if (address < 0 || address >= Size)
                throw new ArgumentOutOfRangeException(nameof(address), "Address is out of bounds.");
            return _data[address];
        }

        /// <summary>
        /// Writes a byte value to the specified address within the data buffer.
        /// </summary>
        /// <param name="address">The zero-based index in the buffer at which to write the value. Must be greater than or equal to 0 and less
        /// than the buffer size.</param>
        /// <param name="value">The byte value to write at the specified address.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="address"/> is less than 0 or greater than or equal to the buffer size.</exception>
        public void WriteByte(int address, byte value)
        {
            // TODO: refactor validation to make it reusable
            if (address < 0 || address >= Size)
                throw new ArgumentOutOfRangeException(nameof(address), "Address is out of bounds.");
            _data[address] = value;
        }

        /// <summary>
        /// Reads a 16-bit unsigned value from the specified address in the underlying data buffer.
        /// </summary>
        /// <param name="address">The zero-based index of the first byte to read. Must be within the valid range of the buffer.</param>
        /// <returns>A 16-bit unsigned integer composed from the byte at the specified address and the following byte, using
        /// little-endian order.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="address"/> is less than 0 or when <paramref name="address"/> plus 1 exceeds the
        /// bounds of the buffer.</exception>
        public ushort ReadWord(int address)
        {
            if (address < 0 || address + 1 >= Size)
                throw new ArgumentOutOfRangeException(nameof(address), "Address is out of bounds.");
            return (ushort)(_data[address] | (_data[address + 1] << 8));
        }

        /// <summary>
        /// Writes a 16-bit unsigned value to the specified address in the underlying data buffer.
        /// </summary>
        /// <param name="address">The zero-based index in the buffer at which to write the lower byte of the value. Must be within the valid
        /// range so that both this address and the next are within bounds.</param>
        /// <param name="value">The 16-bit unsigned value to write. The lower byte is written to <paramref name="address"/>, and the upper
        /// byte to <paramref name="address"/> + 1.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="address"/> is less than 0 or when <paramref name="address"/> + 1 exceeds the
        /// buffer size.</exception>
        public void WriteWord(int address, ushort value)
        {
            if (address < 0 || address + 1 >= Size)
                throw new ArgumentOutOfRangeException(nameof(address), "Address is out of bounds.");
            _data[address] = (byte)(value & 0xFF);
            _data[address + 1] = (byte)((value >> 8) & 0xFF);
        }

        /// <summary>
        /// Removes all elements from the collection, resetting its contents to the default values.
        /// </summary>
        public void Clear()
        {
            Array.Clear(_data, 0, Size);
        }

        /// <summary>
        /// Gets the current size value.
        /// </summary>
        /// <returns>The value of the size. The meaning of the value depends on the context in which the method is used.</returns>
        public int GetSize()
        {
            return Size;
        }

        /// <summary>
        /// Returns a formatted hexadecimal string representation of a range of bytes from the underlying data buffer.
        /// </summary>
        /// <remarks>Each byte is represented as a two-digit hexadecimal value separated by spaces. A line
        /// break is inserted after every 16 bytes for readability.</remarks>
        /// <param name="start">The zero-based index of the first byte to include in the dump. Must be greater than or equal to 0 and less
        /// than the size of the buffer.</param>
        /// <param name="length">The number of bytes to include in the dump. The range defined by <paramref name="start"/> and <paramref
        /// name="length"/> must not exceed the size of the buffer.</param>
        /// <returns>A string containing the hexadecimal values of the specified range of bytes, formatted in groups of 16 per
        /// line.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="start"/> is less than 0 or the range defined by <paramref name="start"/> and
        /// <paramref name="length"/> exceeds the size of the buffer.</exception>
        public string Dump(int start, int length)
        {
            //TODO: refactor to show ASCII representation on the right side
            if (start < 0 || start + length > Size)
                throw new ArgumentOutOfRangeException(nameof(start), "Dump range is out of bounds.");
            var dump = new System.Text.StringBuilder();
            for (int i = start; i < start + length; i++)
            {
                dump.AppendFormat("{0:X2} ", _data[i]);
                if ((i - start + 1) % 16 == 0)
                    dump.AppendLine();
            }
            return dump.ToString();
        }
    }
}
