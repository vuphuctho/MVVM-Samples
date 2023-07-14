using Moq;
using MvvmSample.Core.Models;
using MvvmSample.Core.Services;
using MvvmSample.Core.ViewModels.Widgets;

namespace MvvmSample.Core.Tests.ViewModels.Widgets;

public class SubredditWidgetViewModelUnitTest
{
    private readonly PostsQueryResponse mockedResponse = new PostsQueryResponse(
        new PostListing(new List<PostData>()
        {
            new PostData(new Post("Post 1", "lorem ipsum")),
            new PostData(new Post("Post 2", "lorem ipsum")),
            new PostData(new Post("Post 3", "lorem ipsum")),
            new PostData(new Post("Post 4", "lorem ipsum")),
            new PostData(new Post("Post 5", "lorem ipsum")),
        })
    );
    
    [Fact]
    public void ViewModel_CanInit()
    {
        var redditService = new Mock<IRedditService>();
        var settingsService = new Mock<ISettingsService>();

        var vm = new SubredditWidgetViewModel(redditService.Object, settingsService.Object);
        Assert.NotNull(vm);
        Assert.Equal(vm.SelectedSubreddit, vm.Subreddits[0]);
    }

    [Fact]
    public async Task LoadPostsAsync_ShouldUpdatePosts()
    {
        var redditService = new Mock<IRedditService>();
        var settingsService = new Mock<ISettingsService>();
        redditService.Setup(s => s.GetSubredditPostsAsync("microsoft"))
            .ReturnsAsync(mockedResponse);
        
        var vm = new SubredditWidgetViewModel(redditService.Object, settingsService.Object);
        await vm.LoadPostsCommand.ExecuteAsync(null);
        Assert.NotNull(vm.Posts);
        Assert.NotEmpty(vm.Posts);
        Assert.Equal(vm.Posts.Count, mockedResponse.Data.Items.Count);
    }
}