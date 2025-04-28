using AutoFixture;
using Azure;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Org.BouncyCastle.Asn1.X509;
using System.IO;
using System.Net;
using System.Net.Http.Json;
using TalkNest.Application.Comments;
using TalkNest.Application.Posts;
using TalkNest.Application.Posts.Commands.UpdatePost;
using TalkNest.Core;
using TalkNest.Core.Models;
using TalkNest.Core.Shared.Result;
using TalkNest.Infrastructure.Repositories.CommandRepositories;
using TalkNest.IntegrationTests.Setup;

namespace TalkNest.IntegrationTests
{
    public class PostTests : DatabaseTest, IAsyncLifetime
    {
        private readonly HttpClient _client;
        public PostTests(TalkNestHostFixture factory) : base(factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreatePost_Then_CreateComment_Should_Succeed()
        {
            // Create a Post
            var createPostRequest = new
            {
                Title = "Integration Post",
                Content = "Post Content for Integration Test"
            };

            var postResponse = await _client.PostAsJsonAsync("/api/v1/posts", createPostRequest);
            postResponse.EnsureSuccessStatusCode();

            var postContent = await postResponse.Content.ReadFromJsonAsync<Result<PostViewModel>>();
            var postId = postContent.Value.Id;
            postId.Should().NotBe(Guid.Empty);

            // Create a Comment for that Post
            var createCommentRequest = new CreateCommentRequestDto
            {
                PostId = postId,
                Text = "This is a comment for integration test"
            };

            var commentResponse = await _client.PostAsJsonAsync($"/api/v1/posts/addComment", createCommentRequest);
            commentResponse.EnsureSuccessStatusCode();

            var commentContent = await commentResponse.Content.ReadFromJsonAsync<Result<Guid>>();
            var commentId = commentContent.Value;

            // Assert
            Assert.NotEqual(commentId, Guid.Empty);

            var getPostResponse = await _client.GetAsync($"/api/v1/posts/{postId}");
            getPostResponse.EnsureSuccessStatusCode();

            var getPostContent = await getPostResponse.Content.ReadFromJsonAsync<Result<PostViewModel>>();
            getPostContent.Value.Comments.Count.Should().Be(1);
            var comment = getPostContent.Value.Comments.FirstOrDefault();
            comment.Text.Should().Be(createCommentRequest.Text);
        }

        [Fact]
        public async Task UpdatePost_Should_Update_And_Return_Updated_Post()
        {
            // Arrange: First create a new post
            var createPostRequest = new
            {
                Title = "Original Title",
                Content = "Original Content"
            };

            var createResponse = await _client.PostAsJsonAsync("/api/v1/posts", createPostRequest);
            createResponse.EnsureSuccessStatusCode();

            var createdPostResult = await createResponse.Content.ReadFromJsonAsync<Result<PostViewModel>>();
            createdPostResult.Value.Should().NotBeNull();
            var createdPost = createdPostResult.Value;

            // Prepare Update request
            var updatePostRequest = new UpdatePostRequestDto
            {
                Id = createdPost.Id,
                Title = "Updated Title",
                Content = "Updated Content"
            };

            // Act: Call the Update API
            var updateResponse = await _client.PutAsJsonAsync("/api/v1/posts", updatePostRequest);

            // Assert
            updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var updatedPostResult = await updateResponse.Content.ReadFromJsonAsync<Result<PostViewModel>>();
            updatedPostResult.Should().NotBeNull();
            updatedPostResult.IsSuccess.Should().BeTrue();
            updatedPostResult.Value.Should().NotBeNull();
            updatedPostResult.Value.Id.Should().Be(createdPost.Id);
            updatedPostResult.Value.Title.Should().Be("Updated Title");
            updatedPostResult.Value.Content.Should().Be("Updated Content");
        }

        public new async Task InitializeAsync()
        {
            Fixture.Create<Post>();
            Fixture.Create<Comment>();
        }
    }



}

