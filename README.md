# SmartProfiler.CLI 🚀
*A blazing-fast, intelligent C# CLI tool for measuring execution time, memory consumption, and detecting performance bottlenecks in .NET assemblies.*

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

git clone https://github.com/your-github-username/SmartProfiler.CLI.git
cd SmartProfiler.CLI

### 2. Build the Project
dotnet build

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

## 🎯 Future Enhancements
- 🖥️ Real-time memory graph visualization (Blazor frontend)
- 📜 Multiple method comparison in a single run
- 🔥 Performance regression alerts
- 💾 Database integration for long-term storage
- 🧪 Built-in stress/load testing module

---

## 🙌 Acknowledgements
Inspired by the need for developer-first profiling tools that bridge the gap between brilliant code and high-performing applications.

---

## 📜 License
MIT License.
Use freely. Contribute back. Grow the ecosystem. 🌱

