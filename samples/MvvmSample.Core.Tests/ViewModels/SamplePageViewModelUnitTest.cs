using MvvmSample.Core.Services;

namespace MvvmSample.Core.Tests.ViewModels;

using MvvmSample.Core.ViewModels;
using Xunit;
using Moq;

public class SamplePageViewModelUnitTest
{
    [Fact]
    public void SamplePageViewModel_CanInit()
    {
        var mockedFileService = CreateMockedFileService();
        var model = new SamplePageViewModel(mockedFileService.Object);
        Assert.NotNull(model);
    }

    [Theory]
    [InlineData("sampleFile", "test content")]
    public async Task LoadDocs_ShouldReturnTexts(string name, string fileContent)
    {
        var mockedFileService = CreateMockedFileService();
        
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        await writer.WriteAsync(fileContent);
        await writer.FlushAsync();
        stream.Position = 0;

        mockedFileService
            .Setup(s => s.OpenForReadAsync(name))
            .ReturnsAsync(stream);

        var model = new SamplePageViewModel(mockedFileService.Object);
        
        Assert.Null(model.Texts);
        
        await model.LoadDocsCommand.ExecuteAsync(name);
        
        Assert.NotNull(model.Texts);
    }

    private Mock<IFilesService> CreateMockedFileService()
    {
        var mocked = new Mock<IFilesService>();

        return mocked;
    }
}