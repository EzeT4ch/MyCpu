using MyCpu.Domain.Enums;
using MyCpu.Domain.Interfaces;

namespace MyCpu.Domain.Core
{
    public class CPU
    {
        private readonly IMemory _memory;
        private readonly IRegisters _registers;
        private readonly IALU _alu;
        private bool _halted;

        public CPU(IMemory memory, IRegisters registers, IALU alu)
        {
            _memory = memory;
            _registers = registers;
            _alu = alu;
            _registers.Clear();
        }

        public void LoadProgram(byte[] program, int startAddress = 0)
        {
            for (int i = 0; i < program.Length; i++)
                _memory.WriteByte(startAddress + i, program[i]);

            _registers.PC.Set(startAddress);
        }

        public void Run()
        {
            while (!_halted)
            {
                Step();
            }
        }

        private byte ReadNextByte()
        {
            byte val = _memory.ReadByte(_registers.PC.Value);
            _registers.PC.Increment();
            return val;
        }

        public void Step()
        {
            // Fetch
            byte opcode = ReadNextByte();
            _registers.IR = opcode;

            // Decode & Execute
            switch ((OpCode)opcode)
            {
                case OpCode.NOP:
                    break;

                case OpCode.LDA:
                    {
                        byte addr = ReadNextByte();
                        _registers.ACC = _memory.ReadByte(addr);
                        break;
                    }

                case OpCode.STA:
                    {
                        byte addr = ReadNextByte();
                        _memory.WriteByte(addr, _registers.ACC);
                        break;
                    }

                case OpCode.ADD:
                    {
                        byte addr = ReadNextByte();
                        _registers.ACC = _alu.Add(_registers.ACC, _memory.ReadByte(addr));
                        break;
                    }

                case OpCode.SUB:
                    {
                        byte addr = ReadNextByte();
                        _registers.ACC = _alu.Sub(_registers.ACC, _memory.ReadByte(addr));
                        break;
                    }

                case OpCode.JMP:
                    {
                        byte addr = ReadNextByte();
                        _registers.PC.Set(addr);
                        break;
                    }

                case OpCode.CMP:
                    {
                        byte addr = ReadNextByte();
                        _alu.Sub(_registers.ACC, _memory.ReadByte(addr)); // afecta solo flags
                        break;
                    }

                case OpCode.HLT:
                    _halted = true;
                    break;

                case OpCode.AND:
                    {
                        byte addr = ReadNextByte();
                        _registers.ACC = _alu.And(_registers.ACC, _memory.ReadByte(addr));
                        break;
                    }

                case OpCode.OR:
                    {
                        byte addr = ReadNextByte();
                        _registers.ACC = _alu.Or(_registers.ACC, _memory.ReadByte(addr));
                        break;
                    }

                case OpCode.XOR:
                    {
                        byte addr = ReadNextByte();
                        _registers.ACC = _alu.Xor(_registers.ACC, _memory.ReadByte(addr));
                        break;
                    }
                case OpCode.NOT:
                    {
                        _registers.ACC = _alu.Not(_registers.ACC);
                        break;
                    }

                default:
                    throw new InvalidOperationException($"Unknown opcode {opcode:X2}");
            }
        }
    }
}
