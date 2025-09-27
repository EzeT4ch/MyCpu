using MyCpu.Shared.Enums;
using MyCpu.Shared.Interfaces;
using MyCpu.Shared.Structures;

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
                        _registers.ACC.Value = _memory.ReadByte(addr);
                        break;
                    }

                case OpCode.STA:
                    {
                        byte addr = ReadNextByte();
                        _memory.WriteByte(addr, _registers.ACC.Value);
                        break;
                    }

                case OpCode.ADD:
                    {
                        byte addr = ReadNextByte();
                        AluResult result = _alu.Add(_registers.ACC.Value, _memory.ReadByte(addr));
                        _registers.ACC.Value = result.Value;
                        _registers.ApplyFlags(result.Flags);
                        break;
                    }

                case OpCode.SUB:
                    {
                        byte addr = ReadNextByte();
                        AluResult result = _alu.Sub(_registers.ACC.Value, _memory.ReadByte(addr));
                        _registers.ACC.Value = result.Value;
                        _registers.ApplyFlags(result.Flags);
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
                        AluResult cmpResult = _alu.Sub(_registers.ACC.Value, _memory.ReadByte(addr));
                        _registers.ApplyFlags(cmpResult.Flags); // solo flags, ACC no cambia

                        break;
                    }

                case OpCode.HLT:
                    _halted = true;
                    break;

                case OpCode.AND:
                    {
                        byte addr = ReadNextByte();
                        AluResult result = _alu.And(_registers.ACC.Value, _memory.ReadByte(addr));
                        _registers.ACC.Value = result.Value;
                        _registers.ApplyFlags(result.Flags);
                        break;
                    }

                case OpCode.OR:
                    {
                        byte addr = ReadNextByte();
                        AluResult result = _alu.Or(_registers.ACC.Value, _memory.ReadByte(addr));
                        _registers.ACC.Value = result.Value;
                        _registers.ApplyFlags(result.Flags);
                        break;
                    }

                case OpCode.XOR:
                    {
                        byte addr = ReadNextByte();
                        AluResult result = _alu.Xor(_registers.ACC.Value, _memory.ReadByte(addr));
                        _registers.ACC.Value = result.Value;
                        _registers.ApplyFlags(result.Flags);
                        break;
                    }
                case OpCode.NOT:
                    {
                        AluResult result = _alu.Not(_registers.ACC.Value);
                        _registers.ACC.Value = result.Value;
                        _registers.ApplyFlags(result.Flags);
                        break;
                    }

                default:
                    throw new InvalidOperationException($"Unknown opcode {opcode:X2}");
            }
        }
    }
}
