namespace Test_FileHandler;

using System.IO;
using System.Text.Json;

public static class TestFileHelper
{
    public static string SetupTestEnvironment(string jsonConfigPath)
    {
        string testRootPath = string.Empty;

        try
        {
            // Read and parse the JSON config
            string jsonContent = File.ReadAllText(jsonConfigPath);
            var config = JsonSerializer.Deserialize<TestConfig>(jsonContent);

            if (config == null)
                throw new InvalidOperationException("Invalid configuration.");

            testRootPath = config.RootFolder;
            if (!Directory.Exists(testRootPath))
            {
                Directory.CreateDirectory(testRootPath);
            }

            // Create files based on the config
            foreach (var file in config.Files)
            {
                string directory = Path.GetDirectoryName(file.Path);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(file.Path, "Sample content"); // Add dummy content
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting up test environment: {ex.Message}");
        }

        return testRootPath;
    }

    public static void TeardownTestEnvironment(string rootPath)
    {
        try
        {
            if (Directory.Exists(rootPath))
            {
                Directory.Delete(rootPath, true); // Recursively delete all files and folders
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error tearing down test environment: {ex.Message}");
        }
    }

    private class TestConfig
    {
        public string RootFolder { get; set; }
        public List<FileConfig> Files { get; set; }
    }

    private class FileConfig
    {
        public string Path { get; set; }
    }
}
