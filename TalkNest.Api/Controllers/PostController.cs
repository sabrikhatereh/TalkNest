using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using TalkNest.Core.Shared.Result;
using TalkNest.Application.Posts.Commands.CreatePost;
using Swashbuckle.AspNetCore.Annotations;
using System;
using TalkNest.Application.Posts.Queries;
using TalkNest.Core.Models;
using TalkNest.Application.Posts;
using TalkNest.Application.Posts.Commands.UpdatePost;
using System.Collections;
using System.Collections.Generic;
using TalkNest.Application.Comments;
using Microsoft.AspNetCore.Http.HttpResults;
using MediatR;

namespace TalkNest.Api.Controllers
{
    [ApiController]
    [Route(BaseApiPath + "/Posts")]
    public class PostController : BaseController
    {
        // GET: api/Post/{id}
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Core.Models.Post), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [SwaggerOperation(Summary = "Get a Post by ID")]
        public async Task<ActionResult<Result<PostViewModel>>> GetPostById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetPostQuery(id);
            var Post = await Mediator.Send(query, cancellationToken);

            return Result.Success(Post);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Create new Post", Description = "Create new Post")]
        public async Task<Result<PostViewModel>> Create([FromBody] CreatePostRequestDto createPostRequestDto,
        CancellationToken cancellationToken)
        {
            var command = Mapper.Map<CreatePostRequestDto, CreatePostCommand>(createPostRequestDto);

            var result = await Mediator.Send(command, cancellationToken);

            return Result.Success(result);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update an existing Post")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Result<PostViewModel>> UpdatePost([FromBody] UpdatePostRequestDto request, CancellationToken cancellationToken)
        {
            var command = Mapper.Map<UpdatePostRequestDto, UpdatePostCommand>(request);

            var result = await Mediator.Send(command, cancellationToken);

            return Result.Success(result);
        }


        [HttpGet]
        public async Task<Result<List<PostViewModel>>> GetPosts(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new ListPostsQuery(), cancellationToken);
            return Result.Success(result);
        }

        [HttpPost("AddComment")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful response. Add comment to post")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Add comment to post", Description = "Add comment to post")]
        public async Task<Result<Guid?>> AddComment([FromBody] CreateCommentRequestDto createCommentRequest,
       CancellationToken cancellationToken)
        {
            var command = Mapper.Map<CreateCommentRequestDto, CreateCommentCommand>(createCommentRequest);

            var result = await Mediator.Send(command, cancellationToken);

            return Result.Success(result);
        }

    }

}

