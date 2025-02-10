namespace Substitutioner_Commandsline;
using CommandLine;
public class CMD_Options
{
    [Option( 'v', "variables", Required = true, HelpText = "Array of key:value pairs.")]
    public IEnumerable<string> Variables { get; set; }
    
    [Option( 'l', "filelist", HelpText = "List of files to do Substitution on.")]
    public IEnumerable<string>? FilesList { get; set; }
    
    [Option( 'p', "filepattern", HelpText = "Pattern for file-selection. Supports Wildcards and paths.")]
    public string? FilePattern { get; set; }
    
    [Option( 'f', "folder", HelpText = "Sets Root folder to something else than current Root.")]
    public string? RootFolder { get; set; }
    
    [Option( "verbose", Default = false, HelpText = "Set output to verbose.")]
    public bool Verbose { get; set; }
    
}