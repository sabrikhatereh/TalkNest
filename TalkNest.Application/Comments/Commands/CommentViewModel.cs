using System;

namespace TalkNest.Application.Comments
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedOnUtc { get; set; }
    }
}
