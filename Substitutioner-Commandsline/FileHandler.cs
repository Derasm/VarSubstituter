namespace Substitutioner_Commandsline;
/// <summary>
/// Handles gathering a list of files adhering to criteria from options.
/// </summary>
public class FileHandler
{
    public CMD_Options Options { get; set; }
    //Contains unique strings of files to do substitution on
    private HashSet<string>Files { get; set; } = new HashSet<string>();

    public FileHandler(CMD_Options options)
    {
        Options = new CMD_Options();
    }
    /// <summary>
    /// This method is the collator of Extension, Fileslist and Pattern method
    /// </summary>
    /// <returns></returns>
    public HashSet<string> GetFiles()
    {
        //Check that RootFolder exists. Throw exception if not. 
        if (string.IsNullOrWhiteSpace(Options.RootFolder) == false)
        {
            if (Directory.Exists(Options.RootFolder) == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Root folder doesn't exist: {Options.RootFolder}");
                Console.ResetColor();
                throw new Exception("Root folder doesn't exist");
            }
        }

        string[] filesFromExtension = GetFiles_Extension();
        //Call FilesList, Extension and Pattern. Pass in Rootfolder if its there. 
        return Files;
    }

    private string[] GetFiles_Extension()
    {
        //This can potentially lead to an issue of: is the CurrentDirectory where the program is, or where it is called from?
        string rootFolder = !string.IsNullOrWhiteSpace(Options.RootFolder)
            ? Options.RootFolder
            : Environment.CurrentDirectory;
        
        //if filepattern is false, essentially ignore it. 
        string filePattern = !string.IsNullOrWhiteSpace(Options.FilePattern) ? Options.FilePattern : "*.*";
        // Check for "**/" to determine recursive search
        bool isRecursive = filePattern.StartsWith("**/");
        if (isRecursive)
        {
            // Strip the "**/" prefix from the pattern
            filePattern = filePattern.Substring(3);
        }
        
        if (Options.Verbose)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Searching in: {rootFolder}");
            Console.WriteLine($"File pattern: {filePattern}");
            Console.WriteLine($"Recursive: {isRecursive}");
            Console.ResetColor();
        }
        //Check if we should check subdirectories or only toplevel: 
        bool checkAllDirectories = filePattern.Contains('/');
        //check AllDirectories or TopDirectory depending on checkAllDirectories. 
        
        try
        {
            // Search for files - Enumerate to stream instead of fetch all.
            string[] files = Directory.EnumerateFiles(
                rootFolder,
                filePattern,
                isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly
            ).ToArray();

            if (Options.Verbose)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Found {files.Length} files.");
                Console.ResetColor();
            }

            return files;
        }
        catch (Exception ex)
        {
            // Handle potential errors gracefully
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error occurred: {ex.Message}");
            Console.ResetColor();
            return [];
        }
    }
    
    
}