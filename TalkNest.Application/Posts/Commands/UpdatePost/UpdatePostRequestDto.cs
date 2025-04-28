using System;
using System.ComponentModel.DataAnnotations;

namespace TalkNest.Application.Posts.Commands.UpdatePost
{
    public class UpdatePostRequestDto
    {
        [Required(ErrorMessage = "Id is required.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [MaxLength(500, ErrorMessage = "Content cannot exceed 100 characters.")]
        public string Content { get; set; }

    }
}
