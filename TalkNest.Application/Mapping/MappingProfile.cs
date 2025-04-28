using AutoMapper;
using TalkNest.Application.Posts;
using TalkNest.Application.Posts.Commands.CreatePost;
using TalkNest.Application.Posts.Commands.UpdatePost;
using TalkNest.Core.Models;
using System;
using TalkNest.Application.Comments;
using TalkNest.Application.Posts.Queries;

namespace TalkNest.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Automatically maps properties with the same name
            CreateMap<CreatePostRequestDto, CreatePostCommand>()
                .ForCtorParam(ctorParamName: "Id", opt => opt.MapFrom(_ => Guid.NewGuid()));

            CreateMap<CreateCommentRequestDto, CreateCommentCommand>()
               .ForCtorParam(ctorParamName: "Id", opt => opt.MapFrom(_ => Guid.NewGuid()));

            CreateMap<Post, PostViewModel>().ForMember(dest => dest.CreatedOnUtc, opt => opt.MapFrom(_ => _.CreatedOnUtc));
            CreateMap<UpdatePostRequestDto, UpdatePostCommand>();
            CreateMap<Comment, CommentViewModel>();
        }
    }
}
