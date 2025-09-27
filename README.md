# MyCpu – CPU Educativa en C#

**MyCpu** es un proyecto educativo que simula un CPU simple en C#. Su objetivo es enseñar los conceptos básicos de cómo funciona un procesador a nivel de instrucciones, memoria y registros.  

---

## 🔹 Características principales

- Simulación de CPU con:
  - Memoria de 256 bytes.
  - Registros: Accumulator (ACC), Program Counter (PC), Instruction Register (IR), Stack Pointer (SP).
- Ciclo de ejecución: **Fetch → Decode → Execute**.
- Soporte para instrucciones básicas:
  - `LDA addr` : Carga un valor de memoria en ACC.
  - `STA addr` : Guarda ACC en memoria.
  - `ADD addr` : Suma el valor de memoria a ACC.
  - `JMP addr` : Salto incondicional.
  - `HLT`      : Detener la ejecución.

---

## 🔹 Arquitectura del Proyecto

La solución está organizada en tres módulos principales:

### 1. MyCpu.Application
Contiene la lógica de aplicación y el punto de entrada (`Program.cs`). Aquí se orquesta la simulación y se gestionan las interacciones con el usuario.

**Subcarpetas:**
- `Builder/`: Clases para construir la CPU y sus componentes.
- `Factories/`: Fábricas para instanciar objetos principales del sistema.

### 2. MyCpu.Memory
Implementa los componentes centrales del CPU:
- `Core/`: Incluye la ALU (`ALU.cs`), CPU principal (`CPU.cs`), memoria (`Memory.cs`), contador de programa (`ProgramCounter.cs`), registro acumulador (`RegisterAcc.cs`) y registros generales (`Registers.cs`).

### 3. MyCpu.Shared
Define las interfaces y estructuras compartidas entre los módulos:
- `Enums/`: Enumeraciones como `Flags` y `OpCode` para instrucciones y estados.
- `Interfaces/`: Contratos para los componentes principales (ALU, memoria, registros, etc.).
- `Structures/`: Estructuras de datos como `AluResult`.

---

## 🔹 Instalación

1. Clonar el repositorio:

```bash
git clone https://github.com/EzeT4ch/MyCpu.git
```
