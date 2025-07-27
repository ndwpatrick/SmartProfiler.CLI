# SmartProfiler.CLI 🚀

_A blazing-fast, intelligent C# CLI tool for measuring execution time, memory consumption, and detecting performance bottlenecks in .NET assemblies._

[![NuGet](https://img.shields.io/nuget/v/SmartProfiler.CLI.svg)](https://www.nuget.org/packages/SmartProfiler.CLI)

[![NuGet Downloads](https://img.shields.io/nuget/dt/SmartProfiler.CLI.svg)](https://www.nuget.org/packages/SmartProfiler.CLI)

💼 [Project Roadmap Board](https://github.com/users/ndwpatrick/projects/1/views/1)

---

## 📌 Features

- 🛠️ **Profile Any Static Method**: Just point to your compiled DLL.
- ⏱️ **Precise Execution Timing**: Average, min, max durations per run.
- 🧠 **Memory Consumption Insights**: Detect memory leaks and GC pressure.
- 🔍 **Async/Sync Friendly**: Handles both synchronous and asynchronous methods seamlessly.
- 📊 **Export to CSV**: Easy-to-analyze structured output.
- ⚡ **Cross-Platform**: Windows, MacOS, and Linux supported.
- 📈 **Performance Regression Detector** (Coming Soon 🚧).

---

## 🚀 Quick Start

### 1. Clone the Repository

- git clone https://github.com/ndwpatrick/SmartProfiler.CLI.git
- cd SmartProfiler.CLI

### 2. Build the Project

- dotnet build

---

## 🏗️ How It Works

- Loads your .dll dynamically at runtime.
- Locates the fully qualified static method.
- Executes the method multiple times (default 10).
- Measures:
  - Execution time (min/avg/max)
  - Memory usage (before/after/peak)
- Exports detailed CSV reports for analysis.
- Supports async and Task-returning methods out of the box. No extra configuration needed.

---

## 📦 **Installation**

Install SmartProfiler.CLI globally using the .NET CLI: (Requires .NET 6.0 SDK or higher installed.)

- dotnet tool install --global SmartProfiler.CLI

## ⚡ **Quick Start**

Profile the execution time and memory consumption of your C# methods easily.

**Example command:** smartprofiler --method YourNamespace.YourClass.YourMethod --detailed-memory

- method: Fully qualified method name you want to profile (e.g., MyApp.Services.MathService.AddNumbers).

- assembly: (Optional) Specify the path to your DLL if not in the working directory.

- detailed-memory: (Optional) Enables in-depth memory profiling, including:
  - Heap breakdown into LOH and SOH
  - Count of pinned objects
  - Finalizer queue stats
  - Overall GC memory pressure insights

---

## 🎯 Future Enhancements

- 🖥️ Real-time memory graph visualization (Blazor frontend)
- 📜 Multiple method comparison in a single run
- 🔥 Performance regression alerts
- 💾 Database integration for long-term storage
- 🧪 Built-in stress/load testing module

---

## :handshake: Used In

SmartProfiler has been adopted in:

- Enterprise CI/CD pipelines via tools like **Artifactory**
- Application security environments powered by **Checkmarx**
- Developer workflows optimizing .NET memory and performance

> ⚠️ _Mention based on anonymous telemetry & download analytics. Not an official endorsement._

---

## 🙌 Acknowledgements

Inspired by the need for developer-first profiling tools that bridge the gap between brilliant code and high-performing applications.

---

## 📜 License

MIT License.
Use freely. Contribute back. Grow the ecosystem. 🌱
