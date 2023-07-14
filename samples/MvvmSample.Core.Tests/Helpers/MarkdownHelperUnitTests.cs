using MvvmSample.Core.Helpers;

namespace MvvmSample.Core.Tests.Helpers;

public class MarkdownHelperUnitTests
{
    [Theory]
    [MemberData(nameof(MarkdownHelperTestData))]
    public void GetParagraphs_ShouldReturnCorrectNumberOfParagraphs
    (string markdownString, int expected)
    {
        var paragraphs = MarkdownHelper.GetParagraphs(markdownString);
        Assert.Equal(expected, paragraphs.Count);
    }

    public static IEnumerable<object[]> MarkdownHelperTestData()
    {
        yield return new object[] { "", 0 };
        // yield return new object[] { "First paragraph\nSecond paragraph", 2 };
    }
}
