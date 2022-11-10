using DimonSmart.FileByContentComparer;
using FluentAssertions;
using Xunit.Abstractions;

namespace FileByContentComparerTests;

public class TestsBase
{
    protected readonly ITestOutputHelper _testOutputHelper;
    public TestsBase(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
}


public class FileByContentComparerTests : TestsBase
{
    public FileByContentComparerTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Theory]
    [InlineData(@"ABC", @"ABC", true)]
    [InlineData(@"A", @"A", true)]
    [InlineData(@"", @"", true)]
    [InlineData(@"A", @"", false)]
    [InlineData(@"", @"A", false)]
    [InlineData(@"ABC", @"ABCD", false)]
    [InlineData(@"ABCD", @"ABCD", true)]
    [InlineData(@"ABCD+", @"ABCD-", false)]
    public void FileByContentComparerTest(string content1, string content2, bool expectedResul)
    {
        var comparer = new FileByContentComparer(new FileByContentComparer.FileByContentComparerOptions(4));
        using var stream1 = GetStreamFromString(content1);
        using var stream2 = GetStreamFromString(content2);
        var result = comparer.Compare(stream1, stream2);
        result.Should().Be(expectedResul);
    }

    public static Stream GetStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}