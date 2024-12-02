// See https://aka.ms/new-console-template for more information
// This has an entrypoint specified - it is a CMD program that takes the following:
//  Filelist - array of strings
//  Keyvalue array of variables being substituted - hashmap / dictionairy. Can also be an array where its setup as:
// "key:value". It is used in the file as: #{key} meaning that whatever variable you use, wrap it in #{} to sub it. 
//  File-extension - string.
// File-pattern - makes it possible to check multiple levels.
// Rootfolder - string. Used if not intending to use the original rootfolder 
using CommandLine;
namespace Substitutioner_Commandsline;
class Program
{
    private static DataHandler dataHandler;
    static int Main(string[] args)
    {
        return Parser.Default.ParseArguments<CMD_Options>(args)
        .MapResult(
            (CMD_Options opts) => RunWithOptions(opts),
            errs => HandleParseError(errs));
    }
    static int RunWithOptions(CMD_Options opts)
    {
        if (opts.Verbose)
        {
            Console.WriteLine("Verbose mode enabled.");
            
        }
        dataHandler = new DataHandler(opts);

        return 0; // Success
    }
    static int HandleParseError(IEnumerable<Error> errs)
    {
        // Handle errors (e.g., show usage info)
        Console.WriteLine("Invalid arguments provided.", errs);
        return 1; // Failure
    }
    
    
}

