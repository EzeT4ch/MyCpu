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

            _registers.PC = (ushort)startAddress;
        }

        public void Run()
        {
            while (!_halted)
            {
                Step();
            }
        }

        public void Step()
        {
            // Fetch
            byte opcode = _memory.ReadByte(_registers.PC);
            _registers.IR = opcode;
            _registers.PC++;

            // Decode & Execute
            switch ((OpCode)opcode)
            {
                case OpCode.NOP:
                    break;

                case OpCode.LDA:
                    {
                        byte addr = _memory.ReadByte(_registers.PC++);
                        _registers.ACC = _memory.ReadByte(addr);
                        break;
                    }

                case OpCode.STA:
                    {
                        byte addr = _memory.ReadByte(_registers.PC++);
                        _memory.WriteByte(addr, _registers.ACC);
                        break;
                    }

                case OpCode.ADD:
                    {
                        byte addr = _memory.ReadByte(_registers.PC++);
                        _registers.ACC = _alu.Add(_registers.ACC, _memory.ReadByte(addr));
                        break;
                    }

                case OpCode.SUB:
                    {
                        byte addr = _memory.ReadByte(_registers.PC++);
                        _registers.ACC = _alu.Sub(_registers.ACC, _memory.ReadByte(addr));
                        break;
                    }

                case OpCode.JMP:
                    {
                        byte addr = _memory.ReadByte(_registers.PC++);
                        _registers.PC = addr;
                        break;
                    }

                case OpCode.HLT:
                    _halted = true;
                    break;

                default:
                    throw new InvalidOperationException($"Unknown opcode {opcode:X2}");
            }
        }
    }
}
