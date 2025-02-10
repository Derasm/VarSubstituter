using Substitutioner_Commandsline;
using Xunit;

namespace Test_FileHandler;


public class FileHandlerTests  :IDisposable
{
    private readonly string _testRootPath;
    private CMD_Options _options;
    
    public FileHandlerTests()
    {
        // Set up test environment
        _testRootPath = TestFileHelper.SetupTestEnvironment("FileStructure.json");
    }

    [Fact]
    public void Test_FilePattern_Matching()
    {
        _options = new CMD_Options
        {
            FilePattern = ".txt",
            RootFolder = _testRootPath,
            Variables = ["First", "Second", "Third", "Fourth", "Fifth"]
        };
        // Arrange
        var fileHandler = new FileHandler(_options);

        // Act
        var files = fileHandler.GetFiles();

        // Assert
        Assert.Contains(Path.Combine(_testRootPath, "file1.txt"), files);
        Assert.Contains(Path.Combine(_testRootPath, "SubFolder/file3.txt"), files);
        Assert.DoesNotContain(Path.Combine(_testRootPath, "file2.log"), files);
    }

    [Fact]
    public void Test_RootFolder_Override()
    {
        // Arrange
        _options = new CMD_Options
        {
            FilePattern = "*.json",
            RootFolder = Path.Combine(_testRootPath, "SubFolder"),
            Variables = ["First", "Second", "Third", "Fourth", "Fifth"]
        };
        // Arrange
        var fileHandler = new FileHandler(_options);

        // Act
        var files = fileHandler.GetFiles();

        // Assert
        Assert.Single(files);
        Assert.Contains(Path.Combine(_testRootPath, "SubFolder/file4.json"), files);
    }

    [Fact]
    public void Test_Empty_FilePattern()
    {
        // Arrange
        _options = new CMD_Options
        {
            FilePattern = "*.*",
            RootFolder = _testRootPath,
            Variables = ["First", "Second", "Third", "Fourth", "Fifth"]
        };
        var fileHandler = new FileHandler(_options);

        // Act
        var files = fileHandler.GetFiles();

        // Assert
        Assert.Equal(4, files.Count);
    }

    public void Dispose()
    {
        // Tear down test environment
        TestFileHelper.TeardownTestEnvironment(_testRootPath);
    }
}