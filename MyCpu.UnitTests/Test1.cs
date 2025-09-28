using MyCpu.Application.Factories;
using MyCpu.Domain.Core;
using MyCpu.Shared.Enums;
using MyCpu.Shared.Interfaces;

namespace MyCpu.UnitTests
{
    [TestClass]
    public sealed class Test1
    {
        private CPU _cpu;
        private IMemory _memory;
        private IRegisters _registers;

        [TestInitialize]
        public void Setup()
        {
            _cpu = CPUFactory.CreateCPUWithComponents(out _memory, out _registers);
        }

        [TestMethod]
        public void LDA_LoadsValueIntoACCAndSetsFlags()
        {
            // Inicializamos memoria
            _memory.WriteByte(CPU.ProgramStart + 0x00, 0x00); // Zero
            _memory.WriteByte(CPU.ProgramStart + 0x01, 0x80); // Negative

            // Programa para Zero
            byte[] program = { (byte)OpCode.LDA, (byte)(CPU.ProgramStart + 0x00), (byte)OpCode.HLT };
            _cpu.LoadProgram(program);
            _cpu.Run();

            Assert.AreEqual(0x00, _registers.ACC.Value);
            Assert.IsTrue(_registers.GetFlag(Flags.Zero));

            _registers.Clear();

            // Programa para Negative
            program = new byte[] { (byte)OpCode.LDA, (byte)(CPU.ProgramStart + 0x01), (byte)OpCode.HLT };
            _cpu.LoadProgram(program);
            _cpu.Run();

            Assert.AreEqual(0x80, _registers.ACC.Value);
            Assert.IsTrue(_registers.GetFlag(Flags.Negative));
        }


        [TestMethod]
        public void STA_StoresACCIntoMemory()
        {
            _registers.ACC.Value = 0x55;
            byte[] program = { (byte)OpCode.STA, 0x10, (byte)OpCode.HLT };
            _cpu.LoadProgram(program);
            _cpu.Run();

            Assert.AreEqual(0x55, _memory.ReadByte(0x10));
        }

        [TestMethod]
        public void ADD_SetsACCAndFlagsCorrectly()
        {
            _registers.ACC.Value = 0x01;
            _memory.WriteByte(CPU.ProgramStart + 0x00, 0x01);

            byte[] program = { (byte)OpCode.ADD, (byte)(CPU.ProgramStart + 0x00), (byte)OpCode.HLT };
            _cpu.LoadProgram(program);
            _cpu.Run();

            Assert.AreEqual(0x02, _registers.ACC.Value); // ACC suma correctamente
            Assert.IsFalse(_registers.GetFlag(Flags.Zero));
        }


        [TestMethod]
        public void JMP_JumpsToCorrectAddress()
        {
            byte[] program = { (byte)OpCode.JMP, 0x05, (byte)OpCode.HLT };
            _cpu.LoadProgram(program, 0);
            _cpu.Run();

            Assert.AreEqual(0x05, _registers.PC.Value); // PC debe apuntar al destino
        }

        [TestMethod]
        public void CMP_UpdatesFlagsOnly()
        {
            _registers.ACC.Value = 0x05;
            _memory.WriteByte(CPU.ProgramStart + 0x00, 0x05);

            byte[] program = { (byte)OpCode.CMP, (byte)(CPU.ProgramStart + 0x00), (byte)OpCode.HLT };
            _cpu.LoadProgram(program);
            _cpu.Run();

            Assert.AreEqual(0x05, _registers.ACC.Value); // ACC no cambia
            Assert.IsTrue(_registers.GetFlag(Flags.Zero)); // Zero flag set
        }



        [TestMethod]
        public void HLT_StopsExecution()
        {
            byte[] program = { (byte)OpCode.HLT };
            _cpu.LoadProgram(program);
            _cpu.Run();

            Assert.IsTrue(true); // CPU terminó sin errores
        }

    }
}
