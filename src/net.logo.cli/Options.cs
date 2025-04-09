using CommandLine;

namespace net.logo.cli;

[Verb("compile", HelpText = "Check a logo file.")]
public class CompileOptions
{
    [Value(0, Required = true, HelpText = "Path to the logo file.")]
    public string LogoFilePath { get; set; }
    
    [Option('v', "verbose", Required = false, HelpText = "Enable verbose output.")]
    public bool Verbose { get; set; } = false;
}

[Verb("generate", HelpText = "Generate a SVG from a logo file.")]
public class GenerateOptions
{
    [Option('o', "output", Required = false, HelpText = "Output file name.")]
    public string OutputFile { get; set; } = string.Empty;

    [Option('v', "verbose", Required = false, HelpText = "Enable verbose output.")]
    public bool Verbose { get; set; } = false;
    
    [Value(0, Required = true, HelpText = "Path to the logo file.")]
    public string LogoFilePath { get; set; }
    
}

