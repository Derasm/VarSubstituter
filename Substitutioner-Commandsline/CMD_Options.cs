namespace Substitutioner_Commandsline;
using CommandLine;
public class CMD_Options
{
    [Option( HelpText = "Array of key:value pairs.")]
    public string[] Variables { get; set; }
    [Option( HelpText = "List of files to do Substitution on.")]
    public string[]? FilesList { get; set; }
    [Option( HelpText = "Pattern for file-selection. Supports Wildcards and paths.")]
    public string? FilePattern { get; set; }
    [Option( HelpText = "Sets Root folder to something else than current Root.")]
    public string? RootFolder { get; set; }
    [Option('v', "verbose", Default = false, HelpText = "Set output to verbose.")]
    public bool Verbose { get; set; }
    
}