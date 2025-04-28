using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FluentValidation;
using MediatR;
using TalkNest.Application.Comments;

namespace TalkNest.Application.Posts
{
    public class PostViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
   
}
