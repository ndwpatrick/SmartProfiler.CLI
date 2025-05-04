# 📦 Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).

## [release] - yyyy-mm-dd

### Added

- New feature implementation.

### Changed

- Change in previous implementation.

### Fixed

- Bug fixes.

---

## [1.1.0] - 2025-05-04

### Added

- 🧠 `--detailed-memory` CLI flag for advanced memory profiling.
  - Reports LOH (Large Object Heap), SOH (Small Object Heap), pinned objects count, and finalizer queue stats.
  - Logs memory diagnostics to file if `--memory-log` is used.
- 💾 New `MemoryProfiler.cs` to support structured and extensible memory analysis.

### Changed

- 📈 Improved memory report formatting in CLI output and file logs.

### Fixed

---

## [1.0.1] - 2025-04-29

### Added

- 📛 NuGet version badge in README for live version tracking.

### Changed

### Fixed

- 📝 Fixed NuGet packaging error due to missing `README.md` reference in `.csproj`.
- ✅ Enabled NuGet readme support via `<PackageReadmeFile>`.

---

## [1.0.0] - 2025-04-28

### Initial Release

- 🎉 CLI tool to profile .NET method execution time and memory usage.
- 🛠️ Arguments supported: `--method`, `--assembly`, `--memory-log`.
- 📂 Generates runtime diagnostics and logs to user-defined path.
