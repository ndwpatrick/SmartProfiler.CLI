using Spectre.Console.Cli;
using System.ComponentModel;

namespace SmartProfiler.CLI;

public class ProfileSettings : CommandSettings
{
    //It's a DTO-like object that maps directly to the input options of user commands
    [CommandArgument(0, "<processName>")]
    [Description("The name of the process to profile.")]
    public string ProcessName { get; set; }

    [CommandOption("--memory-log|-m")]
    [Description("Path to save memory profiling log")]
    public string? MemoryLogPath { get; set; }

    [CommandOption("--detailed-memory|-d")]
    [Description("Enable detailed memory profiling")]
    public bool UseDetailedMemory { get; set; } = false;

    [CommandOption("--cpu-profile")]
    [Description("Enable CPU profiling")]
    public bool CpuProfileEnabled { get; set; } = false;

    [CommandOption("--method")]
    [Description("Fully qualified method name to profile")]
    public string? MethodName { get; set; }

    [CommandOption("--assembly")]
    [Description("Path to the assembly containing the method")]
    public string? AssemblyPath { get; set; }

    [CommandOption("--verbose")]
    [Description("Enable verbose output")]
    public bool Verbose { get; set; } = false;
}