using MyCpu.Domain.Enums;
using MyCpu.Domain.Interfaces;

namespace MyCpu.Domain.Core
{
    internal class ALU : IALU
    {
        private readonly IRegisters _registers;

        public ALU(IRegisters registers)
        {
            _registers = registers;
        }

        /// <summary>
        /// Adds two 8-bit unsigned values and updates processor flags based on the result.
        /// </summary>
        /// <remarks>After the addition, the Carry, Zero, Negative, and Overflow flags are updated to
        /// reflect the result. This method is typically used in emulation scenarios where flag updates are required for
        /// correct processor state.</remarks>
        /// <param name="a">The first operand to add. Represents an 8-bit unsigned value.</param>
        /// <param name="b">The second operand to add. Represents an 8-bit unsigned value.</param>
        /// <returns>The 8-bit unsigned result of adding <paramref name="a"/> and <paramref name="b"/>. Only the least
        /// significant 8 bits are returned.</returns>
        public byte Add(byte a, byte b)
        {
            int result = a + b;

            // Update flags
            _registers.SetFlag(Flags.Carry, result > 0xFF);
            _registers.SetFlag(Flags.Zero, (result & 0xFF) == 0);
            _registers.SetFlag(Flags.Negative, (result & 0x80) != 0);
            _registers.SetFlag(Flags.Overflow, ((a ^ result) & (b ^ result) & 0x80) != 0);

            return (byte)(result & 0xFF);
        }

        /// <summary>
        /// Subtracts the value of the second operand from the first operand and returns the result as an 8-bit unsigned
        /// integer.
        /// </summary>
        /// <remarks>Processor flags such as Carry, Zero, Negative, and Overflow are updated based on the
        /// result of the subtraction. This method is typically used in contexts where flag updates are required for
        /// subsequent operations.</remarks>
        /// <param name="a">The minuend. The 8-bit unsigned integer from which <paramref name="b"/> is subtracted.</param>
        /// <param name="b">The subtrahend. The 8-bit unsigned integer to subtract from <paramref name="a"/>.</param>
        /// <returns>An 8-bit unsigned integer representing the result of <paramref name="a"/> minus <paramref name="b"/>. The
        /// result is truncated to fit within the byte range (0–255).</returns>
        public byte Sub(byte a, byte b)
        {
            int result = a - b;

            // Update flags
            _registers.SetFlag(Flags.Carry, result < 0);
            _registers.SetFlag(Flags.Zero, (result & 0xFF) == 0);
            _registers.SetFlag(Flags.Negative, (result & 0x80) != 0);
            _registers.SetFlag(Flags.Overflow, ((a ^ b) & (a ^ result) & 0x80) != 0);

            return (byte)(result & 0xFF);
        }

        /// <summary>
        /// Performs a bitwise AND operation on two 8-bit unsigned integers.
        /// </summary>
        /// <param name="a">The first operand for the bitwise AND operation.</param>
        /// <param name="b">The second operand for the bitwise AND operation.</param>
        /// <returns>A byte value representing the result of the bitwise AND operation between <paramref name="a"/> and <paramref
        /// name="b"/>.</returns>
        public byte And(byte a, byte b)
        {
            byte result = (byte)(a & b);
            UpdateLogicFlags(result);
            return result;
        }

        /// <summary>
        /// Performs a bitwise OR operation on two 8-bit unsigned integers.
        /// </summary>
        /// <remarks>After performing the operation, logic flags are updated to reflect the result. This
        /// method can be used to combine bit patterns or set specific bits in a value.</remarks>
        /// <param name="a">The first operand for the bitwise OR operation.</param>
        /// <param name="b">The second operand for the bitwise OR operation.</param>
        /// <returns>A byte value representing the result of the bitwise OR of the two operands.</returns>
        public byte Or(byte a, byte b)
        {
            byte result = (byte)(a | b);
            UpdateLogicFlags(result);
            return result;
        }

        /// <summary>
        /// Performs a bitwise exclusive OR (XOR) operation on two 8-bit unsigned integer values.
        /// </summary>
        /// <param name="a">The first operand for the bitwise XOR operation.</param>
        /// <param name="b">The second operand for the bitwise XOR operation.</param>
        /// <returns>A byte value that is the result of the bitwise XOR of the two operands.</returns>
        public byte Xor(byte a, byte b)
        {
            byte result = (byte)(a ^ b);
            UpdateLogicFlags(result);
            return result;
        }

        /// <summary>
        /// Computes the bitwise complement of the specified 8-bit unsigned integer.
        /// </summary>
        /// <param name="a">The value for which to compute the bitwise complement.</param>
        /// <returns>A byte value representing the bitwise complement of <paramref name="a"/>.</returns>
        public byte Not(byte a)
        {
            byte result = (byte)~a;
            UpdateLogicFlags(result);
            return result;
        }

        /// <summary>
        /// Updates the processor status flags based on the specified result value.
        /// </summary>
        /// <remarks>This method sets the Zero flag if <paramref name="result"/> is zero, and the Negative
        /// flag if the most significant bit of <paramref name="result"/> is set. The Carry and Overflow flags are
        /// cleared.</remarks>
        /// <param name="result">The result value used to determine the state of the Zero and Negative flags. The value is interpreted as an
        /// 8-bit unsigned integer.</param>
        private void UpdateLogicFlags(byte result)
        {
            _registers.SetFlag(Flags.Zero, result == 0);
            _registers.SetFlag(Flags.Negative, (result & 0x80) != 0);
            _registers.SetFlag(Flags.Carry, false);
            _registers.SetFlag(Flags.Overflow, false);
        }
    }
}
