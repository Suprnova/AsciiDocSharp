
using Microsoft.Extensions.Logging;

namespace AsciiDocSharp;

public enum SafeMode
{
    Unsafe = 0,
    Safe = 1,
    Server = 10,
    Secure = 20,
}

public class Configuration
{
    // TODO: Implement Configuration class, used to config output type, attributes from CLI, etc.
    // temp fix to allow compiling
    public Dictionary<string, string> Attributes { get; private set; } = [];
    public string[] InputFiles { get; private set; } = [];
    public string OutputFile { get; private set; } = "";
    public SafeMode SafeMode { get; private set; } = SafeMode.Unsafe;
    public bool SourceMap { get; private set; } = false;
    public bool Standalone { get; private set; } = true;
    public string BaseDir { get; private set; } = ".";
    public string SourceDir { get; private set; } = ".";
    public string DestinationDir { get; private set; } = ".";
    public string[] LoadPaths { get; private set; } = [];
    public LogLevel LogLevel { get; private set; } = LogLevel.Warning;
    public LogLevel FailureLevel { get; private set; } = LogLevel.Critical;
    public bool Trace { get; private set; } = false;
    public bool Warnings { get; private set; } = false;
    public bool Timings { get; private set; } = false;
}
