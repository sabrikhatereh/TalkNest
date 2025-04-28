using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace TalkNest.Application.Comments
{
    public record CreateCommentCommand(Guid Id, Guid PostId, string Text) : IRequest<Guid?>;
    public class CreateCommentRequestDto
    {
        [Required(ErrorMessage = "PostId is required.")]
        public Guid PostId { get; set; }

        [Required(ErrorMessage = "Text is required.")]
        [MaxLength(500, ErrorMessage = "Text cannot exceed 200 characters.")]
        public string Text { get; set; }
    }
}
